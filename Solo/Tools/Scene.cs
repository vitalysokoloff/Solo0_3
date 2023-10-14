using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solo.Collections;
using Solo.Entities;
using Solo.Physics;

namespace Solo
{
    public class Scene : IEntity
    {
        public ContentManager Content; 
        public Dictionary<string, IGameObject> GOs;
        public Dictionary<string, Texture2D> Textures;
        public Dictionary<string, SMaterial> Materials;
        public Dictionary<string, SoundEffect> Sounds;
        public string MapsDirectory;
        public string TexturesDirectory;
        public string RootDirectory;
        public string AudioDirectory;

        protected Settings _settings;
        protected Camera _camera;         
        protected GraphicsDevice _graphicsDevice;
        protected bool _isContentLoaded;  
        protected SpriteBatch _spriteBatch;     

        public Scene(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice)
        {
            _settings = settings;
            _camera = camera;
            Content = content;
            _graphicsDevice = graphicsDevice;
            _isContentLoaded = false;
            GOs = new Dictionary<string, IGameObject>();
            Textures = new Dictionary<string, Texture2D>();
            Materials = new Dictionary<string, SMaterial>();
            Sounds = new Dictionary<string, SoundEffect>();
            Heap GSettings = settings.Config.GetHeap("game");
            RootDirectory = GSettings.GetString("root-directory");
            Content.RootDirectory = RootDirectory;
            TexturesDirectory = GSettings.GetString("texture-directory") + "\\";
            MapsDirectory = RootDirectory + "\\" + GSettings.GetString("maps-directory") + "\\";
            AudioDirectory = GSettings.GetString("audio-directory") + "\\";
            _spriteBatch = new SpriteBatch(graphicsDevice);
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
            if (_isContentLoaded)
            {
                List<IGameObject> drawingGOs = new List<IGameObject>();
                foreach (string k in GOs.Keys)
                {
                    //if (GJK.CheckCollision(GOs[k].Collider.GetShape(), _camera.DrawRectangle))
                        drawingGOs.Add(GOs[k]);
                }
                _settings.SetLog("qty", drawingGOs.Count.ToString());

                //1) draw.gos
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, transformMatrix: _camera.Transform);
                    foreach(IGameObject go in drawingGOs)
                    {
                        go.Draw(gameTime, _spriteBatch);
                        if (_settings.DebugMode)
                            go.Debug(gameTime, _spriteBatch);
                    }
                _spriteBatch.End();
                
                //2) draw.GUI
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    foreach(IGameObject go in drawingGOs)
                        go.GUI(gameTime, _spriteBatch);
                _spriteBatch.End();
            }
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