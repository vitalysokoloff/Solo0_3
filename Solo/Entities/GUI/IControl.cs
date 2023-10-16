using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public interface IControl : IEntity
    {
        public GUIStyle Style {get; set;}
        public Texture2D Icon {get; set;}
        public Rectangle IconSourceRect {get; set;}
        public bool IsActive {get; set;}
        public Rectangle DrawRect {get; set;} 
        public Point Position {get; set;}
        public void Shift(Point offset);
        public void SetText(string text);
        public string GetText();
        public void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton);
    }
}