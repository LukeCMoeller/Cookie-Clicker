using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleSystemExample;
using System;
using System.Threading.Tasks.Sources;
using System.Windows.Forms.Design;

namespace Cookie_Clicker
{
    public class Game1 : Game
    {
        TileMap tileMap;
        TextHandler th;
        private Song music;
        private SpriteBatch _spriteBatch;
        private TheCookie _theCookie;
        private GraphicsDeviceManager _graphics;
        private CrumbleSystem _crumble;
        private MouseState _past;
        private double _elapsedTime;
        private SpriteFont _font;
        private TheShoe _theShoe;
        private bool GameStart;
        bool superGameEnd;
        TheBoot _theBoot;
        Song test;
        private Texture2D Cursor;
        private goldencookie _goldencookie;
        private double GoldenTimer;
        private double GoldenSpawnInterval;
        public ContentImporter importer;
        Texture2D mess;
        Texture2D end;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// creates a new random interval between 30 and 60 seconds
        /// </summary>
        private void GoldenSpawntime()
        {
            GoldenSpawnInterval = random.Next(30, 60);
        }
        protected override void Initialize()
        {
            tileMap = new TileMap();
            th = new TextHandler();
            importer = new ContentImporter();
            test = Content.Load<Song>("test");
            _theBoot = new TheBoot();
            _theBoot.OnBootHit = TheBootHit;

            _goldencookie = new goldencookie();
            random = new Random();
            GoldenSpawntime();
            GoldenTimer = 0;


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
            _font = Content.Load<SpriteFont>("Phy");
            mess = Content.Load<Texture2D>("mess");
            end = Content.Load<Texture2D>("bigCookie1");
            tileMap.LoadContent(Content);
            _theCookie.LoadContent(Content);
            GameState GS = importer.Load();
            if (GS.Score != -1)
            {
                _theCookie.score = GS.Score;
                _theCookie.previousScore = GS.PreviousScore;
                _elapsedTime = GS.Time;
                GameStart = GS.Gamestart;
                if(_theCookie.score <= 0)
                {
                    superGameEnd=true;
                }
            }
            th.LoadContent(Content);    
            Cursor = Content.Load<Texture2D>("TheShoe");
            _goldencookie.LoadContent(Content);
            _theBoot.LoadContent(Content);
            _theShoe.LoadContent(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MediaPlayer.IsRepeating = true;
            if (GameStart == true)
            {
                Mouse.SetCursor(MouseCursor.FromTexture2D(Cursor, 0, 0));
                _theCookie.gamestart = true;
                if (MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.Play(test);
                    isVolumeIncreasing = true;
                }
            }
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
            if (_theCookie.score <= 0)
            {
                superGameEnd = true;
            }
            else
            {

           
            _elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            th.Update(gameTime);
            tileMap.Update(gameTime);


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
            // golden cookie spawn timer
            if(GameStart == true)
            {
                GoldenTimer += gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            if (GoldenTimer >= GoldenSpawnInterval && _goldencookie.isVisible == false && GameStart == true)
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
                        GoldenSpawntime();
                        GoldenTimer = 0;
                        _theBoot.onTrigger();
                    }

                }
            }
            #endregion

            _past = mouse;
            _theCookie.Update(gameTime);
            base.Update(gameTime);
            if (_elapsedTime < 10)
            {
                return;
            }

            if(GameStart != true)
            {
                _theShoe.Update(gameTime);
            }
            else
            {
                _theBoot.Update(gameTime);
            }
            }
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
            
            if (superGameEnd == false)
            {
                tileMap.Draw(gameTime, _spriteBatch);
                th.Draw(gameTime, _spriteBatch, _elapsedTime, _theCookie.score, GameStart);
                _theShoe.Draw(gameTime, _spriteBatch);
                _theCookie.Draw(gameTime, _spriteBatch);
                _theBoot.Draw(gameTime, _spriteBatch);
                _goldencookie.Draw(gameTime, _spriteBatch);

            }
            else if (superGameEnd == true)
            {
                GraphicsDevice.Clear(new Color(0, 0, 0));
                _crumble.Enabled = false;
                _crumble.Visible = false;

                _spriteBatch.DrawString(_font, "Look what you did to my kitchen floor >:(", new Vector2(160, 600), Color.White);
                _spriteBatch.Draw(mess, new Rectangle(0, 100, 800, 500), Color.White);
                _spriteBatch.DrawString(_font, "Thanks for kicking!", new Vector2(280, 630), Color.White);
                MediaPlayer.Stop();
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        protected override void OnExiting(object sender, EventArgs args)
        {
            GameState gs = new GameState(_theCookie.score, _theCookie.previousScore, _elapsedTime, GameStart);
            importer.Save(gs);
            base.OnExiting(sender, args);
        }
    }
}