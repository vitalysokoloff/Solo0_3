using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Input;
using Solo.Collections;

namespace Solo
{
    public static class SConsole
    {
        public static Heap Configs = new Heap();
        public static Junk Stuff = new Junk();
        public static SpriteFont Font;
        public static Color FontColor = Color.White;
        public static bool isTextInput = false;
        /// <summary>
        /// Позиция последней строки
        /// </summary>
        public static Vector2 Position = new Vector2(5, 400);

        private static StringBuilder _text = new StringBuilder();
        private static TextInput _textInput = new TextInput(Input.Chars.GetDefaultChars());
        private static bool _state = false;

        public static void Update(GameTime gameTime)
        {
            if (isTextInput)
            {
                string input = _textInput.Listen();
                if (input != null)
                {
                    Write(input);
                }
            }
            if (_text.Length > 6000)
                Clear();
        }
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_state && Font != null)
            {               
                spriteBatch.DrawString(Font, _text, Position - new Vector2(0, Font.MeasureString(_text).Y), FontColor, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);                
            }
        }

        public static void On()
        {
            _state = true;
        }

        public static void Off()
        {
            _state = false;
        }

        public static bool GetState()
        {
            return _state;
        }

        public static void WriteLine(string str)
        {
            _text.AppendLine(str.ToString());
        }

        public static void WriteLine(int n)
        {
            _text.AppendLine(n.ToString());
        }

        public static void WriteLine(float f)
        {
            _text.AppendLine(f.ToString());
        }

        public static void WriteLine(bool b)
        {
            _text.AppendLine(b.ToString());
        }

        public static void WriteLine(object obj)
        {
            _text.AppendLine(obj.ToString());
        }

        public static void Write(string s)
        {
            _text.Append(s);
        }

        public static void Write(int n)
        {
            _text.Append(n);
        }

        public static void Write(float f)
        {
            _text.Append(f);
        }

        public static void Write(bool b)
        {
            _text.Append(b);
        }

        public static void Write(object obj)
        {
            _text.Append(obj);
        }

        public static string ReadLine()
        {
            string[] tmp = _text.ToString().Split('\n');
            return tmp[tmp.Length - 2];
        }

        /// <summary>
        /// Удаляет указанное количество символов начиная с конца
        /// </summary>
        public static void Remove(int n)
        {
            if (_text.Length > 0)
                _text.Remove(_text.Length - 1, n);
        }

        public static void Clear()
        {
            _text.Clear();
        }
    }
}