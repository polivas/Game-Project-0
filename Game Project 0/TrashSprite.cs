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
    public class TrashSprite
    {

        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        /// <summary>
        /// How many trash cants searched through
        /// </summary>
        public bool Emptied { get; set; } = false;

        /// <summary>
        /// Bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Creates a new coin sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        public TrashSprite(Vector2 position)
        {
            this.position = position;
            // this.bounds = new BoundingCircle(position + new Vector2(16, 16), 53, 32);//might be 53,32
            this.bounds = new BoundingCircle(position + new Vector2(53, 53), 32);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("trash_can");
        }

        /// <summary>
        /// Draw the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Emptied) return;

            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, position, Color.White);
        }


    }
}
