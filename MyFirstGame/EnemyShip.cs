using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace MyFirstGame
{
    class EnemyShip : Enemy
    {
        private float timeSinceLastFired;

        private bool moveLeft = new Random().NextDouble() >= 0.5;
        private int health;
        public EnemyShip(Vector2 vector, float speed) : this(Game1.Textures["Enemy"], vector, speed, (int)GameWorld.GetWidthScreen / 20, (int)GameWorld.GetHeightScreen / 20)
        {

        }
        public EnemyShip(Texture2D givenTexture, Vector2 vector, float theSpeed, int width, int heigth) : base(givenTexture, vector, theSpeed, width, heigth)
        {
            Health = 1;
            timeSinceLastFired += (float)new Random().NextDouble();
        }

        public int Health { get => health; set => health = value; }

        public override bool canRemove()
        {
            return Health <= 0 || Position.Y >= GameWorld.GetHeightScreen - HitBox.Height;
        }

        public override void DrawObject(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, HitBox, Color.White);
        }

        public override void onCollision(GameObject other)
        {
            switch (other)
            {
                case Bullet bullet:
                    {
                        if (bullet.BulletAffiliation == Bullet.Affiliation.Ally)
                        {
                            Health -= 1;
                        }
                        return;
                    }
                case Player player:
                    {
                        Health -= 1;
                        return;
                    }
                default:
                    return;

            }
        }

        public override List<GameObject> UpdatePositionReturnNewObjects(GameTime gameTime)
        {
            var createdObjects = new List<GameObject>();
            timeSinceLastFired += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFired > 1)
            {
                timeSinceLastFired = 0;
                createdObjects.Add(new Bullet(Game1.Textures["ball"], Position, -600f, Bullet.Affiliation.Enemy));
            }

            float dx;
            float dy = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (moveLeft)
            {
                dx = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            else
            {
                dx = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            Position = new Vector2(Position.X + dx, Position.Y + dy);
            if (Position.X <= 0)
            {
                moveLeft = false;
            }
            if (Position.X + HitBox.Width >= GameWorld.GetWidthScreen)
            {
                moveLeft = true;
            }

            return createdObjects;
        }
    }
}
