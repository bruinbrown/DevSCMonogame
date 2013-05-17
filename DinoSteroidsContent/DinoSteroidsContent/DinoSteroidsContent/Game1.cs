using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace DinoSteroidsContent
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Asteroid> asteroids = new List<Asteroid>();
        private int score = 0;
        Dinosaur dino;
        SpriteFont scoreFont;
        Texture2D pixel;
        private bool IsGameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;
            graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var startAsteroid = new Asteroid(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, this.Content, pixel);
            startAsteroid.Destroyed += new EventHandler((sender, e) =>
            {
                score += (sender as Asteroid).Score;
                //asteroids.Remove((Asteroid)sender);
            });
            asteroids.Add(startAsteroid);
            dino = new Dinosaur(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, this.Content, pixel);
            dino.Killed += new EventHandler(HandleGameOver);
            scoreFont = Content.Load<SpriteFont>("Fonts/scorefont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleGameOver(object sender, EventArgs e)
        {
            IsGameOver = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (!IsGameOver)
            {
                foreach (var asteroid in asteroids)
                {
                    asteroid.Update(gameTime);
                    if (asteroid.Bounds.Intersects(dino.Bounds))
                    {
                        //Debug.WriteLine("He dead");
                        dino.Kill();
                    }
                }
                asteroids.RemoveAll(x => x.destroyed == true);
                while (asteroids.Count < 10)
                {
                    var asteroid = new Asteroid(this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, this.Content, pixel);
                    asteroid.Destroyed += new EventHandler((sender, e) => score += (sender as Asteroid).Score);
                    asteroids.Add(asteroid);
                }
                dino.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Bisque);
            spriteBatch.Begin();
            if (!IsGameOver)
            {
                foreach (var asteroid in asteroids)
                {
                    asteroid.Draw(spriteBatch);
                }
                dino.Draw(spriteBatch);
                spriteBatch.DrawString(scoreFont, score.ToString(), new Vector2(10, 10), Color.White);
            }
            else
            {
                spriteBatch.DrawString(scoreFont, "GAME OVER!", new Vector2(400, 400), Color.Black, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0.5f);
                spriteBatch.DrawString(scoreFont, String.Format("YOU SCORED {0} POINTS", this.score), new Vector2(350, 600), Color.Black);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
