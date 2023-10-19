using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Solo.Entities
{
    public class Page : IPage
    {   
        public List<IControl> Controls {get; set;}
        public IGUI Parent {get; set;}
        protected bool _isActive;   
        
        public Page()
        {
            Controls = new List<IControl>();
            _isActive = false;
        }

        public Page(IGUI parent, List<IControl> controls)
        {
            Controls = controls;
        }
        
        public void Add(IControl control)
        {
            Controls.Add(control);
        }
        public IControl Get(int n)
        {
            return Controls[n];
        }
        public void Delete(int n)
        {
            Controls.RemoveAt(n);
        }
        public void Clear()
        {
            Controls = new List<IControl>();                 
        }
        public void Activate()
        {
            if (!_isActive)
                foreach(IControl c in Controls)
                   Parent.GUIevent += c.OnGUI;
        }
        public void Deactivate()
        {
            if (_isActive)
                _isActive = false;
        }
        public void Shift(Point offset)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Shift(offset);
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach(IControl c in Controls)
                   c.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(IControl c in Controls)
                   c.Draw(gameTime, spriteBatch);
        }
    }
}