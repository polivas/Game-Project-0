using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_Project_0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SpriteFont spriteFont;
        private int cansLeft;
        private int foodLeft;

        private PigeonSprite pigeon;
        private TrashSprite[] trashCans;
        private FoodSprite[] foodScraps;

        private SpriteFont bangers; //Love this font tbh


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
                new TrashSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new TrashSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new TrashSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height))
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

            // spriteFont = Content.Load<SpriteFont>("bangers");


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

            //Pigeon eats spilled food


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
          
            foreach (var can in trashCans)
            {
                can.Draw(gameTime, spriteBatch);

                var rect = new Rectangle((int)(can.Bounds.Center.X - can.Bounds.Radius),
                                         (int)(can.Bounds.Center.Y - can.Bounds.Radius),
                                         (int)can.Bounds.Radius, (int)can.Bounds.Radius);
                spriteBatch.Draw(ball, rect, Color.White);
            }

            pigeon.Draw(gameTime, spriteBatch);

            var rect2 = new Rectangle((int)(pigeon.Bounds.Center.X - pigeon.Bounds.Radius),
                         (int)(pigeon.Bounds.Center.Y - pigeon.Bounds.Radius),
                         (int)pigeon.Bounds.Radius, (int)pigeon.Bounds.Radius);

            spriteBatch.Draw(ball, rect2, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
