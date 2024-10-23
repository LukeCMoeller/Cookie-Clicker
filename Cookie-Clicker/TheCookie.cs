using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CollisionExample;
using CollisionExample.Collisions;
using System;
using Microsoft.Xna.Framework.Audio;

namespace Cookie_Clicker
{
    public class TheCookie
    {
        /// <summary>
        /// if the game has started or not past the inital waiting portion
        /// </summary>
        public bool gamestart = false;
        /// <summary>
        /// texture files of the cookie
        /// </summary>
        public Texture2D[] Cookie = new Texture2D[4];
        public Vector2 Position;
        public float rotation;
        public Vector2 orgin;
        public BoundingCircle hitbox;
        public float score;
        /// <summary>
        /// bothe scale and isShrinking are for adjusting the size if clicked
        /// </summary>
        private float scale = 1f;
        private bool isShrinking = false;
        /// <summary>
        /// sound effect
        /// </summary>
        SoundEffect CRUNCH;
        /// <summary>
        /// saves the previous score
        /// </summary>
        public float previousScore;
   
        int cookieIndex = 0;

        /// <summary>
        /// the boot just hit and your score gets halfved
        /// </summary>
        public void GotBooted()
        {
            score = (float)Math.Round((score * 0.75f), 3);
        }
        /// <summary>
        /// makes sure the right % of cracked cookie is here
        /// </summary>
        public void UpdateCookieIndex()
        {
            if (gamestart)
            {
                if (score < 0) score = 0;

                if (score >= previousScore * 0.75)
                {
                    cookieIndex = 0;
                }
                else if (score >= previousScore * 0.50f)
                {
                    cookieIndex = 1;
                }
                else if (score >= previousScore * 0.25f)
                {
                    cookieIndex = 2;
                }
                else
                {
                    cookieIndex = 3;
                }
            }
        }
        public void LoadContent(ContentManager content)
        {
            CRUNCH = content.Load<SoundEffect>("CRUNCH");
            Cookie[0] = content.Load<Texture2D>("bigCookie1");
            Cookie[1] = content.Load<Texture2D>("bigCookie2");
            Cookie[2] = content.Load<Texture2D>("bigCookie3");
            Cookie[3] = content.Load<Texture2D>("bigCookie4");
            Position = new Vector2(400, 500);
            orgin = new Vector2(Cookie[0].Width / 2, Cookie[0].Height / 2);
            hitbox = new BoundingCircle(Position, (Cookie[0].Width) / 2f);
            score = 1;
            previousScore = score;
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
            UpdateCookieIndex();
            spriteBatch.Draw(Cookie[cookieIndex], Position, null, Color.White, rotation, orgin, scale, SpriteEffects.None, 0f);
        }


        public void OnClick(bool gs) { 

         isShrinking = true;
            CRUNCH.Play();
            if (!gs)
            {
                score += 1;
                previousScore += 1;
            }
            else
            {
                score -= 0.005f;
                score = (float)Math.Round(score, 3);
            }
        }
    }
}
