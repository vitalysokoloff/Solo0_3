using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class TextBox : Control
    {
        protected Vector2 _margin;
        public TextBox(Rectangle drawRect, GUIStyle style, string text, Vector2 margin) : base (drawRect, style)
        {    
            _margin = margin;      
            SetText(text);
        }

        public override void SetText(string text)
        {
            _text = text;
            Vector2 size = Style.Font.MeasureString(_text);
            _textPosition = new Vector2(DrawRect.X + _margin.X, DrawRect.Y + _margin.Y);
        }
    }
}