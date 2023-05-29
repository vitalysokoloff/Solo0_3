using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo
{
    public static class SConsole
    {
        public static SpriteFont Font;
        /// <summary>
        /// Позиция последней строки
        /// </summary>
        public static Vector2 Position = new Vector2(5, 400);

        private static StringBuilder _text = new StringBuilder();

        public static void Update(GameTime gameTime)
        {}
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Font != null)
            {               
                spriteBatch.DrawString(Font, _text, Position - new Vector2(0, Font.MeasureString(_text).Y), Color.White);
            }
        }

        public static void WriteLine(string str)
        {
            _text.Append(str + "\n");
        }

        public static void WriteLine(int n)
        {
            _text.Append(n + "\n");
        }

        public static void WriteLine(float f)
        {
            _text.Append(f + "\n");
        }

        public static void WriteLine(bool b)
        {
            _text.Append(b + "\n");
        }

        public static void WriteLine(object obj)
        {
            _text.Append(obj + "\n");
        }

        public static void Clear()
        {
            _text.Clear();
        }
    }
}