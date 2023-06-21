using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class Button : Control
    {
        public Button(Rectangle drawRect, GUIStyle style, string text) : base (drawRect, style)
        {          
            SetText(text);
        }

        public override void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton)
        {
            if (IsActive)
            {
                if (hoverRect.Intersects(DrawRect))
                {
                   isHovered = true;
                }
                else
                {
                    isHovered = false;
                }

                if (aButton)
                {
                    _texture = Style.Action;
                    _color = Style.ActionColor;
                    _sourceRect = Style.ActionSourceRectangle;
                    _textColor = Style.ActionFontColor;
                    AButtonAction?.Invoke();
                }
                if (bButton)
                {
                    BButtonAction?.Invoke();
                }
                if (cButton)
                {
                    CButtonAction?.Invoke();
                }
            }
        }
    }
}