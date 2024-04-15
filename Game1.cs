using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Game_engine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        private Vector2 _backgroundPosition = Vector2.Zero; // Posição do fundo
        private Ship _ship;
        private Spider _spider;
        private Texture2D _projectileTexture;
        private const float BACKGROUND_SPEED = 100.0f;
        private List <SoundEffect> _collisionSoundEffect = new List<SoundEffect>();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 531;
            _graphics.PreferredBackBufferHeight = 885;
            _graphics.ApplyChanges();
        }

protected override void Initialize()
        {
            base.Initialize();
            Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
            Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;

            _collisionSoundEffect.Add(Content.Load<SoundEffect>("pare-effect"));
            _collisionSoundEffect.Add(Content.Load<SoundEffect>("cavalo-effect"));
            _collisionSoundEffect.Add(Content.Load<SoundEffect>("jesus-effect"));
            _collisionSoundEffect.Add(Content.Load<SoundEffect>("ui-effect"));
            _collisionSoundEffect.Add(Content.Load<SoundEffect>("queisso-effect"));
            _collisionSoundEffect.Add(Content.Load<SoundEffect>("ai-effect"));
            _projectileTexture = Content.Load<Texture2D>("shoot");
            List<Texture2D> shipTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("sprites-ship/ship-1"),
                Content.Load<Texture2D>("sprites-ship/ship-2"),
                Content.Load<Texture2D>("sprites-ship/ship-3")
            };
            _ship = new Ship(shipTextures, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 130), 5.0f, _projectileTexture, _collisionSoundEffect);
            _spider = new Spider(Content.Load<Texture2D>("spider"), new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - 880), 3.0f);
            _spider.Initialize();
            _backgroundPosition = new Vector2(0, -(_background.Height - _graphics.PreferredBackBufferHeight));
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("stars");

            // Carrega a textura do projétil
            _projectileTexture = Content.Load<Texture2D>("shoot");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            // move a imagm de fundo para baixo
            _backgroundPosition.Y += BACKGROUND_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //  reinicia a posição da imagem
            if (_backgroundPosition.Y >= 0)
                _backgroundPosition.Y = -(_background.Height - _graphics.PreferredBackBufferHeight);

            _ship.Update(gameTime);
            _spider.Update(gameTime);

            _ship.HasCollided(_spider);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, _backgroundPosition, Color.White); // Desenha o fundo na posição atual
            _ship.Draw(_spriteBatch);
            _spider.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
