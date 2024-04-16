using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace Game_engine
{
    public class Ship
    {
        private List<Texture2D> _textures; // Lista de texturas para animação da nave
        private int _currentTextureIndex = 0; // Índice da textura atual
        private Vector2 _position;
        private float _speed;

        private Texture2D _projectileTexture;
        private List<Projectile> _projectiles = new List<Projectile>();
        private float _shootCooldown = 0.5f;
        private float _shootTimer = 0f;
        private List<SoundEffect> _collisionSoundEffects = new List<SoundEffect>();
        private Random _random = new Random();

        public Ship(List<Texture2D> textures, Vector2 position, float speed, Texture2D projectileTexture, List<SoundEffect> collisionSoundEffects)
        {
            _textures = textures;
            _position = position;
            _speed = speed;
            _projectileTexture = projectileTexture;
            _collisionSoundEffects = collisionSoundEffects;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Animação da nave
            AnimateShip();

            // Movimento da nave
            MoveShip(keyboardState);

            // Atualização do temporizador de disparo
            _shootTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Disparo de projéteis
            if (keyboardState.IsKeyDown(Keys.Space) && _shootTimer <= 0)
            {
                Shoot();
                _shootTimer = _shootCooldown;
            }

            // Atualiza os projéteis
            foreach (Projectile projectile in _projectiles)
            {
                projectile.Update();
            }

            // Limita o movimento da nave dentro da tela
            if (_position.X < 0)
            {
                _position.X = 0;
            }
            else if (_position.X > Globals.SCREEN_WIDTH - _textures[_currentTextureIndex].Width)
            {
                _position.X = Globals.SCREEN_WIDTH - _textures[_currentTextureIndex].Width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_currentTextureIndex], _position, Color.White);

            // Desenha os projéteis
            foreach (Projectile projectile in _projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        private void AnimateShip()
        {
            // Atualiza a textura da nave para próxima da lista
            // Aqui você pode implementar a lógica para a animação desejada
            // Por exemplo, pode alternar entre os sprites "ship-1", "ship-2", "ship-3", etc.
            // Esta é uma implementação simples que apenas alterna entre as texturas sequencialmente
            _currentTextureIndex++;
            if (_currentTextureIndex >= _textures.Count)
            {
                _currentTextureIndex = 0;
            }
        }

        private void MoveShip(KeyboardState keyboardState)
        {
            // Movimento da nave baseado na entrada do teclado
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _position.X -= _speed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _position.X += _speed;
            }
            if (keyboardState.IsKeyDown(Keys.W))
            {
                _position.Y -= _speed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _position.Y += _speed;
            }
        }

        private void Shoot()
        {
            Projectile newProjectile = new Projectile(_projectileTexture, new Vector2(_position.X + _textures[_currentTextureIndex].Width / 2 - _projectileTexture.Width / 2, _position.Y), _speed);
            _projectiles.Add(newProjectile);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _textures[_currentTextureIndex].Width, _textures[_currentTextureIndex].Height);
        }

        public void HasCollided(Spider spider)
        {
            Rectangle spiderBounds = spider.GetBounds();

            // Verifica colisão entre os projéteis e a aranha
            foreach (Projectile projectile in _projectiles)
            {
                Rectangle projectileBounds = projectile.GetBounds();

                if (projectileBounds.Intersects(spiderBounds))
                {
                   try
            {
                int index = _random.Next(_collisionSoundEffects.Count);
                _collisionSoundEffects[index].Play();
            }
            catch
            {
                // Exceção ignorada
            }
                    // Remove o projétil
                    _projectiles.Remove(projectile);
                    break;
                }
            }
        }
    }
}
