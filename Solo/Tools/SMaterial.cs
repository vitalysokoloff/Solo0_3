using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo
{
    public class SMaterial
    {
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        // Может звук добавить

        public SMaterial(Texture2D texture, Rectangle sourceRectangle)
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
        }        
    }
}