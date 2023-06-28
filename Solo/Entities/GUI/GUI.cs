using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Solo.Entities;

namespace Solo.Entities 
{
    public class GUI : IGUI
    {        
        public event GUIDelegate GUIevent;

        protected Dictionary<string, IPage> _pages;
        protected string _currentPage;
        protected MouseState _mouseState;
        public void AddPage(string name, IPage page)
        {
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
            if (!SConsole.Configs.GetBool("gui-lock"))
            {
                MouseState currentState = Mouse.GetState();
                if (_mouseState != currentState)
                {
                    _mouseState = Mouse.GetState();
                    Rectangle rect = new Rectangle(currentState.X, currentState.Y, 2, 2);
                    SConsole.Configs.Add("under-gui-lock", true);
                    GUIevent?.Invoke(rect, currentState.LeftButton == ButtonState.Pressed,
                                    currentState.RightButton == ButtonState.Pressed,
                                    currentState.MiddleButton == ButtonState.Pressed);
                    SConsole.Configs.Add("under-gui-lock", false);
                }
                // сделать геймпад стейт и киборд стейт
                _pages[_currentPage].Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!SConsole.Configs.GetBool("gui-lock"))
            {
                _pages[_currentPage].Draw(gameTime, spriteBatch);
            }
        }
    }
}