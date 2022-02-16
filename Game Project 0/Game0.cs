using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Project_0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private double animationTimer;
        private short animationFrame = 0;


        private PigeonSprite pigeon;

        
        private int cansLeft = 3;
        private int foodLeft = 3;

        private TrashSprite[] trashCans;
        private FoodSprite[] foodScraps;

        private SpriteFont bangers;

        private Texture2D title;
        private Texture2D border;
        private Texture2D clouds;

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


            title = new Texture2D(graphics.GraphicsDevice, 331, 101);
            border = new Texture2D(graphics.GraphicsDevice, 250, 82);

            clouds = new Texture2D(graphics.GraphicsDevice, 96, 32);

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

            title = Content.Load<Texture2D>("TitleSceen");
            border = Content.Load<Texture2D>("Border");
            clouds = Content.Load<Texture2D>("CloudSprite");

            foreach (var can in trashCans) can.LoadContent(Content);

            foreach (var food in foodScraps) food.LoadContent(Content);



            bangers = Content.Load<SpriteFont>("bangers");

        }

        /// <summary>
        /// Updates the game contents
        /// </summary>
        /// <param name="gameTime">Total gametime</param>
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

        /// <summary>
        /// Draws the sprites onto the window
        /// </summary>
        /// <param name="gameTime">the measured game time</param>
        protected override void Draw(GameTime gameTime)
        {

            Vector2 pos = new Vector2((250), (50));
            Vector2 pos2 = new Vector2((300), (400));
            Vector2 pos3 = new Vector2((300), (300));

            GraphicsDevice.Clear(Color.LightSteelBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(title, pos , Color.White);

            //trashCans[0].Draw(gameTime, spriteBatch);
          //  if(trashCans[0].Emptied) foodScraps[0].Draw(gameTime, spriteBatch);


          //  trashCans[1].Draw(gameTime, spriteBatch);
          //  if (trashCans[1].Emptied) foodScraps[1].Draw(gameTime, spriteBatch);


           // trashCans[2].Draw(gameTime, spriteBatch);
           // if (trashCans[2].Emptied) foodScraps[2].Draw(gameTime, spriteBatch);

            pigeon.Draw(gameTime, spriteBatch);

            //CLOUD STUFF
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.3 )
            {
                animationFrame++;
                if (animationFrame > 2) animationFrame = 0;
                animationTimer -= 0.3;
            }
            if (animationTimer > 0.3) animationTimer -= 0.3;
            var source = new Rectangle(animationFrame * 32, 0 , 32, 32);
            spriteBatch.Draw(clouds, pos2 - new Vector2(50, 20), source, Color.White);
            spriteBatch.Draw(clouds, pos2 + new Vector2(220, 20), source, Color.White);
            
            spriteBatch.Draw(border, pos2 - new Vector2(20,20), Color.White);
            spriteBatch.DrawString(bangers, $"Press ESC to Exit", pos2, Color.Black);

            spriteBatch.Draw(border, pos3 - new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(bangers, $"Use Arrow Keys", pos3 + new Vector2(10, -5), Color.Black);
            spriteBatch.DrawString(bangers, $"to Walk Around", pos3 + new Vector2(0, 20), Color.Black);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
