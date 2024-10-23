using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollisionExample.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ParticleSystemExample;
using System.Windows.Forms;

namespace Cookie_Clicker
{
    public class Shoebox
    {
        private Texture2D theBox;
        private Vector2 Position;
        public Shoebox()
        {
            Position = new Vector2(50, 500);
        }
        public void LoadContent(ContentManager content)
        {
            theBox = content.Load<Texture2D>("bigCookie1");
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            
             spriteBatch.Draw(theBox, Position, null, Color.White, 0f, new Vector2(16, 16), 0.5f, SpriteEffects.None, 0f);
        }
    }
}
