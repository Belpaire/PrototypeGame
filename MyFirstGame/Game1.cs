using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Utils;
using System.Linq;
namespace MyFirstGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameWorld gameWorld;
        bool hasInitialized;

        private static Dictionary<string,Texture2D>textures;
        public static SpriteFont SpriteFont { get; private set; }
        public static Dictionary<string, Texture2D> Textures { get => textures; private set => textures = value; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight=1080;
            Textures = new Dictionary<string, Texture2D>();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var background=Content.Load<Texture2D>("space_img");
            var player=Content.Load<Texture2D>("spaceShip");
            var enemy=Content.Load<Texture2D>("Enemy_ship");
            var bullet = Content.Load<Texture2D>("lazer");
            var spriteFont = Content.Load<SpriteFont>("SpriteFontGame/GameSpriteFont");
            SpriteFont = spriteFont;
            Textures.Add("Player", player);
            Textures.Add("Enemy", enemy);
            Textures.Add("Background", background);
            Textures.Add("ball", bullet);
           


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (!hasInitialized)
            {
                hasInitialized = true;
                gameWorld = new GameWorld(graphics, spriteBatch);
                gameWorld.addGameObject(new Player(new Vector2(500, 500), 300f));
            }

            gameWorld.UpdatePositionsObjects(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            // TODO: Add your drawing code here
            
            //spriteBatch.Draw(spaceTexture,new Rectangle(0,0,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight), Color.White);
            gameWorld.DrawAllObjects();

           // if(isLazerTime)
            //{
              //  this.spriteBatch.Draw(lazerTexture, new Vector2(loc.X+10,loc.Y-400), Color.White);
                //spriteBatch.Draw(spaceTexture, bullet, Color.White);
           // }
    


            base.Draw(gameTime);
        }
    }
}
