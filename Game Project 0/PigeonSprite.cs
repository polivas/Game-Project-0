using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Game_Project_0.Collisions;

namespace Game_Project_0
{
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3,
    }

    public class PigeonSprite //150ms delay
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private Vector2 position = new Vector2(200, 200);

        private double animationTimer;
        private short animationFrame = 1;

        /// <summary>
        /// the direction of the pigeon
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// poition of the pigeon
        /// </summary>
        public Vector2 Position;


        /// <summary>
        /// Loads the pigeon texture
        /// </summary>
        /// <param name="content">The content manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pigeon-SWEN");
        }

        /// <summary>
        /// Updates the pigeon sprite to fly in a pattern
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1);
                Direction = Direction.Up;
            }

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 1);
                Direction = Direction.Down;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                Direction = Direction.Left;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                Direction = Direction.Right;
            }

        }


        /// <summary>
        /// Draws the animated sprite
        /// </summary>
        /// <param name="gametime">The game time</param>
        /// <param name="spriteBatch">The spritebatch </param>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            //Update animation Timer
            animationTimer += gametime.ElapsedGameTime.TotalSeconds;

            //Update animation frame
            if (animationTimer > 0.3)
            {
                animationFrame++;
                if (animationFrame > 3) animationFrame = 1;
                animationTimer -= 0.3;
            }


            //Draw the sprite
            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);

            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            spriteBatch.Draw(texture, Position, source, Color.White);
        }

    }
}
