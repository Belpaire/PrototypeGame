using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame
{
    public abstract class GameObject
    {
        private Texture2D texture;
        private readonly int hitboxWidth;
        private readonly int hitboxHeight;
        private Rectangle hitBox;
        private Vector2 position;
        private float speed;
        public GameObject(Texture2D givenTexture, Vector2 vector, float theSpeed, int width, int height)
        {
            Texture = givenTexture;
            Position = vector;
            hitboxWidth = width;
            hitboxHeight = height;

            Speed = theSpeed;
        }

        public Texture2D Texture { get => texture; set => texture = value; }
        public Vector2 Position { get => position; set => position = BoundsChecker(value); }
        public float Speed { get => speed; set => speed = value; }
        public Rectangle HitBox { get => new Rectangle((int)position.X, (int)position.Y, hitboxWidth, hitboxHeight); }

        public abstract List<GameObject> UpdatePositionReturnNewObjects(GameTime gameTime);

        public abstract bool canRemove();

        public abstract void DrawObject(SpriteBatch spriteBatch);

        private Vector2 BoundsChecker(Vector2 pos)
        {
            float xpos;
            float ypos;
            if (pos.X < 0)
            {
                xpos = 0;
            }
            else if (pos.X + hitboxWidth > GameWorld.GetWidthScreen)
            {
                xpos = GameWorld.GetWidthScreen - hitboxWidth;
            }
            else
            {
                xpos = pos.X;
            }

            if (pos.Y < 0)
            {
                ypos = 0;
            }
            else if (pos.Y + hitboxHeight > GameWorld.GetHeightScreen)
            {
                ypos = GameWorld.GetHeightScreen - hitboxHeight;
            }
            else
            {
                ypos = pos.Y;
            }
            return new Vector2(xpos, ypos);
        }

        public bool collides(GameObject other)
        {
            return HitBox.Intersects(other.HitBox);
        }

        public abstract void onCollision(GameObject other);
    }
}
