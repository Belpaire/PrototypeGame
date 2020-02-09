using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame
{
    public abstract class Enemy : GameObject
    {
        public Enemy(Texture2D givenTexture, Vector2 vector, float theSpeed,int width, int height) : base(givenTexture, vector, theSpeed, width, height)
        {
        }
    }
}
