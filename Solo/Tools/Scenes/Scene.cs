using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solo.Collections;
using Solo.Entities;
using Solo.Input;
using Solo.Physics;

namespace Solo
{
    public class Scene : IEntity
    {
        public ColliderManager ColliderManager
        {
            get
            {
                return _colliderManager;
            }
        }
        protected bool _isContentLoaded {get; set;}  
        public ChangeScene ChangeScene;
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
        protected SpriteBatch _spriteBatch;  
        protected GraphicsDeviceManager _graphics;
        protected ColliderManager _colliderManager;

        public Scene(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            _settings = settings;
            _camera = camera;
            Content = content;
            _graphicsDevice = graphicsDevice;
            _graphics = graphics;
            _isContentLoaded = false;
            _colliderManager = new ColliderManager();
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
            Heap game = _settings.Config.GetHeap("game");
            int width = game.GetInt("original-width");
            int height = game.GetInt("original-height");
            _spriteBatch = new SpriteBatch(graphicsDevice);  
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_isContentLoaded)
            {
                List<IGameObject> updatingGOs = new List<IGameObject>();
                foreach (string k in GOs.Keys)
                {
                    if (_camera.DrawRectangle.Intersects(GOs[k].DrawRect) && GOs[k].IsExist)
                        updatingGOs.Add(GOs[k]);
                }
                _settings.SetLog("U-qty", updatingGOs.Count.ToString());

                foreach(IGameObject go in updatingGOs)
                {
                    go.Update(gameTime);
                }

                _colliderManager.Colliding(updatingGOs);
            }
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
                    if (_camera.DrawRectangle.Intersects(GOs[k].DrawRect) && GOs[k].IsExist)
                        drawingGOs.Add(GOs[k]);
                }
                _settings.SetLog("D-qty", drawingGOs.Count.ToString());

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

    } 

    public delegate void ChangeScene(int number);

}