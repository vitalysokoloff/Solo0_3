using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class IconButton : Control
    {
        protected Rectangle _promptRect;
        protected Vector2 _charSize;
        public IconButton(Rectangle drawRect, GUIStyle style, Texture2D icon, string prompt) : base (drawRect, style)
        {          
            SetText(prompt);
        }

        public override void SetText(string text)
        {
            _text = text;
            _charSize = Style.Font.MeasureString("A");
            Vector2 size = Style.Font.MeasureString(_text);
            _promptRect = new Rectangle(0, 0, (int)(size.X + _charSize.X), (int)size.Y);
        }

        public override void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton)
        {
            if (IsActive)
            {
                if (hoverRect.Intersects(DrawRect))
                {
                    isHovered = true;
                    int delta = (int)_charSize.Y;
                    _promptRect.X = hoverRect.X - 5;
                    _promptRect.Y = hoverRect.Y - delta;
                    _textPosition = new Vector2(hoverRect.X, hoverRect.Y - delta);
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, DrawRect, _sourceRect, _color, 0, Vector2.Zero, SpriteEffects.None, 0.99f);            
            if (Icon != null)
                spriteBatch.Draw(Icon, DrawRect, IconSourceRect, _color, 0, Vector2.Zero, SpriteEffects.None, 0.99f);
            if (isHovered && _text != "")
            {
                spriteBatch.Draw(Style.Prompt, _promptRect, Style.PromptSourceRectangle, Style.PromptColor, 0, Vector2.Zero, SpriteEffects.None, 0.99f); 
                spriteBatch.DrawString(Style.Font, _text, _textPosition, Style.PromptFontColor);
            }
        }
    }
}