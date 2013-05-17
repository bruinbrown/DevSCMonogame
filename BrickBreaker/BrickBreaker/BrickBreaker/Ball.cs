using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BrickBreaker
{
    public class Ball
    {
        Texture2D balltex;
        Vector2 position;
        public Vector2 motion;
        float speed = 2.5f;
        int maxWidth, maxHeight;

        public Rectangle Bounds { get { return new Rectangle((int)position.X, (int)position.Y, (int)balltex.Width, (int)balltex.Height); } }

        public Ball(ContentManager content, int maxWidth, int maxHeight)
        {
            balltex = content.Load<Texture2D>("ball");
            position = new Vector2(0, maxHeight / 2);
            motion = new Vector2(1, 1);
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
        }

        public void Update(GameTime gameTime)
        {
            if (position.X < 0)
                motion.X *= -1;
            if (position.Y < 0)
                motion.Y *= -1;
            if (position.X > (maxWidth - balltex.Width))
                motion.X *= -1;
            if (position.Y > (maxHeight - balltex.Height))
                motion.Y *= -1;

            position += (motion * speed);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(balltex, position, Color.White);
        }
    }
}
