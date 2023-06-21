using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class Label : Control
    {
        public Label(Rectangle drawRect, GUIStyle style, string text) : base (drawRect, style)
        {          
            SetText(text);
        }        
    }
}