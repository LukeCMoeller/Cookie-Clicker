using CollisionExample.Collisions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cookie_Clicker
{
    public class TileMap
    {
        private Texture2D MapTexture;
        private int _tileWidth, _tileHeight, _mapWidth, _mapHeight;
        private Rectangle[] _tiles;
        private int[] _map;
        private float _scale = 2.0f;
        private Vector2 _scrollOffset = Vector2.Zero;
        private float _scrollSpeed = 50f;

        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText("Map.txt");
            var lines = data.Split('\n');

            // First line is tileset image file name 
            var tilesetFileName = lines[0].Trim();
            MapTexture = content.Load<Texture2D>(tilesetFileName);

            // Second line is tile size
            var secondLine = lines[1].Split(',');
            _tileWidth = int.Parse(secondLine[0]);
            _tileHeight = int.Parse(secondLine[1]);

            int tilesetColumns = MapTexture.Width / _tileWidth;
            int tilesetRows = MapTexture.Height / _tileWidth;
            _tiles = new Rectangle[tilesetColumns * tilesetRows];
            for (int y = 0; y < tilesetRows; y++)
            {
                for (int x = 0; x < tilesetColumns; x++)
                {
                    _tiles[y * tilesetColumns + x] = new Rectangle(
                        x * _tileWidth,
                        y * _tileHeight,
                        _tileWidth,
                        _tileHeight
                    );
                }
            }
            var thirdLine = lines[2].Split(',');
            _mapWidth = int.Parse(thirdLine[0]);
            _mapHeight = int.Parse(thirdLine[1]);

            _map = new int[_mapWidth * _mapHeight];
            var fourthLine = lines[3].Split(',');
            for (int i = 0; i < _mapWidth * _mapHeight; i++)
            {
                _map[i] = int.Parse(fourthLine[i]);
            }
        }
        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _scrollOffset.X += _scrollSpeed * deltaTime;

            // Wrap offset smoothly using modulo
            float totalWidth = _mapWidth * _tileWidth * _scale;
            _scrollOffset.X %= totalWidth;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float totalWidth = _mapWidth * _tileWidth * _scale;

            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    int index = _map[y * _mapWidth + x] - 1;

                    float posX = (x * _tileWidth * _scale) - _scrollOffset.X;
                    float posY = (y * _tileHeight * _scale) - _scrollOffset.Y;


                    spriteBatch.Draw(MapTexture, new Vector2(posX, posY), _tiles[index], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);

                    if (posX + _tileWidth * _scale < 0)
                    {
                        spriteBatch.Draw(MapTexture, new Vector2(posX + totalWidth, posY), _tiles[index], Color.White, 0f, Vector2.Zero, _scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

    }

}
