﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject4.Collisions;

namespace GameProject4
{
    /// <summary>
    /// States the main character will be in
    /// </summary>
    public enum Action
    {
        Idle = 0,
        Running = 1,
        Jumping = 2,
        Attacking = 3,
    }


    /// <summary>
    /// Class for the main character sprite
    /// </summary>
    public class mcSprite
    {
        #region privateFields
        private Texture2D _texture;

        private Vector2 _position = new Vector2();


        private KeyboardState currentKeyboardState;
        private KeyboardState priorKeyboardState;


        private BoundingRectangle _bounds;

        private float _velocityY = 0;

        private float _gravity;

        private float _jumpHeight;

        private double _animationTimer;

        private double _attackingTimer = 0;

        private short _animationFrame;

        private bool _flipped;

        public bool offGround = false;

        private bool _attacked = false;

        private Vector2 direction;
        #endregion

        #region publicFields
        public int coinsCollected;

        public Action action;


        public Vector2 Position => _position;

        /// <summary>
        /// Boundaries for the bounding rectangle of the sprite
        /// </summary>
        public BoundingRectangle Bounds => _bounds;
        #endregion

        #region publicMethods

        public mcSprite(Vector2 pos)
        {
            _position = pos;
            _bounds = new BoundingRectangle(new Vector2(_position.X - 32, _position.Y - 32), 48, 130);
        }

        /// <summary>
        /// Loads the Main character sprite
        /// </summary>
        /// <param name="content">ContentManager</param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprite_MC");


        }


        /// <summary>
        /// Updates the Main character
        /// </summary>
        /// <param name="gameTime">The real time elapsed in the game</param>
        public void Update(GameTime gameTime)
        {
            _jumpHeight = 150;
            _gravity = 10;
            direction = new Vector2(200 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);


            priorKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            //if (_position.Y < 300)
            //{
            //    offGround = true;
            //}
            //if (_position.Y >= 300)
            //{
            //    _position.Y = 300;
            //    offGround = false;
            //}



            for (int i = 0; i < coinsCollected; i++)
            {
                direction += new Vector2(0.75f, 0);
                if (direction.X > 300) direction.X = 300;
            }
            if (currentKeyboardState.IsKeyDown(Keys.A) ||
                currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _position += -direction;
                action = Action.Running;
                _flipped = true;
            }
            if (currentKeyboardState.IsKeyDown(Keys.D) ||
                currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _position += direction;
                action = Action.Running;
                _flipped = false;
            }
            if (!(currentKeyboardState.IsKeyDown(Keys.A) ||
                currentKeyboardState.IsKeyDown(Keys.Left)) &&
                !(currentKeyboardState.IsKeyDown(Keys.D) ||
                currentKeyboardState.IsKeyDown(Keys.Right))
                )
            {
                action = Action.Idle;
            }


            //Gravity Function
            if (offGround)
            {
                action = Action.Jumping;
                _velocityY += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                _position.Y += _velocityY;
                //_position.Y += gravity;

            }
            //Jump Function
            if (currentKeyboardState.IsKeyDown(Keys.Space) && !offGround)
            {
                //_offGround = true;
                _velocityY -= _jumpHeight;
                if (!_attacked)_animationFrame = 0;
                if (!_attacked) _animationTimer = 0;


            }
            _position.Y += _velocityY;

            if (!offGround)
            {
                _velocityY = 0;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && !_attacked)
            {
                _attacked = true;
                _animationFrame = 0;
                _animationTimer = 0;

                
            }

            if (_attacked)
            {
                action = Action.Attacking;
                _attackingTimer += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (_attackingTimer > 0.4)
            {
                _attacked = false;
                _attackingTimer = 0;
            }


            if (_position.X < 0) _position.X = 0;
            if (_position.X > 1150) _position.X = 1150;

            _bounds.X = _position.X;
            _bounds.Y = _position.Y;
        }

        /// <summary>
        /// Draws the main character
        /// </summary>
        /// <param name="gameTime">The real time elapsed in the game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            SpriteEffects spriteEffects = (_flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (_attacked)
            {
                
                _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (_animationTimer > 0.1)
                {
                    _animationFrame++;
                    if (_animationFrame > 3) _animationFrame = 3;
                    _animationTimer -= 0.1;
                }
               
            }
            else if (offGround)
            {

                _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (_animationTimer > 0.1)
                {
                    _animationFrame++;

                    if (_animationFrame > 3) _animationFrame = 3;
                    _animationTimer -= 0.1;
                }


            }
            //Update animationFrame
            else
            {
                _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

                if (_animationTimer > 0.2)
                {
                    _animationFrame++;
                    if (_animationFrame > 3)
                    {
                        _animationFrame = 0;

                    }
                    _animationTimer -= 0.2;
                }
            }

            var source = new Rectangle(_animationFrame * 250, (int)action * 512, 268, 512);
            spriteBatch.Draw(_texture, _position, source, Color.White, 0f, new Vector2(80, 120), 0.5f, spriteEffects, 0);


        }
        #endregion
    }
}
