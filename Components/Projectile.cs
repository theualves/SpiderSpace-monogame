using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_engine
{
    public class Projectile
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;

        public Projectile(Texture2D texture, Vector2 position, float speed)
        {
            _texture = texture;
            _position = position;
            _speed = speed;
        }

        public void Update()
        {
            _position.Y -= _speed; // Move o projétil para cima
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        // Define o retângulo delimitador do projétil
        public Rectangle GetBounds()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
    }
}
