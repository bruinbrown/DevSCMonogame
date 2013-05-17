using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DinoSteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont scoreFont;
        Dinosaur dino;
        private bool IsGameOver = false;
        private int score;
        List<Asteroid> asteroids = new List<Asteroid>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager( this );

            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
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
            spriteBatch = new SpriteBatch( GraphicsDevice );

            var startAsteroid = new Asteroid( this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, this.Content );
            startAsteroid.Destroyed += new EventHandler( ( sender, e ) =>
            {
                score += ( sender as Asteroid ).Score;
            } );
            asteroids.Add( startAsteroid );

            dino = new Dinosaur( graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Content );
            dino.Killed += new EventHandler( HandleGameOver );

            scoreFont = Content.Load<SpriteFont>( "Fonts/scorefont" );

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>( "spriteFont1" );
        }

        private void HandleGameOver( object sender, EventArgs e )
        {
            this.IsGameOver = true;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
            {
                Exit();
            }

            if ( !IsGameOver )
            {
                foreach ( var asteroid in asteroids )
                {
                    asteroid.Update( gameTime );
                    if ( asteroid.Bounds.Intersects( dino.Bounds ) )
                    {
                        dino.Kill();
                    }
                }
                asteroids.RemoveAll( x => x.destroyed == true );
                while ( asteroids.Count < 10 )
                {
                    var asteroid = new Asteroid( this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight, this.Content );
                    asteroid.Destroyed += new EventHandler( ( sender, e ) => score += ( sender as Asteroid ).Score );
                    asteroids.Add( asteroid );
                }
                dino.Update( gameTime );
            }

            // TODO: Add your update logic here

            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            graphics.GraphicsDevice.Clear( Color.Bisque );

            spriteBatch.Begin();
            if ( !IsGameOver )
            {
                foreach ( var asteroid in asteroids )
                {
                    asteroid.Draw( spriteBatch );
                }
                dino.Draw( spriteBatch );
                spriteBatch.DrawString( scoreFont, score.ToString(), new Vector2( 10, 10 ), Color.White );
            }
            else
            {
                spriteBatch.DrawString( scoreFont, "GAME OVER!", new Vector2( 10, 10 ), Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.5f );
                spriteBatch.DrawString( scoreFont, String.Format( "YOU SCORED {0} POINTS", this.score ), new Vector2(10,200 ), Color.White );
            }
            spriteBatch.End();

            base.Draw( gameTime );
        }
    }
}
