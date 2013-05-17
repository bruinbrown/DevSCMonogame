using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DinoSteroidsContent
{
    public class Dinosaur
    {
        public Vector2 Position { get; private set; }
        public int direction = 1;
        private float speed = 5.0f;
        public Texture2D DinosaurTex { get; private set; }
        public Texture2D Pixel { get; set; }
        private int maxWidth, maxHeight;
        private float scale = 0.3f;

        public EventHandler Killed;

        public Rectangle Bounds { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(DinosaurTex.Width * scale), (int)(DinosaurTex.Height * scale)); } }

        public Dinosaur(int maxWidth, int maxHeight, ContentManager content)
        {
            DinosaurTex = content.Load<Texture2D>("Textures/dinosaur");
            this.maxWidth = maxWidth;
            this.maxHeight = maxHeight;
            this.Position = new Vector2(maxWidth / 2, maxHeight / 2);
        }

        public Dinosaur(int maxWidth, int maxHeight, ContentManager content, Texture2D pixel)
            : this(maxWidth, maxHeight, content)
        {
            Pixel = pixel;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 delta = new Vector2();
            var kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.W))
                delta.Y -= 1;
            if (kb.IsKeyDown(Keys.S))
                delta.Y += 1;
            if (kb.IsKeyDown(Keys.A))
                delta.X -= 1;
            if (kb.IsKeyDown(Keys.D))
                delta.X += 1;
            delta *= speed;
            Position += delta;
            if (Position.X < 0)
                Position = new Vector2(0.0f, Position.Y);
            if (Position.X > maxWidth - Bounds.Width)
                Position = new Vector2(maxWidth - Bounds.Width, Position.Y);
            if (Position.Y < 0)
                Position = new Vector2(Position.X, 0.0f);
            if (Position.Y > maxHeight - Bounds.Height)
                Position = new Vector2(Position.X, maxHeight - Bounds.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DinosaurTex, Position, null, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 0.5f);
            //Helpers.DrawBorder(spriteBatch, Pixel, Bounds, 5, Color.Black);
        }

        public void Kill()
        {
            OnKilled(new EventArgs());
        }

        protected void OnKilled(EventArgs e)
        {
            if (Killed != null)
            {
                Killed(this, e);
            }
        }
    }
}
