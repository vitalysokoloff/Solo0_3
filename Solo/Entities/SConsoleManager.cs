using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Solo.Entities;
using Solo.Collections;
using Solo.Input;

namespace Solo
{
    public class SConsoleManager : IEntity
    {
        public Heap Configs;
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public Rectangle DrawRectangle;

        private GraphicsDeviceManager _graphics;
        private KeysInput _input;

        public SConsoleManager(GraphicsDeviceManager graphics, SpriteFont font)
        {
            Configs = new Heap();            
            _graphics = graphics;

            Texture = Tools.MakeSolidColorTexture(_graphics, new Point(1, 1), new Color(34, 34, 34));
            SourceRectangle = new Rectangle(0, 0, 1, 1);
            DrawRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight / 2);
            _input = new KeysInput();
            _input.Add("console", new Key(Keys.OemTilde));
            _input.Add("unlockConsole", new Key(Keys.F1));
            _input.Add("input", new Key(Keys.Enter));
            SConsole.Font = font;
            SConsole.FontColor = Color.White;
            SConsole.Position = new Vector2(15, _graphics.PreferredBackBufferHeight / 2);
            SConsole.Off();
            SConsole.isTextInput = true;
        }

        public void Update(GameTime gameTime)
        {
            if (_input.IsPressed("console") == SKeyState.Pressed)
            {
                if (!SConsole.GetState())
                {
                    SConsole.On();
                }
                else
                {
                    SConsole.Off();
                }
            }

            if (SConsole.GetState())
            {
                if (_input.IsPressed("unlockConsole") == SKeyState.Pressed)
                {
                    if (!SConsole.isTextInput)
                    {
                        SConsole.isTextInput = true;
                    }
                    else
                    {
                        SConsole.isTextInput = false;
                    }
                }
                SConsole.Update(gameTime);
            }

            if (SConsole.isTextInput)
                if (_input.IsPressed("input") == SKeyState.Pressed) 
                {
                    string[] tmp = SConsole.ReadLine().Split(' ');
                    if (tmp.Length > 1)
                        Configs.Add(tmp[0], tmp[1]);
                }            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (SConsole.GetState())
            {
                spriteBatch.Draw(Texture, DrawRectangle, SourceRectangle, Color.White * 0.9f);
                spriteBatch.DrawString(SConsole.Font, ">", SConsole.Position - new Vector2(10, SConsole.Font.MeasureString(">").Y), Color.White);
            }
            SConsole.Draw(gameTime, spriteBatch);
        }
    }
}