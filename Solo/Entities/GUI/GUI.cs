using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Solo.Input;

namespace Solo.Entities 
{
    public class GUI : IGUI
    {        
        public ISInput Input {get; protected set;}
        public PlayerIndex Index {get; protected set;}

        public event GUIDelegate GUIevent;

        protected Dictionary<string, IPage> _pages;
        protected string _currentPage;
        protected MouseState _mouseState;
        protected KeyboardState _keyboardState;
        protected GamePadState _gamePadState;
        protected int _pointer;
        
        public GUI()
        {
            Index = PlayerIndex.One;
            _pages = new Dictionary<string, IPage>();
            _currentPage = "";
            _mouseState = new MouseState();
            _keyboardState = new KeyboardState();
            _gamePadState = new GamePadState();
            _pointer = 0;
            InputInit();
        }
        public void AddPage(string name, IPage page)
        {
            page.Parent = this;
            _pages.Add(name, page);
        }
        public void DeletePage(string name)
        {
            _pages[name].Deactivate();
            _pages.Remove(name);
        }
        public string[] GetKeys()
        {
            string[] keys = new string[_pages.Keys.Count];
            _pages.Keys.CopyTo(keys, 0); 
            return keys;   
        }
        public void SetPage(string name)
        {
            if (_currentPage != "")
                _pages[_currentPage].Deactivate();
            _currentPage = name;
            _pages[_currentPage].Activate();
        }
        public IPage GetPage(string name)
        {
            return _pages[name];
        }
        public void Update(GameTime gameTime)
        {
            if (_currentPage != "" && !SConsole.Configs.GetBool("gui-lock"))
            {
                MouseState currentMState = Mouse.GetState();
                if (_mouseState != currentMState)
                {
                    _mouseState = Mouse.GetState();
                    Rectangle rect = new Rectangle(currentMState.X, currentMState.Y, 2, 2);
                    SConsole.Configs.Add("under-gui-lock", true);
                    GUIevent?.Invoke(rect, currentMState.LeftButton == ButtonState.Pressed,
                                    currentMState.RightButton == ButtonState.Pressed,
                                    currentMState.MiddleButton == ButtonState.Pressed);
                    SConsole.Configs.Add("under-gui-lock", false);
                }
                KeyboardState currentKState = Keyboard.GetState();
                GamePadState currentGState = GamePad.GetState(Index);
                if (_keyboardState != currentKState || _gamePadState != currentGState )
                {
                    IPage currentPage = _pages[_currentPage]; 
                    int length = currentPage.Controls.Count;
                    bool isA = false;
                    bool isB = false;
                    
                    if (Input.IsPressed("Up"))
                    {
                        _pointer--;
                        if (_pointer < 0)
                            _pointer = length - 1;
                    }
                    if (Input.IsPressed("Down"))
                    {
                        _pointer++;
                        if (_pointer >= length)
                            _pointer = 0;
                    }
                    if (Input.IsPressed("A"))
                    {
                        isA = true;
                        _pointer = 0;
                    }
                    if (Input.IsPressed("B"))
                        isB = true;
                    Rectangle rect = new Rectangle(currentPage.Controls[_pointer].DrawRect.X, currentPage.Controls[_pointer].DrawRect.Y, 2, 2);
                    GUIevent?.Invoke(rect, isA,
                                    isB,
                                    false);
                }
                _pages[_currentPage].Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_currentPage != "" && !SConsole.Configs.GetBool("gui-lock"))
            {
                _pages[_currentPage].Draw(gameTime, spriteBatch);
            }
        }

        protected virtual void InputInit()
        {
            Input = new KeysInput();
            Input.Add("Up",new Key(Keys.Up));
            Input.Add("Down",new Key(Keys.Down));
            Input.Add("Up",new Key(Buttons.DPadUp));
            Input.Add("Down",new Key(Buttons.DPadDown));
            Input.Add("A",new Key(Keys.Enter));
            Input.Add("A",new Key(Buttons.A));
            Input.Add("B",new Key(Keys.LeftAlt));
            Input.Add("B",new Key(Buttons.B));
        }
    }
}