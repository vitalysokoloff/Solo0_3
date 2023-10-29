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
        public Settings GameSettings {get; }

        protected GraphicsDeviceManager _graphics;        
        protected Camera _camera;
        protected KeysInput _input;
        protected string _buffer;

        public SConsoleManager(GraphicsDeviceManager graphics, Camera camera, SpriteFont font, Settings settings)
        {           
            _graphics = graphics;
            _camera = camera;
            GameSettings = settings;


            Texture = Tools.MakeSolidColorTexture(_graphics, new Point(1, 1), new Color(24, 24, 24));
            SourceRectangle = new Rectangle(0, 0, 1, 1);
            DrawRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight / 2);
            _input = new KeysInput();
            _input.Add("console", new Key(Keys.OemTilde));
            _input.Add("help", new Key(Keys.F1));
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
                    SConsole.Configs.Add("gui-lock", true);
                }
                else
                {
                    SConsole.Off();
                    SConsole.Configs.Add("gui-lock", false);
                }
            }

            if (SConsole.GetState())
            {
                if (_input.IsPressed("help"))
                {
                    SConsole.WriteLine("========== Help ==========");
                    SConsole.WriteLine("[F1   ] - help");
                    SConsole.WriteLine("[F3   ] - clear console");
                    SConsole.WriteLine("[Enter] - input last line");
                    SConsole.WriteLine("[Back ] - remove last char");
                    SConsole.WriteLine("[Tab  ] - insert last input line");
                    SConsole.WriteLine("[Up   ] - insert last input line");
                    SConsole.WriteLine("write [-help] to check commands list");
                    SConsole.WriteLine("==========================");
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
                            case "-reboot":
                                Reboot();
                                break;
                            case "-help":
                                Help();
                                break;
                            case "-log":
                                ReadLog();
                                break;
                            case "-reset-settings":
                                GameSettings.Reset(_graphics, _camera);
                                break;
                            case "-save-settings":
                                GameSettings.Save(_graphics);
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
                spriteBatch.Draw(Texture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight / 2), SourceRectangle, Color.White * 0.9f, 0, Vector2.Zero, SpriteEffects.None, 0.98f);
                spriteBatch.DrawString(SConsole.Font, ">", SConsole.Position - new Vector2(10, SConsole.Font.MeasureString(">").Y), Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 1f);
            }
            SConsole.Draw(gameTime, spriteBatch);
        }

        protected virtual void ParseString(string str)
        {
            string setResolution = @"^-resolution\s[0-9]+\s[0-9]+$";
            string showResolution = @"^-resolution$";
            string fullScreenOn = @"^-fullscreen\son$";
            string fullScreenOff = @"^-fullscreen\soff$";
            string getMusic = @"^-music$";
            string getSound = @"^-sound$";
            string setMusic = @"^-music\s[01]\.[0-9]$";
            string setSound = @"^-sound\s[01]\.[0-9]$";
            string openingOn = @"^-opening\son$";
            string openingOff = @"^-opening\soff$";
            string debugOn = @"^-debug\son$";
            string debugOff = @"^-debug\soff$";
            string godOn = @"^-god\son$";
            string godOff = @"^-god\soff$";
            string ConsoleOn = @"^-console\son$";
            string ConsoleOff = @"^-console\soff$";
            string SoundOn = @"^\+msound$";
            string SoundOff = @"^-msound$";

            if (Regex.IsMatch(str, setResolution))
            {
                string[] tmp = str.Split(' ');
                GameSettings.SetResolution(_graphics, _camera, Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2]));
                return;
            }
            if (Regex.IsMatch(str, showResolution))
            {
                SConsole.WriteLine(GameSettings.GetResolution(_graphics));
                return;
            }
            if (Regex.IsMatch(str, fullScreenOn))
            {
                GameSettings.SetFullScreen(_graphics, _camera, true);
                return;
            }
            if (Regex.IsMatch(str, fullScreenOff))
            {
                GameSettings.SetFullScreen(_graphics, _camera, false);
                return;
            }
            if (Regex.IsMatch(str, getMusic))
            {
                GameSettings.GetMusicVolume();
                return;
            }
            if (Regex.IsMatch(str, getSound))
            {
                GameSettings.GetSoundVolume();
                return;
            }
            if (Regex.IsMatch(str, setMusic))
            {
                string[] tmp = str.Replace('.', ',').Split(' ');
                float volume = float.Parse(tmp[1]);
                volume = volume > 1? 1 : volume;
                GameSettings.SetMusicVolume(volume);
                return;
            }
            if (Regex.IsMatch(str, setSound))
            {
                string[] tmp = str.Replace('.', ',').Split(' ');
                float volume = float.Parse(tmp[1]);
                volume = volume > 1? 1 : volume;
                GameSettings.SetSoundVolume(volume);
                return;
            }
            if (Regex.IsMatch(str, openingOn))
            {
                GameSettings.SetOpening(true);
                return;
            }
            if (Regex.IsMatch(str, openingOff))
            {
                GameSettings.SetOpening(false);
                return;
            }
            if (Regex.IsMatch(str, debugOn))
            {
                GameSettings.DebugMode = true;
                return;
            }
            if (Regex.IsMatch(str, debugOff))
            {
                GameSettings.DebugMode = false;
                return;
            }
            if (Regex.IsMatch(str, godOn))
            {
                GameSettings.GodMode = true;
                return;
            }
            if (Regex.IsMatch(str, godOff))
            {
                GameSettings.GodMode = false;
                return;
            }
            if (Regex.IsMatch(str, ConsoleOn))
            {
                GameSettings.SetConsole(true);
                return;
            }
            if (Regex.IsMatch(str, ConsoleOff))
            {
                GameSettings.SetConsole(false);
                return;
            }
            if (Regex.IsMatch(str, SoundOn))
            {
                GameSettings.AllSoundsOFF(false);
                return;
            }
            if (Regex.IsMatch(str, SoundOff))
            {
                GameSettings.AllSoundsOFF(true);
                return;
            }
        }

        public virtual void Reboot()
        {
            SConsole.WriteLine("You should override this method!");
        }

        public virtual void ReadLog()
        {
            GameSettings.GetLog();
        }

        public virtual void Help()
        {
            SConsole.WriteLine("You should override this method!!!");
            SConsole.WriteLine("-----default commands list!!!-----");
            SConsole.WriteLine("-resolution   |   -resolutiob on/off |   -fullscreen on/off   |   -music            |   -music x.x  ");
            SConsole.WriteLine("-sound        |   -sound x.x         |   -opening on/off      |   -debug on/off     | -god on/off   ");
            SConsole.WriteLine("-reboot       |   -help              |   -log                 |   -reset-settings   | -save-settings");
            SConsole.WriteLine("-sound        |   +sound              ");
        }       
    }
}