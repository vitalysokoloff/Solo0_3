using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities  
{
    public class Control : IControl
    {
        /// <summary>
        /// По умолчанию левая кнопка мыши. Но можно переопределить ГУИ и передавать
        /// кнопки из объявленного кейсинпут. 
        /// </summary>
        public ControlAction AButtonAction;
        /// <summary>
        /// По умолчанию правая кнопка мыши. Но можно переопределить ГУИ и передавать
        /// кнопки из объявленного кейсинукт. 
        /// </summary>
        public ControlAction BButtonAction;
        /// <summary>
        /// По умолчанию центральная (колесо) кнопка мыши. Но можно переопределить ГУИ и передавать
        /// кнопки из объявленного кейсинпут. 
        /// </summary>
        public ControlAction CButtonAction;
        public GUIStyle Style {get; set;}
        public Texture2D Icon {get; set;}
        public Rectangle IconSourceRect {get; set;}
        public bool isHovered
        {
            get
            {
                return _isHovered;
            }
            set
            {
                if (value)
                {
                    _texture = Style.Hovered;
                    _color = Style.HoveredColor;
                    _sourceRect = Style.HoveredSourceRectangle;
                    _textColor = Style.HoveredFontColor;                    
                }
                else
                {
                    _texture = Style.Active;
                    _color = Style.ActiveColor;
                    _sourceRect = Style.ActiveSourceRectangle;
                    _textColor = Style.ActiveFontColor;  
                }
                _isHovered = value;
            }
        }
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                if (value)
                {
                    _texture = Style.Active;
                    _color = Style.ActiveColor;
                    _sourceRect = Style.ActiveSourceRectangle;
                    _textColor = Style.ActiveFontColor;  
                }
                else
                {
                    _texture = Style.NonAvtive;
                    _color = Style.NonActiveColor;
                    _sourceRect = Style.NonActiveSourceRectangle;
                    _textColor = Style.NonActiveFontColor;
                }
            }
        }
        public Rectangle DrawRect 
        {
            get
            {
                return _drawRect;
            }
            set
            {
                _drawRect = value;
            }
        }
        public Point Position
        {
            get
            {
                return new Point(_drawRect.X, _drawRect.Y);
            }
            set
            {
                _drawRect.X = value.X;
                _drawRect.Y = value.Y;
            }
        }

        protected Texture2D _texture;
        protected Color _color;
        protected Color _textColor;
        protected Rectangle _sourceRect;
        protected bool _isHovered;
        protected bool _isActive;
        protected Vector2 _textPosition;
        protected string _text;
        protected Rectangle _drawRect;

        public Control(Rectangle drawRect, GUIStyle style)
        {
            DrawRect = drawRect;
            Style = style;
            IsActive = true;
            SetText("");            
        } 
        
        public virtual void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton){}

        public virtual void SetText(string text)
        {
            _text = text;
            Vector2 size = Style.Font.MeasureString(_text);
            _textPosition = new Vector2(DrawRect.X + (DrawRect.Width - size.X) / 2, DrawRect.Y + (DrawRect.Height - size.Y )/ 2);
        } 

        public virtual string GetText()
        {
            return _text;
        }       

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Shift(Point offset)
        {
            _drawRect.X += offset.X;
            _drawRect.Y += offset.Y;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, DrawRect, _sourceRect, _color, 0, Vector2.Zero, SpriteEffects.None, 0.60f);            
            if (Icon != null)
                spriteBatch.Draw(Icon, DrawRect, IconSourceRect, _color, 0, Vector2.Zero, SpriteEffects.None, 0.70f);
            spriteBatch.DrawString(Style.Font, _text, _textPosition, _textColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0.80f);
        }
    }
}