using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Solo
{
    public class SMaterial
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public SoundEffect Sound { get; set; }

        public SMaterial()
        {

        } 

        public SMaterial(Texture2D texture, Rectangle sourceRectangle)
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
        }        
    }
}