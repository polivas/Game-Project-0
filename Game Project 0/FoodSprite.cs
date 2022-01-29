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
    public class FoodSprite
    {


        private Vector2 position;


        private BoundingCircle bounds;

        /// <summary>
        /// If the trash was eaten
        /// </summary>
        public bool Eaten { get; set; }

        /// <summary>
        /// Bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// Helps load each texture in a different location
        /// </summary>
        public Texture2D[] _Texture = new Texture2D[3];

        /// <summary>
        /// Constructs bounding circle for the new bundle of food scraps 
        /// </summary>
        /// <param name="position">position of trashcan to place the food</param>
        public FoodSprite(Vector2 position)
        {
            
            this.position = position;
            this.bounds = new BoundingCircle(position + new Vector2(22, 32), 10);

        }

        /// <summary>
        /// Loads the food sprite textures using the provided contentmanager
        /// </summary>
        /// <param name="content">content manager</param>
        public void LoadContent(ContentManager content)
        {
            _Texture[0] = content.Load<Texture2D>("burger");
            _Texture[1] = content.Load<Texture2D>("pizza-1");
            _Texture[2] = content.Load<Texture2D>("Pataepollo");
        }



        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (Eaten) return;

            if (_Texture is null) throw new InvalidOperationException("Texture must be loaded to render");

            spriteBatch.Draw(_Texture[0], new Vector2(position.X -10, position.Y+10), Color.White);
            spriteBatch.Draw(_Texture[1], new Vector2(position.X + 10, position.Y + 10), Color.White);
            spriteBatch.Draw(_Texture[2], new Vector2(position.X - 10, position.Y + 10), Color.White);

        }
    }
}
