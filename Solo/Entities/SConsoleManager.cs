using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Text.RegularExpressions;
using System;
using Solo.Input;

namespace Solo.Entities
{
    public class SConsoleManager : IEntity
    {        
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public Rectangle DrawRectangle;

        protected GraphicsDeviceManager _graphics;
        protected KeysInput _input;
        protected string _buffer;

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
            _input.Add("showLast", new Key(Keys.Tab));
            _input.Add("showLast", new Key(Keys.Up));
            SConsole.Font = font;
            SConsole.FontColor = Color.White;
            SConsole.Position = new Vector2(15, _graphics.PreferredBackBufferHeight / 2);
            SConsole.Off();
            SConsole.isTextInput = true;
            _buffer = "";
        }

        public void Update(GameTime gameTime)
        {
            if (_input.IsPressed("console"))
            {
                if (!SConsole.GetState())
                {
                    SConsole.On();
                    SConsole.WriteLine("[F1] - to help");
                    ParseString("gui-lock +");
                }
                else
                {
                    SConsole.Off();
                    ParseString("gui-lock -");
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
                    SConsole.WriteLine("[Tab  ] - last input line");
                    SConsole.WriteLine("[Up   ] - last input line");
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
                if (_input.IsPressed("showLast"))
                {
                    SConsole.Write(_buffer);
                }
                
                SConsole.Update(gameTime); 
                
                if (SConsole.isTextInput)
                {
                    if (_input.IsPressed("input")) 
                    {
                        _buffer = SConsole.ReadLine();
                        switch (_buffer)
                        {
                            case "reboot":
                                Reboot();
                                break;
                            case "god-mode":
                                SetGodMode();
                                break;
                            case "make-fullscreen":
                                MakeFullScreen();
                                break;
                            case "unmake-fullscreen":
                                UnmakeFullScreen();
                                break;
                            case "graphics-reset":
                                ApplyChanges();
                                break;
                            default:
                                ParseString(_buffer);  
                                break;
                        }                    
                    }  
                }               
            }

                      
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (SConsole.GetState())
            {
                spriteBatch.Draw(Texture, DrawRectangle, SourceRectangle, Color.White * 0.9f, 0, Vector2.Zero, SpriteEffects.None, 0.98f);
                spriteBatch.DrawString(SConsole.Font, ">", SConsole.Position - new Vector2(10, SConsole.Font.MeasureString(">").Y), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
            }
            SConsole.Draw(gameTime, spriteBatch);
        }

        protected void ParseString(string str)
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

        public virtual void Reboot()
        {
            SConsole.WriteLine("You should override this method!");
        }
        public virtual void SetGodMode()
        {
            SConsole.WriteLine("You should override this method!");
        }
        public virtual void MakeFullScreen()
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)SConsole.Stuff.Get("graphics");
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }
        public virtual void UnmakeFullScreen()
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)SConsole.Stuff.Get("graphics");
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }
        public virtual void ApplyChanges()
        {
            GraphicsDeviceManager graphics = (GraphicsDeviceManager)SConsole.Stuff.Get("graphics");
            graphics.ApplyChanges();
        }
    }
}