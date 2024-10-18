using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CollisionExample;
using CollisionExample.Collisions;
using System;

namespace Cookie_Clicker
{
    public class TheCookie
    {
        public Texture2D Cookie;
        public Vector2 Position;
        public Rectangle source;
        public float rotation;
        public Vector2 orgin;
        public BoundingCircle hitbox;
        public float score;
        private float scale = 1f;
        private bool isShrinking = false;


        public TheCookie()
        {
            // Constructor logic here, if needed
        }

        public void LoadContent(ContentManager content)
        {
            Cookie = content.Load<Texture2D>("TheCookie");
            Position = new Vector2(400, 500);
            source = new Rectangle();
            orgin = new Vector2(Cookie.Width / 2, Cookie.Height / 2);

            hitbox = new BoundingCircle(Position, (Cookie.Width * 1.75f) / 2f);
            score = 1; 
        }
        public void Update(GameTime gameTime)
        {
            rotation += (float)(gameTime.ElapsedGameTime.TotalSeconds * 1.1);

            if (isShrinking)
            {
                scale -= 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (scale <= 0.9f)
                {
                    scale = 0.9f;
                    isShrinking = false;
                }
            }
            else if (scale < 1f)
            {
                scale += 1.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (scale >= 1f)
                {
                    scale = 1f;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Cookie, Position, null, Color.White, rotation, orgin, scale * 1.75f, SpriteEffects.None, 0f);
        }


        public void OnClick()
        {
            isShrinking = true;
            score += 1; // Increment score when clicked
        }
    }
}
