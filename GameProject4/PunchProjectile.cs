using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject4.Collisions;

namespace GameProject4
{
    public enum state
    {
        traveling,
        connected
    }

    public class PunchProjectile
    {
        private Texture2D _texture;

        private Vector2 _position;

        private BoundingRectangle _bounds;

        private double _projTimer;

        private double _animationTimer;

        private short _animationFrame;

        public float Speed = 300;

        public state projState = state.traveling;

        public bool Flipped;

        public bool Expired = false;

        public Vector2 Position => _position;

        public BoundingRectangle Bounds => _bounds;

        public PunchProjectile(Vector2 pos)
        {
            _position = pos;
        }

        public void update(GameTime gameTime)
        {
            _position += new Vector2(Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            _bounds = new BoundingRectangle(new Vector2(_position.X - 32, _position.Y - 32), 20, 40);
            _projTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (_projTimer >= 2.5)
            {
                Destroy(this);
            }

        }

        public void Destroy(PunchProjectile p)
        {
            p._texture.Dispose();
            p._bounds = new BoundingRectangle(Vector2.Zero, 0, 0);
            Expired = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (Flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if(projState == state.traveling)
            {
                if (_projTimer < 2)
                {
                    _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (_animationTimer > 0.1)
                    {
                        _animationFrame++;
                        if (_animationFrame > 1) _animationFrame = 0;
                        _animationTimer -= 0.1;
                    }

                }
                
            }
        }
    }
}
