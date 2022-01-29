using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Project_0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;


        private PigeonSprite pigeon;

        
        private int cansLeft = 3;
        private int foodLeft = 3;

        private TrashSprite[] trashCans;
        private FoodSprite[] foodScraps;

        private SpriteFont bangers;


        private Texture2D ball;


        /// <summary>
        /// Constructs the game
        /// </summary>
        public Game0()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            System.Random rand = new System.Random();

            pigeon = new PigeonSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height));

            trashCans = new TrashSprite[]
             {
                new TrashSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width -50 , (float)rand.NextDouble() * GraphicsDevice.Viewport.Height-50)),
                new TrashSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width -50, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height-50)),
                new TrashSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width -50, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height-50))
             };



            foodScraps = new FoodSprite[]
            {
                new FoodSprite(trashCans[0].Position),
                new FoodSprite(trashCans[1].Position),
                new FoodSprite(trashCans[2].Position)
            };

            base.Initialize();
        }

        /// <summary>
        /// Loads content for the game
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pigeon.LoadContent(Content);

            foreach (var can in trashCans) can.LoadContent(Content);

            foreach (var food in foodScraps) food.LoadContent(Content);



            bangers = Content.Load<SpriteFont>("bangers");

            ball = Content.Load<Texture2D>("ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            pigeon.Update(gameTime);

            //Pigeon gets to trashcan
            foreach (var can in trashCans)
            {
                if (!can.Emptied && can.Bounds.CollidesWith(pigeon.Bounds))
                {
                    can.Emptied = true;
                    cansLeft--;

                }
            }

            //Pigeon eats food
            foreach (var food in foodScraps)
            {
                if (!food.Eaten && food.Bounds.CollidesWith(pigeon.Bounds))
                {
                    food.Eaten = true;
                    foodLeft--;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.DarkSlateGray);

            spriteBatch.Begin();
/*          
            foreach (var can in trashCans)
            {
                can.Draw(gameTime, spriteBatch);

                if (can.Emptied)
                {
                    foodScraps[].Draw(gameTime, spriteBatch);
                }

               var rect = new Rectangle((int)(can.Bounds.Center.X - can.Bounds.Radius),
                                         (int)(can.Bounds.Center.Y - can.Bounds.Radius),
                                         (int)can.Bounds.Radius, (int)can.Bounds.Radius);
                spriteBatch.Draw(ball, rect, Color.White);
            }*/

            trashCans[0].Draw(gameTime, spriteBatch);
            if(trashCans[0].Emptied) foodScraps[0].Draw(gameTime, spriteBatch);


            trashCans[1].Draw(gameTime, spriteBatch);
            if (trashCans[1].Emptied) foodScraps[1].Draw(gameTime, spriteBatch);


            trashCans[2].Draw(gameTime, spriteBatch);
            if (trashCans[2].Emptied) foodScraps[2].Draw(gameTime, spriteBatch);

            //  foreach (var food in foodScraps) food.Draw(gameTime, spriteBatch,);

            pigeon.Draw(gameTime, spriteBatch);


            if (cansLeft > 0)
            {

            }
            else if(cansLeft <= 0 && foodLeft !=0)
            {

            }
            else
            {
              //  spriteBatch.DrawString(bangers, $"All trash has been eatten!", new Vector2(100,100), Color.Gold);
              //  spriteBatch.DrawString(bangers, $"Time : {gameTime.TotalGameTime:c}", new Vector2(100, 200), Color.Gold);
            }


            spriteBatch.DrawString(bangers, $"{gameTime.TotalGameTime:c}", new Vector2(2, 2), Color.Gold);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
