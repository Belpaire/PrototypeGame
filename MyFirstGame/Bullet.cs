using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame
{
    public class Bullet : GameObject
    {

        public enum Affiliation
        {
            Enemy,
            Ally
        }
        private bool hasHit;
        public Affiliation BulletAffiliation { get; set; }

        public Bullet(Texture2D givenTexture, Vector2 vector, float theSpeed, Affiliation affiliation) : this(givenTexture, vector, theSpeed, affiliation, (int)GameWorld.GetWidthScreen/20, (int)GameWorld.GetHeightScreen / 20)
        {
            
        }

        public Bullet(Texture2D givenTexture, Vector2 vector, float theSpeed, Affiliation affiliation,int width, int height) : base(givenTexture, vector, theSpeed,width,height)
        {
            BulletAffiliation = affiliation;
            hasHit = false;
        }

        public override bool canRemove()
        {
            return Position.Y <= 0 || Position.Y + HitBox.Height >= GameWorld.GetHeightScreen || hasHit;
        }

        public override void DrawObject(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, HitBox, Color.White);
        }

        public override List<GameObject> UpdatePositionReturnNewObjects(GameTime gameTime)
        {
            var dy = -Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position = new Vector2(Position.X, Position.Y + dy);

            return new List<GameObject>();
        }

        public override void onCollision(GameObject other)
        {
            Player playership = other as Player;
            EnemyShip enemyship = other as EnemyShip;

            if (playership != null && this.BulletAffiliation==Affiliation.Enemy)
            {
                hasHit = true;
            }
            else if (enemyship != null && this.BulletAffiliation == Affiliation.Ally)
            {
                hasHit = true;
            }
        }
    }
}
