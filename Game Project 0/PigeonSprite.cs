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
        Right = 2,
        Up = 3,
        Left = 1,
    }

    public class PigeonSprite //150ms delay
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private BoundingCircle bounds; // = new BoundingRectangle(new Vector2(200 - 16, 200 - 16), 32, 32);

        private bool pressing = false;

        private double animationTimer;
        private short animationFrame = 0;

        /// <summary>
        /// the direction of the pigeon
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// poition of the pigeon
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Loads the pigeon texture
        /// </summary>
        /// <param name="content">The content manager to load with</param>

        public PigeonSprite(Vector2 position)
        {
            this.Position = position;
            this.bounds = new BoundingCircle(position + new Vector2(200 - 18, 200 - 18), 18);
        }


        /// <summary>
        /// Loads the texture file
        /// </summary>
        /// <param name="content">content that has file</param>
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
                Position += new Vector2(0, -1);
                Direction = Direction.Up;
                pressing = true;
            }

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, 1);
                Direction = Direction.Down;
                pressing = true;
            }

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0);
                Direction = Direction.Left;
                pressing = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                Position += new Vector2(1, 0);
                Direction = Direction.Right;
                pressing = true;
            }

            
            //Update the bounds
            bounds.Center.X = Position.X + 32;
            bounds.Center.Y = Position.Y + 32;

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
            if (animationTimer > 0.3 && pressing)
            {
                animationFrame++;
                if (animationFrame > 2) animationFrame = 0;
                animationTimer -= 0.3;
            }
            if (animationTimer > 0.3 && pressing == false) animationTimer -= 0.3;

            pressing = false;

            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);

            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            spriteBatch.Draw(texture, Position, source, Color.White);
        }

    }
}
