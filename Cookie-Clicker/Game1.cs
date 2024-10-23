using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleSystemExample;
using System;

namespace Cookie_Clicker
{
    public class Game1 : Game
    {
        private Song music;
        private SpriteBatch _spriteBatch;
        private TheCookie _theCookie;
        private GraphicsDeviceManager _graphics;
        private SpriteFont _font;
        private CrumbleSystem _crumble;
        private MouseState _past;
        private double _elapsedTime;
        private TheShoe _theShoe;
        private bool GameStart;
        bool superGameEnd;
        TheBoot _theBoot;
        Song test;
        private Texture2D Cursor;
        private goldencookie _goldencookie;
        private double miniBootSpawnTimer;
        private double miniBootSpawnInterval;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// creates a new random interval between 30 and 60 seconds
        /// </summary>
        private void SetMiniBootSpawnInterval()
        {
            miniBootSpawnInterval = random.Next(10, 15);
        }
        protected override void Initialize()
        {
            test = Content.Load<Song>("test");
            _theBoot = new TheBoot();
            _theBoot.OnBootHit = TheBootHit;

            _goldencookie = new goldencookie();
            random = new Random();
            SetMiniBootSpawnInterval();
            miniBootSpawnTimer = 0;


            music = Content.Load<Song>("CookieClickerTheme");
            GameStart = false;
            superGameEnd = false;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _theShoe = new TheShoe();
            _crumble = new CrumbleSystem(this, 3);
            Components.Add( _crumble );
            _theCookie = new TheCookie();
            
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Cursor = Content.Load<Texture2D>("TheShoe");
            _goldencookie.LoadContent(Content);
            _theBoot.LoadContent(Content);
            _theShoe.LoadContent(Content);
            _theCookie.LoadContent(Content);
            _font = Content.Load<SpriteFont>("Phy");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MediaPlayer.IsRepeating = true;

        }

        /// <summary>
        /// stuff for handeling the music changes
        /// </summary>
        private float currentVolume = 0f;
        private bool isVolumeIncreasing = false; 
        /// <summary>
        /// going back i really should have made a seprete class for detecting all these things or something. 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;




            ///handle cookie clicking
            MouseState mouse = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouse.X, mouse.Y);
            if (mouse.LeftButton == ButtonState.Pressed && _past.LeftButton == ButtonState.Released)
            {
                
                if (_theCookie.hitbox.CollidesWith(mousePosition))
                {
                    _theCookie.OnClick(GameStart);
                    _crumble.PlaceCrumble(mousePosition);
                }
            }
            //handle shoe clicking
            #region shoe and play music and change cursor. "a lot happens here"
            ///handle  shoe input
            if (mouse.LeftButton == ButtonState.Pressed && _past.LeftButton == ButtonState.Released)
            {
                if (_theShoe.Hitbox.CollidesWith(mousePosition))
                {

                    _theShoe.OnClick();
                    Mouse.SetCursor(MouseCursor.FromTexture2D(Cursor, 0,0));
                    GameStart = true;
                    _theCookie.gamestart = true;
                    if (MediaPlayer.State != MediaState.Playing)
                    {
                        MediaPlayer.Play(test);
                        isVolumeIncreasing = true;
                    }
                }
            }
            if (isVolumeIncreasing)
            {
                currentVolume += 0.0025f;
                if (currentVolume >= 0.5f)
                {
                    currentVolume = 0.5f;
                    isVolumeIncreasing = false;
                }
                MediaPlayer.Volume = currentVolume;
            }
            #endregion
            #region make shake
            if (isShaking)
            {
                shakeDuration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (shakeDuration <= 0)
                {
                    isShaking = false;
                }
            }
            #endregion
            #region goldencookie
            // MiniBoot spawn timer
            miniBootSpawnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (miniBootSpawnTimer >= miniBootSpawnInterval && _goldencookie.isVisible == false)
            {
                _goldencookie.Spawn();

            }
            if (mouse.LeftButton == ButtonState.Pressed && _past.LeftButton == ButtonState.Released)
            {
                if (_goldencookie.Hitbox.CollidesWith(mousePosition))
                {
                    if (_goldencookie.isVisible)
                    {
                        _goldencookie.onClick(); 
                        SetMiniBootSpawnInterval();
                        miniBootSpawnTimer = 0;
                        _theBoot.onTrigger();
                    }

                }
            }
            #endregion

            _past = mouse;
            _theCookie.Update(gameTime);
            base.Update(gameTime);
            if (_elapsedTime < 4)
            {
                return;
            }
            _theBoot.Update(gameTime);
            _theShoe.Update(gameTime);
 
        }
        #region boot stuff
        bool isShaking = false;
        float shakeDuration = 0f;
        float shakeMagnitude = 10f;
        private Random random = new Random();
        private void TheBootHit()
        {
            _theCookie.GotBooted();
            isShaking = true;
            shakeDuration = 0.5f;
            for(int i = 0; i <10; i++)
            {
                _crumble.PlaceCrumble(new Vector2(random.Next(250, 550), random.Next(350, 650)));
            }
        }
        #endregion
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 114, 214));
            Matrix shakeTransform = Matrix.Identity;
            if (isShaking)
            {
                Vector2 shakeOffset = new Vector2((float)(random.NextDouble() * 2 - 1) * shakeMagnitude, (float)(random.NextDouble() * 2 - 1) * shakeMagnitude);

                shakeTransform = Matrix.CreateTranslation(shakeOffset.X, shakeOffset.Y, 0);
            }
            _spriteBatch.Begin(transformMatrix: shakeTransform);
  
            if(superGameEnd == false)
            {
                _theShoe.Draw(gameTime, _spriteBatch);
                if (GameStart == false){ _spriteBatch.DrawString(_font, "Clicker", new Vector2(425, 50), Color.White); }
                else{_spriteBatch.DrawString(_font, "Kicker", new Vector2(425, 50), Color.White);}

                _spriteBatch.DrawString(_font, "Cookie", new Vector2(325, 50), Color.White);
                if (GameStart != true)
                {
                    _spriteBatch.DrawString(_font, "Score: " + _theCookie.score.ToString(), new Vector2(10, 10), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(_font, "Score: " + _theCookie.score.ToString("F3"), new Vector2(10, 10), Color.White);
                }

                _spriteBatch.DrawString(_font, $"Time: {(int)_elapsedTime}", new Vector2(10, 50), Color.White);
                _theCookie.Draw(gameTime, _spriteBatch);
                _theBoot.Draw(gameTime, _spriteBatch);
                _goldencookie.Draw(gameTime, _spriteBatch);

            }
            else
            {
                GraphicsDevice.Clear(new Color(0, 0, 0));
                _spriteBatch.DrawString(_font, "GAME IS SUPER END", new Vector2(325, 50), Color.Red);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}