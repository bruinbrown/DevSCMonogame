using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BrickBreaker
{
    public class Brick
    {
        Texture2D bricktex;
        Vector2 position;
        public bool IsActive { get; set; }

        public Rectangle Bounds{get { return new Rectangle((int)position.X, (int)position.Y, (int)bricktex.Width, (int)bricktex.Height);}}

        public Brick(ContentManager content, int positionX, int positionY)
        {
            bricktex = content.Load<Texture2D>("brick");
            IsActive = true;
            position = new Vector2(positionX, positionY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsActive)
                spriteBatch.Draw(bricktex, position, Color.White);
        }

        public void CheckForDestroy(Ball ball)
        {
            if (IsActive)
            {
                if (ball.Bounds.Intersects(this.Bounds))
                {
                    IsActive = false;
                    ball.motion.Y *= -1;
                }
            }
        }
    }
}
