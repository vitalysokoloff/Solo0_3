using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class SwitchButton : Control
    {
        public SwitchButton(Rectangle drawRect, GUIStyle style, string text, bool state) : base (drawRect, style)
        {          
            SetText(text);
            IsActive = state;
        }

        public override void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton)
        {
            
            if (hoverRect.Intersects(DrawRect))
            {
                isHovered = true;
            }
            else if (IsActive)
            {
                IsActive = true;
            }
            else
            {
                IsActive = false;
            }

            if (aButton)
            {
                IsActive = !IsActive;
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