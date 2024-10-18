using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleSystemExample;

namespace Cookie_Clicker
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private TheCookie _theCookie;
        private GraphicsDeviceManager _graphics;
        private SpriteFont _font;
        private CrumbleSystem _crumble;
        private MouseState _past;
        private double _elapsedTime; 

        private bool GameStart;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _crumble = new CrumbleSystem(this, 3);
            Components.Add( _crumble );
            _theCookie = new TheCookie();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _theCookie.LoadContent(Content);
            _font = Content.Load<SpriteFont>("Phy");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Handle exit conditions
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            // Check if 10 seconds have passed to start the game
            if (_elapsedTime < 10)
            {
                return; // Do nothing until 10 seconds have passed
            }
            // Handle mouse input
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && _past.LeftButton == ButtonState.Released)
            {
                Vector2 mousePosition = new Vector2(mouse.X, mouse.Y);
                if (_theCookie.hitbox.CollidesWith(mousePosition))
                {
                    _theCookie.OnClick();
                    _crumble.PlaceCrumble(mousePosition);
                }
            }
            _past = mouse;

            // Update the cookie
            _theCookie.Update(gameTime);

            // Add other update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (_elapsedTime < 10)
            {
                _spriteBatch.DrawString(_font, $"Score: {_theCookie.score}", new Vector2(10, 10), Color.White);
                _spriteBatch.DrawString(_font, $"Time: {(int)_elapsedTime}", new Vector2(10, 50), Color.White);
                _theCookie.Draw(gameTime, _spriteBatch);
            }
            else
            {
                _spriteBatch.DrawString(_font, $"Score: {_theCookie.score}", new Vector2(10, 10), Color.White);
                _spriteBatch.DrawString(_font, $"Time: {(int)_elapsedTime}", new Vector2(10, 50), Color.White);
                _theCookie.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}