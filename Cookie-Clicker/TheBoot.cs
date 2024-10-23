using System;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Cookie_Clicker
{
    public class TheBoot
    {
        public Action OnBootHit;

        public BoundingRectangle Hitbox;

        private Texture2D Boot;
        private Vector2 Position;
        private float speed;
        private float waitTimer;
        private enum BootState { Falling, Waiting, Rising }
        private BootState state;
        bool Begin = false;
        public void LoadContent(ContentManager content)
        {
            Random random = new Random();
            Boot = content.Load<Texture2D>("TheBoot");

            Position = new Vector2(400, -400);
            state = BootState.Falling;
            waitTimer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            if(Begin == true)
            {
                float timer = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (state == BootState.Falling)
                {
                    Position.Y += 20;
                    if (Position.Y >= 400)
                    {
                        Position.Y = 400;
                        state = BootState.Waiting;
                        waitTimer = 1.5f;
                        OnBootHit?.Invoke();
                    }
                }
                else if (state == BootState.Waiting)
                {
                    waitTimer -= timer;
                    if (waitTimer <= 0)
                    {
                        state = BootState.Rising;
                    }
                }
                else if (state == BootState.Rising)
                {
                    Position.Y -= 5;
                    if(Position.Y <= -400)
                    {
                        state = BootState.Falling;
                        Begin = false;
                    }
                }
            }
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(Begin == true)
            {
                spriteBatch.Draw(Boot, Position, null, Color.White, 0f, new Vector2(100, 100), 4f, SpriteEffects.None, 0f);
            }
            
        }
        public void onTrigger()
        {
            Begin = true;
        }
    }
}
