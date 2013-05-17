using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DinoSteroidsContent
{
    public class Asteroid
    {
        public float Angle { get; private set; }
        public Vector2 Position { get; set; }
        public bool IsOnPlayerHeight { get; set; }
        public const int BaseScore = 10;
        public float Scale { get; set; }
        public Texture2D FlamingAsteroid { get; private set; }
        public Texture2D Explosion { get; set; }
        private Texture2D Pixel { get; set; }
        private bool drawExplosionAnimation = false;
        private int maxWidth, maxHeight;
        private Random random = new Random();
        public int Score { get;set;}
        public bool destroyed = false;

        public EventHandler Destroyed;

        public Rectangle Bounds { get { return new Rectangle((int)Position.X, (int)Position.Y, (int)(FlamingAsteroid.Width * Scale), (int)(FlamingAsteroid.Height * Scale)); } }

        public Asteroid(int maxWidth, int maxHeight, ContentManager content)
        {
            this.maxHeight = maxHeight;
            this.maxWidth = maxWidth;
            this.Position = GetRandomStartPosition();
            this.Angle = CalculateDirection();
            this.Scale = (float)random.NextDouble();
            FlamingAsteroid = content.Load<Texture2D>("Textures/asteroid on fire");
        }

        public Asteroid(int maxWidth, int maxHeight, ContentManager content, Texture2D pixel) : this(maxWidth, maxHeight, content)
        {
            Pixel = pixel;
        }

        private Vector2 GetRandomStartPosition()
        {
            int useXOrY = random.Next(2);
            int x = 0, y = 0;
            if(useXOrY > 0)
                x = random.Next(maxWidth);
            else
                y = random.Next(maxHeight);
            return new Vector2(x, y);
        }

        private float CalculateDirection()
        {
            float direction = 0.0f;
            double opp = Position.X - maxWidth / 2;
            double adj = Position.Y - maxHeight / 2;
            direction = (float)Math.Atan(opp / adj);
            return direction + (float)Math.PI - 0.5f ;
        }


        public void Update(GameTime gameTime)
        {
            var direction = new Vector2(maxWidth / 2 - Position.X, maxHeight /2 -Position.Y);
            this.Position = Position + direction * gameTime.ElapsedGameTime.Milliseconds * 0.00075f;
            if ((Math.Abs(maxWidth / 2 - Position.X) < 25) || (Math.Abs(maxHeight / 2 - Position.Y) < 25))
            {
                this.DestroyAndGetScore();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (drawExplosionAnimation)
            { }
            else
            {
                if (!destroyed)
                {
                    spriteBatch.Draw(FlamingAsteroid, Position, null, Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.5f);
                    //Helpers.DrawBorder(spriteBatch, Pixel, Bounds, 5, Color.Black);
                }
            }
        }

        protected void DestroyAndGetScore()
        {
            Score = (int)(BaseScore * Scale);
            if(!destroyed)
                OnDestroy(new EventArgs());
            destroyed = true;
        }

        protected virtual void OnDestroy(EventArgs e)
        {
            if (Destroyed != null)
                Destroyed(this, e);
        }
    }
}
