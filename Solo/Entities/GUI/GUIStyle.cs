using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities
{
    public class GUIStyle
    {       
        public Texture2D Active {get; set;}
        public Texture2D NonAvtive {get; set;}
        public Texture2D Hovered {get; set;}
        public Texture2D Action {get; set;}
        public Rectangle ActiveSourceRectangle {get; set;}
        public Rectangle NonActiveSourceRectangle {get; set;}
        public Rectangle HoveredSourceRectangle {get; set;}
        public Rectangle ActionSourceRectangle {get; set;}
        public Color ActiveFontColor {get; set;}
        public Color NonActiveFontColor {get; set;}
        public Color HoveredFontColor {get; set;}
        public Color ActionFontColor {get; set;}
        public Color ActiveColor {get; set;}
        public Color NonActiveColor {get; set;}
        public Color HoveredColor {get; set;}
        public Color ActionColor {get; set;}
        public SpriteFont Font {get; set;}

        protected GraphicsDeviceManager _graphics;        

        public GUIStyle(GraphicsDeviceManager graphics, SpriteFont font)
        {
            _graphics = graphics;
            Font = font;
            Texture2D texture = Tools.MakeSolidColorTexture(graphics, new Point(20, 20), Color.White);
            Active = texture;
            NonAvtive = texture;
            Hovered = texture;
            Action = texture;

            Rectangle rect = new Rectangle(0, 0, 10, 10);
            ActiveSourceRectangle = rect;
            NonActiveSourceRectangle = rect;
            HoveredSourceRectangle = rect;
            ActionSourceRectangle = rect;

            ActiveColor = Color.White;
            NonActiveColor = Color.Gray;
            HoveredColor = Color.Red;
            ActionColor = Color.Orange;

            ActiveFontColor = Color.Black;
            NonActiveFontColor = Color.LightGray;
            HoveredFontColor = Color.White;
            ActionFontColor = Color.White;            
        }
    }
}