using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class SwitchButton : Control
    {
        public bool IsOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                _isOn = value;
                if (value)
                {
                    _texture = Style.Action;
                    _color = Style.ActionColor;
                    _sourceRect = Style.ActionSourceRectangle;
                    _textColor = Style.ActionFontColor;  
                }
                else
                {
                    _texture = Style.Active;
                    _color = Style.ActiveColor;
                    _sourceRect = Style.ActiveSourceRectangle;
                    _textColor = Style.ActiveFontColor;
                }
            }
        }

        protected bool _isOn;

        public SwitchButton(Rectangle drawRect, GUIStyle style, string text, bool state) : base (drawRect, style)
        {          
            SetText(text);
            IsActive = true;
            IsOn = state;
        }

        public override void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton)
        {
            if (IsActive)
            {
                if (hoverRect.Intersects(DrawRect))
                {
                    isHovered = true;
                }
                else if (IsOn)
                {                    
                    isHovered = false;
                    IsOn = true;                    
                }
                else
                {
                    isHovered = false;
                }
            
                if(isHovered)
                {
                    if (aButton)
                    {
                        IsOn = !IsOn;                        
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
}