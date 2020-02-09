using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace MyFirstGame
{
    public class EnemyFactory
    {
        public Random Seed { get; set; }
        public Random SpawnerSeed { get; set; }
        private double timeUntilNextSpawn;
        public float TimeSinceSpawned { get; set; }
        public EnemyFactory()
        {
            Seed = new Random(1000);
            SpawnerSeed = new Random(2000);
        }
        
        public Enemy createEnemyShip()
        {
            var xpos=Seed.Next(0, (int)GameWorld.GetWidthScreen*19/20);
            timeUntilNextSpawn=SpawnerSeed.NextDouble();
            return new EnemyShip(new Vector2(xpos,0),400f);
        }
        public List<GameObject> spawnEnemy(GameTime time)
        {
            var createdEnemyShip = new List<GameObject>();
            TimeSinceSpawned += (float)time.ElapsedGameTime.TotalSeconds;
            if (TimeSinceSpawned > timeUntilNextSpawn)
            {
                createdEnemyShip.Add(createEnemyShip());
                TimeSinceSpawned = 0;
            }
            return createdEnemyShip;
        }
    }
}
