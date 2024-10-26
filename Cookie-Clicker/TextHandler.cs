using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using ParticleSystemExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Cookie_Clicker
{
    public class TextHandler
    {
        private SpriteFont _font;
        private SpriteFont itsBroken;
        private double Timer;
        public TextHandler()
        {
        }

        public void LoadContent(ContentManager content)
        {
            itsBroken = content.Load<SpriteFont>("Phy");
            _font = content.Load<SpriteFont>("Arcade");
        }

        public void Update(GameTime gameTime)
        {
            
        }
        /// <summary>
        /// had chat gpt help me. i needed to render specifcally the period. had it write the parsing part to get the . to load where then i just manually adjusted the + and - till itl ooked nice
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="_spriteBatch"></param>
        /// <param name="_elapsedTime"></param>
        /// <param name="Score"></param>
        /// <param name="Gamestart"></param>
        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch, double _elapsedTime, double Score, bool Gamestart)
        {
            _spriteBatch.DrawString(_font, "Cookie", new Vector2(320, 50), Color.White);

            if (!Gamestart)
            {
                _spriteBatch.DrawString(_font, "Clicker", new Vector2(420, 50), Color.White);
                _spriteBatch.DrawString(_font, "Score  " + Score.ToString(), new Vector2(10, 10), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(_font, "Kicker", new Vector2(420, 50), Color.White);

                // Split the score into integer and decimal parts
                string scoreString = Score.ToString("F3"); // Format to 3 decimal places
                string[] scoreParts = scoreString.Split('.'); // Split into integer and decimal parts

                // Render the integer part
                Vector2 scorePosition = new Vector2(10, 10); // Start position for the score
                _spriteBatch.DrawString(_font, "Score  " + scoreParts[0], scorePosition, Color.White);

                // Calculate position for the period
                Vector2 periodPosition = new Vector2(scorePosition.X + _font.MeasureString("Score " + scoreParts[0]).X + 4, scorePosition.Y - 4);

                // Draw the period using the itsBroken font
                _spriteBatch.DrawString(itsBroken, ".", periodPosition, Color.White);

                // Calculate position for the decimal part
                Vector2 decimalPosition = new Vector2(periodPosition.X + _font.MeasureString(".").X * 0.8f, scorePosition.Y); // Adjust this multiplier for tighter spacing

                // Render the decimal part
                _spriteBatch.DrawString(_font, scoreParts[1], decimalPosition, Color.White);
            }
            _spriteBatch.DrawString(_font, $"Time  {(int)_elapsedTime}", new Vector2(10, 50), Color.White);
        }
    }
}
