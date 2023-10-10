using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solo.Entities;

namespace Solo
{
    public class Scene : IEntity
    {
        protected Settings _settings;
        protected Camera _camera;         
        protected GraphicsDevice _graphicsDevice;
        protected bool _isContentLoaded;

        public ContentManager Content; 
        // public GOList;
        // паблик журнал текстур
        // паблик журнал материалов

        public Scene(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice)
        {
            _settings = settings;
            _camera = camera;
            Content = content;
            _graphicsDevice = graphicsDevice;
            _isContentLoaded = false;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //1) draw.gos
            //2) draw.GUI
        }

        public virtual void Draw(GameTime gameTime)
        {
            // свой спрайт бетч
            //1) draw.gos
            //2) draw.GUI
        }

        public virtual void OnLoad(int stage)
        {

        }

        public virtual void Colliding()
        {

        }

        public virtual void ChangeScene(int number)
        {

        }

    }
}