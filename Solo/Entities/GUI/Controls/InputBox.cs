using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Solo.Collections;
using Solo.Input;

namespace Solo.Entities  
{
    public class InputBox : Control
    {
        protected int _maxLength;
        protected TextInput _textInput;
        protected KeysInput _keysInput;
        public InputBox(Rectangle drawRect, GUIStyle style, int maxLength) : base (drawRect, style)
        {          
            SetText("");
            _maxLength = maxLength;
            _textInput = new TextInput(Chars.GetDefaultChars());
            _keysInput = new KeysInput();
            _keysInput.Add("remove", new Key(Keys.Back));
        }

        public InputBox(Rectangle drawRect, GUIStyle style, int maxLength, string text) : base (drawRect, style)
        {          
            SetText(text);
            _maxLength = maxLength;
            _textInput = new TextInput(Chars.GetDefaultChars());
            _keysInput = new KeysInput();
            _keysInput.Add("remove", new Key(Keys.Back));
        }

        public InputBox(Rectangle drawRect, GUIStyle style, int maxLength, string text, Heap chars) : base (drawRect, style)
        {          
            SetText(text);
            _maxLength = maxLength;
            _textInput = new TextInput(chars);
            _keysInput = new KeysInput();
            _keysInput.Add("remove", new Key(Keys.Back));
        }

        public override void Update(GameTime gameTime)
        {
            string input = _textInput.Listen();
            if (IsActive)
            {
                if (_text.Length < _maxLength && input != null)
                {
                    _text += input;
                    SetText(_text);                
                }
                if(_keysInput.IsPressed("remove") && _text.Length > 0)
                {  
                    SetText(_text.Remove(_text.Length - 1, 1)); 
                }
            }
        }

        public override void OnGUI(Rectangle hoverRect, bool aButton, bool bButton, bool cButton)
        {
            
            if (hoverRect.Intersects(DrawRect))
            {
                isHovered = true;
            }
            else if (IsActive)
            {
                isHovered = false;
                if (aButton)
                {
                    IsActive = !IsActive;
                }
            }
            else
            {
                isHovered = false;
                IsActive = false;
            }

            if(isHovered)
            {
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
}