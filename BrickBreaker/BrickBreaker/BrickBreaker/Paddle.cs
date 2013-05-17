using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker
{
    class Paddle
    {
        Texture2D paddleTex;
        Vector2 position;
        Vector2 motion;
        float speed = 2.5f;
        int maxWidth, maxHeight;

        public Rectangle Bounds { get { return new Rectangle((int)position.X, (int)position.Y, (int)paddleTex.Width, (int)paddleTex.Height); } }

        public Paddle(ContentManager content, int maxWidth, int maxHeight)
        {
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            paddleTex = content.Load<Texture2D>("paddle");
            position = new Vector2(maxWidth/4, maxHeight-paddleTex.Height);
        }

        public void Update(GameTime gameTime)
        {
            motion = new Vector2();
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Left))
            {
                motion.X -= 1.0f;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                motion.X += 1.0f;
            }

            position += motion * speed;

            if (position.X < 0)
                position.X = 0;

            if (position.X > maxWidth - paddleTex.Width)
                position.X = maxWidth - paddleTex.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTex, position, Color.White);
        }
    }
}
