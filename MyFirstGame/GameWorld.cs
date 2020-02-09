using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame
{
    public class GameWorld
    {
        private List<GameObject> gameObjects;

        public static bool HasWon { get; set; }

        public static bool GameHasEnded { get; set; }
        public static float GetHeightScreen { get; private set; }
        private EnemyFactory factory;
        public static int Score { get; set; }
        public static float GetWidthScreen { get; private set; }
        SpriteBatch spriteBatch;
        /// <summary>
        /// Constructor for backend testing
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public GameWorld(float width, float height) : this(null, null)
        {
            GetHeightScreen = height;
            GetWidthScreen = width;
        }
        /// <summary>
        /// Default Contstructor
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="spriteBatch"></param>
        public GameWorld(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            GetHeightScreen = graphics.PreferredBackBufferHeight;
            GetWidthScreen = graphics.PreferredBackBufferWidth;
            GameObjects = new List<GameObject>();
            SpriteBatch = spriteBatch;
            factory = new EnemyFactory();
        }

        public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }

        public void addGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }
        public void DrawAllObjects()
        {
            SpriteBatch.Begin();
            if (!GameHasEnded)
            {
                
                spriteBatch.Draw(Game1.Textures["Background"], new Rectangle(0, 0, (int)GetWidthScreen, (int)GetHeightScreen), Color.White);
                SpriteBatch.DrawString(Game1.SpriteFont, "My Score is: " + Score.ToString(), new Vector2(50, 150), Color.White);
                foreach (GameObject gameObject in GameObjects)
                {
                    gameObject.DrawObject(SpriteBatch);
                }
            }
            else
            {
                if (HasWon)
                {
                    spriteBatch.DrawString(Game1.SpriteFont, "YOU WON", new Vector2(GetWidthScreen / 2, GetHeightScreen / 2), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(Game1.SpriteFont, "YOU LOST", new Vector2(GetWidthScreen / 2, GetHeightScreen / 2), Color.White);

                }
            }
            SpriteBatch.End();

        }

        public void UpdatePositionsObjects(GameTime gameTime)
        {
            if (!GameHasEnded)
            {
                Score += (int) gameTime.TotalGameTime.TotalSeconds*5;
                List<GameObject> createdObjects = new List<GameObject>();
                foreach (GameObject gameObject in GameObjects)
                {
                    createdObjects.AddRange(gameObject.UpdatePositionReturnNewObjects(gameTime));
                }
                foreach (GameObject gameObject1 in createdObjects)
                {
                    GameObjects.Add(gameObject1);

                }
                GameObjects.AddRange(factory.spawnEnemy(gameTime));
                checkCollisions();
                int i = 0;
                while (i < GameObjects.Count)
                {
                    if (GameObjects.ElementAt(i).canRemove())
                    {
                        GameObjects.RemoveAt(i);
                        i--;
                    }
                    i++;
                }
            }
        }


        public void checkCollisions()
        {
            int i = 0;
            while (i < GameObjects.Count - 1)
            {
                int j = i + 1;
                var objecti = GameObjects.ElementAt(i);
                while (j < GameObjects.Count)
                {
                    var objectj = GameObjects.ElementAt(j);
                    if (objecti.collides(objectj))
                    {
                        objecti.onCollision(objectj);
                        objectj.onCollision(objecti);
                    }
                    j++;
                }
                i++;
            }
        }

    }
}
