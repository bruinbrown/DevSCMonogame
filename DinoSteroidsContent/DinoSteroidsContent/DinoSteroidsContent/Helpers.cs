using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinoSteroidsContent
{
    public class Helpers
    {
        internal static void DrawBorder(SpriteBatch spriteBatch, Texture2D Pixel, Rectangle rectangle, int width, Color color)
        {
            spriteBatch.Draw(Pixel, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, width), color);

            spriteBatch.Draw(Pixel, new Rectangle(rectangle.X, rectangle.Y, width, rectangle.Height), color);

            spriteBatch.Draw(Pixel, new Rectangle((rectangle.X + rectangle.Width - width),
                                                  rectangle.Y,
                                                  width,
                                                  rectangle.Height), color);

            spriteBatch.Draw(Pixel, new Rectangle(rectangle.X,
                                                  rectangle.Y + rectangle.Height - width,
                                                  rectangle.Width,
                                                  width), color);
        }
    }
}
