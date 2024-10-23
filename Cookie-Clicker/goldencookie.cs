using CollisionExample.Collisions;
using Cookie_Clicker;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

public class goldencookie
{
    public BoundingRectangle Hitbox;
    private Texture2D cookie;
    private Vector2 Position;
    public bool isVisible; 
    private Random random;

    public goldencookie()
    {
        random = new Random();
        isVisible = false; 
    }

    public void LoadContent(ContentManager content)
    {
        cookie = content.Load<Texture2D>("bigCookie1");
    }

    public void Spawn()
    {
        Position = new Vector2(random.Next(100, 700), random.Next(100, 900));
        Hitbox = new BoundingRectangle(Position.X, Position.Y, cookie.Width * 0.25f, cookie.Height * 0.25f);
        isVisible = true; 
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (isVisible)
        {
            spriteBatch.Draw(cookie, Position, null, Color.Yellow, 0f, new Vector2(25, 25), 0.25f, SpriteEffects.None, 0f);
        }
    }

    public void onClick()
    {
        isVisible = false;
    }
}
