﻿using CollisionExample.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ParticleSystemExample;
using System;

public class TheShoe
{
    public BoundingRectangle Hitbox;

    Texture2D Shoe;
    Vector2 Position;
    float rotation;
    bool clicked;

    public void LoadContent(ContentManager content)
    {
        clicked = false;
        Random random = new Random();
        rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);
        Shoe = content.Load<Texture2D>("TheShoe");
        Position = new Vector2(RandomHelper.NextFloat(50, 750), -50);

        Hitbox = new BoundingRectangle(Position.X - 16, Position.Y - 16, 32, 32);
    }

    public void Update(GameTime gameTime)
    {
        if (Position.Y < 984)
        {
            Position.Y += 6;
            rotation += (float)(gameTime.ElapsedGameTime.TotalSeconds * 7);

            Hitbox.X = Position.X - 16;
            Hitbox.Y = Position.Y - 16;
        }
        else
        {
            rotation = 0;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        
        if (clicked == false)
        {
            spriteBatch.Draw(Shoe, Position, null, Color.White, rotation, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }
    }
    public void OnClick()
    {
        clicked = true;
    }
}
