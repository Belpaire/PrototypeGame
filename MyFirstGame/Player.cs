using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;
using System.Threading.Tasks;

namespace MyFirstGame
{
    /// <summary>
    /// The Player Ship in the game
    /// </summary>
    public class Player : GameObject
    {

        private int health;

        public Player( Vector2 vector, float speed) :this(Game1.Textures["Player"],vector,speed,(int)GameWorld.GetWidthScreen/10,(int)GameWorld.GetHeightScreen/10)
        {
            
        }

        public Player(Texture2D texture, Vector2 vector,float speed,int width,int heigth): base(texture,vector,speed,width, heigth)
        {
            Health = 5;
        }

        public int Health { get => health; set => health = value; }

        public override bool canRemove()
        {
            return false;
        }

        public override void DrawObject(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.SpriteFont, "MyHealth: " + Health.ToString(), new Vector2(50, 50), Color.White);
            spriteBatch.Draw(Texture, HitBox, Color.White);
            
        }

        public override void onCollision(GameObject other)
        {
            switch (other)
            {
                case Bullet bullet:
                    {
                        if (bullet.BulletAffiliation == Bullet.Affiliation.Enemy)
                        {
                            Health -= 1;
                        }
                        return;
                    }
                case Enemy enemy:
                    {
                        Health -= 1;
                        return;
                    }
                default:
                    return;

            }
        }

        private float lastFired;
        //Logic for moving
        public override List<GameObject> UpdatePositionReturnNewObjects(GameTime gameTime)
        {
            var createdBullets = new List<GameObject>();
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                var dy = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position = new Vector2(Position.X, Position.Y + dy);
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                var dy = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position = new Vector2(Position.X, Position.Y + dy);

            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                var dx = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position = new Vector2(Position.X+dx, Position.Y);

            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                var dx = Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position = new Vector2(Position.X + dx, Position.Y);

            }
            lastFired += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.Space)) //&& lastFired>=1)
            {
                lastFired = 0;
                createdBullets.Add(new Bullet(Game1.Textures["ball"],new Vector2(Position.X+HitBox.Width/4,Position.Y-HitBox.Height/3),600f, Bullet.Affiliation.Ally));
            }
            if (Health<=0)
            {
                GameWorld.HasWon = false;
                GameWorld.GameHasEnded = true;
            }
            return createdBullets;
        }
    }
}
