using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Solo.Entities
{
    public class Page : IPage
    {   
        protected Dictionary<string,IControl> _controls;
        protected IGUI _parent;
        protected bool _isActive;   
        
        public Page(IGUI parent)
        {
            _parent = parent;
            _controls = new Dictionary<string, IControl>();
        }

        public Page(IGUI parent, Dictionary<string,IControl> controls)
        {
            _parent = parent;
            _controls = controls;
        }
        
        public void Add(string name, IControl control)
        {
            _controls.Add(name, control);
        }
        public void Delete(string name)
        {
            if (_isActive)
                _parent.GUIevent -= _controls[name].OnGUI;
            _controls.Remove(name);
        }
        public void Clear()
        {
            if (_isActive)
                foreach(string k in _controls.Keys)
                    _parent.GUIevent -= _controls[k].OnGUI;
            _controls = new Dictionary<string, IControl>();                    
        }
        public string[] GetKeys()
        {
            string[] keys = new string[_controls.Keys.Count];
            _controls.Keys.CopyTo(keys, 0); 
            return keys;   
        }
        public void Activate()
        {
            if (!_isActive)
                foreach(string k in _controls.Keys)
                   _parent.GUIevent += _controls[k].OnGUI;
        }
        public void Deactivate()
        {
            if (_isActive)
                foreach(string k in _controls.Keys)
                   _parent.GUIevent -= _controls[k].OnGUI;
        }
        public void Update(GameTime gameTime)
        {
            foreach(string k in _controls.Keys)
                   _controls[k].Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(string k in _controls.Keys)
                   _controls[k].Draw(gameTime, spriteBatch);
        }
    }
}