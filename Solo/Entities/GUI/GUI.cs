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
        protected bool _isMouseInMoving;
        protected Rectangle _rect;
        
        public GUI()
        {
            Index = PlayerIndex.One;
            _pages = new Dictionary<string, IPage>();
            _mouseState = new MouseState();
            _keyboardState = new KeyboardState();
            _gamePadState = new GamePadState();
            _pointer = 0;
            _isMouseInMoving = false;
            _rect = new Rectangle(0, 0, 2, 2); 
            InputInit();
        }
        public void AddPage(string name, IPage page)
        {
            page.Parent = this;
            if (_pages.ContainsKey(name))
            {
                _pages[name] = page;
            }  
            else
            {
                _pages.Add(name, page);
            }
        }
        public void DeletePage(string name)
        {
            if (_pages.ContainsKey(name))
            {
                if (name == _currentPage)
                {
                    GUIevent = null;
                    _currentPage = null;
                }
                _pages.Remove(name);
            }
        }
        public string[] GetKeys()
        {
            string[] keys = new string[_pages.Keys.Count];
            _pages.Keys.CopyTo(keys, 0); 
            return keys;   
        }
        public void SetPage(string name)
        {
            if (name == null)
            {
                _currentPage = null;
                if (SConsole.Configs.GetBool("debug"))    
                    SConsole.WriteLine("GUI is off"); 
                return;
            }
            if (_pages.ContainsKey(name))
            {
                GUIevent = null;
                if (_currentPage == null)
                {  
                    if (SConsole.Configs.GetBool("debug"))     
                        SConsole.WriteLine("Current GUI page is null"); 
                }
                else
                {
                    _pages[_currentPage].Deactivate();                    
                }
                _currentPage = name;
                _pages[_currentPage].Activate();
            }
            else
            {
                if (SConsole.Configs.GetBool("debug"))  
                    SConsole.WriteLine("There is no this GUI page");
            }
            _pointer = 0;
        }
        public IPage GetPage(string name)
        {
            if (_pages.ContainsKey(name))
                return _pages[name];
            else
                return new Page();
        }
        public void Update(GameTime gameTime)
        {
            if (_currentPage != null && !SConsole.Configs.GetBool("gui-lock"))
            {
                _pages[_currentPage].Update(gameTime);
                MouseState currentMState = Mouse.GetState();
                
                IPage currentPage = _pages[_currentPage]; 
                int length = currentPage.Controls.Count; 
                if (_mouseState.Position != currentMState.Position)
                {
                    _rect = new Rectangle(_mouseState.X, _mouseState.Y, 1, 1);
                } 
                if (Input.IsPressed("Up"))
                {
                    _pointer--;
                    if (_pointer < 0)
                        _pointer = 0;
                    _rect = new Rectangle(currentPage.Controls[_pointer].DrawRect.X, currentPage.Controls[_pointer].DrawRect.Y, 2, 2);  
                }
                if (Input.IsPressed("Down"))
                {
                    _pointer++;
                    if (_pointer >= length)
                        _pointer = length - 1;
                    _rect = new Rectangle(currentPage.Controls[_pointer].DrawRect.X, currentPage.Controls[_pointer].DrawRect.Y, 2, 2);  
                }

                GUIevent?.Invoke(_rect, false,
                                    false,
                                    false);

                if (Input.IsPressed("A"))
                {
                    GUIevent?.Invoke(_rect, true,false,false);
                }
                if (Input.IsPressed("B"))
                {
                    GUIevent?.Invoke(_rect, false,true,false);
                }
                _mouseState = Mouse.GetState(); 
                _keyboardState = new KeyboardState();
                _gamePadState = new GamePadState();                
            }
        }

        public void Shift(Point offset)
        {
            foreach (string k in _pages.Keys)
            {
                _pages[k].Shift(offset);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_currentPage != null && !SConsole.Configs.GetBool("gui-lock"))
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
            Input.Add("A", new Key(MouseButtons.Left));
            Input.Add("B",new Key(Keys.LeftAlt));
            Input.Add("B",new Key(Buttons.B));
            Input.Add("B", new Key(MouseButtons.Right));
        }
    }
}