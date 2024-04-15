using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_engine
{
    public class Spider
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;
        private bool right;
        private bool left;

        private bool _disappeared = false;

        public float Velocity { get => _speed;}


        public Spider(Texture2D texture, Vector2 position, float speed)
        {
            _texture = texture;
            _position = position;
            _speed = speed;
        }

        public void Initialize()
        {
            right = true;
            left = false;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (right)
            {
                if (_position.X < Globals.SCREEN_WIDTH - _texture.Width)
                {
                    _position.X += _speed;
                }
                else
                {
                    right = false;
                    left = true;
                }
            }
            if (left)
            {
                if (_position.X > 0)
                {
                    _position.X -= _speed;
                }
                else
                {
                    right = true;
                    left = false;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_disappeared)
            {
            spriteBatch.Draw(_texture, _position, Color.White);                
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }

        public bool HasDisappeared()
        {
            return _disappeared;
        }

        public void Disappear()
        {
            _disappeared = true;
        }

        public void SetPosition(float X)
        {
            _position.X = X;
        }

    }



}