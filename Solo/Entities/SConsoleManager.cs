using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Solo.Entities;
using Solo.Collections;
using Solo.Input;
using System.Text.RegularExpressions;
using System;

namespace Solo
{
    public class SConsoleManager : IEntity
    {        
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public Rectangle DrawRectangle;

        protected GraphicsDeviceManager _graphics;
        protected KeysInput _input;

        public SConsoleManager(GraphicsDeviceManager graphics, SpriteFont font)
        {           
            _graphics = graphics;

            Texture = Tools.MakeSolidColorTexture(_graphics, new Point(1, 1), new Color(34, 34, 34));
            SourceRectangle = new Rectangle(0, 0, 1, 1);
            DrawRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight / 2);
            _input = new KeysInput();
            _input.Add("console", new Key(Keys.OemTilde));
            _input.Add("help", new Key(Keys.F1));
            _input.Add("configs", new Key(Keys.F2));
            _input.Add("clear", new Key(Keys.F3));
            _input.Add("input", new Key(Keys.Enter));
            _input.Add("remove", new Key(Keys.Back));
            SConsole.Font = font;
            SConsole.FontColor = Color.White;
            SConsole.Position = new Vector2(15, _graphics.PreferredBackBufferHeight / 2);
            SConsole.Off();
            SConsole.isTextInput = true;
        }

        public void Update(GameTime gameTime)
        {
            if (_input.IsPressed("console"))
            {
                if (!SConsole.GetState())
                {
                    SConsole.On();
                    SConsole.WriteLine("[F1] - to help");
                }
                else
                {
                    SConsole.Off();
                }
            }

            if (SConsole.GetState())
            {
                if (_input.IsPressed("help"))
                {
                    SConsole.WriteLine("========== Help ==========");
                    SConsole.WriteLine("[F1   ] - help");
                    SConsole.WriteLine("[F2   ] - show configs");
                    SConsole.WriteLine("[F3   ] - clear console");
                    SConsole.WriteLine("[Enter] - input last line");
                    SConsole.WriteLine("[Back ] - remove last char");
                    SConsole.WriteLine("==========================");
                }
                if (_input.IsPressed("configs"))
                {
                    SConsole.Write("\n" + SConsole.Configs.ToString());
                }
                if (_input.IsPressed("clear"))
                {
                    SConsole.Clear();
                }
                if (_input.IsPressed("remove"))
                {
                    SConsole.Remove(1);
                }
                
                SConsole.Update(gameTime); 
                
                if (SConsole.isTextInput)
                {
                    if (_input.IsPressed("input")) 
                    {
                        ParseString(SConsole.ReadLine());
                    }  
                }               
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

        private void ParseString(string str)
        {
            string intPattern = @"^.+\s[0-9]+$";
            string floatPattern = @"^.+\s((\d+,\d+)|(\d+))f$";
            string stringPattern = "^.+\\s\".+\"$";
            string truePattern = @"^.+\s(\+|true|True|on|On)$";
            string falsePattern = @"^.+\s(-|false|False|off|Off)$";

            if (Regex.IsMatch(str, intPattern))
            {
                string[] tmp = str.Split(' ');
                SConsole.Configs.Add(tmp[0], Convert.ToInt32(tmp[1]));
                return;
            }
            if (Regex.IsMatch(str, floatPattern))
            {
                string[] tmp = str.Split(' ');
                SConsole.Configs.Add(tmp[0], (float)Convert.ToDouble(tmp[1].Trim('f')));
                return;
            }
            if (Regex.IsMatch(str, stringPattern))
            {
                string[] tmp = str.Split(' ');
                SConsole.Configs.Add(tmp[0], tmp[1]);
                return;
            }
            if (Regex.IsMatch(str, truePattern))
            {
                string[] tmp = str.Split(' ');
                SConsole.Configs.Add(tmp[0], true);
                return;
            }
            if (Regex.IsMatch(str, falsePattern))
            {
                string[] tmp = str.Split(' ');
                SConsole.Configs.Add(tmp[0], false);
                return;
            }
        }
    }
}