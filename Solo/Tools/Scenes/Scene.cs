using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solo.Collections;
using Solo.Entities;

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
        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get
            {
                return _graphics;
            }
        }
        public Camera Camera
        {
            get
            {
                return _camera;
            }
        }
        public List<IGameObject> ActingGOs
        {
            get
            {
                return _updatingGOs;
            }
        }

        protected bool _isContentLoaded {get; set;}  
        public ChangeScene ChangeScene;
        public ContentManager Content; 
        public Dictionary<string, IGameObject> GOs;
        public Dictionary<string, Texture2D> Textures;
        public Dictionary<string, SMaterial> Materials;
        public Dictionary<string, SoundEffect> Sounds;
        public Dictionary<string, SoundEffect> Musics;
        public string MapsDirectory;
        public string TexturesDirectory;
        public string RootDirectory;
        public string AudioDirectory;

        public Settings Settings;
        protected Camera _camera;         
        protected GraphicsDevice _graphicsDevice;        
        protected SpriteBatch _spriteBatch;  
        protected GraphicsDeviceManager _graphics;
        protected ColliderManager _colliderManager;
        protected List<IGameObject> _updatingGOs;

        public Scene(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics)
        {
            Settings = settings;
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
            Musics = new Dictionary<string, SoundEffect>();
            Heap GSettings = settings.Config.GetHeap("game");
            RootDirectory = GSettings.GetString("root-directory");
            Content.RootDirectory = RootDirectory;
            TexturesDirectory = GSettings.GetString("textures-directory") + "\\";
            MapsDirectory = RootDirectory + "\\" + GSettings.GetString("maps-directory") + "\\";
            AudioDirectory = GSettings.GetString("audio-directory") + "\\";
            Heap game = Settings.Config.GetHeap("game");
            int width = game.GetInt("original-width");
            int height = game.GetInt("original-height");
            _spriteBatch = new SpriteBatch(graphicsDevice);  
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_isContentLoaded)
            {
                _updatingGOs = new List<IGameObject>();
                foreach (string k in GOs.Keys)
                {
                    if (_camera.DrawRectangle.Intersects(GOs[k].DrawRect) && GOs[k].IsExist)
                        _updatingGOs.Add(GOs[k]);
                }
                Settings.SetLog("U-qty", _updatingGOs.Count.ToString());

                foreach(IGameObject go in _updatingGOs)
                {
                    go.Update(gameTime);
                }

                _colliderManager.Colliding(_updatingGOs);
            }
            else
            {
                WhileLoadUpdate(gameTime);
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
                Settings.SetLog("D-qty", drawingGOs.Count.ToString());

                //1) draw.gos
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, transformMatrix: _camera.Transform);
                    foreach(IGameObject go in drawingGOs)
                    {
                        go.Draw(gameTime, _spriteBatch);
                        if (Settings.DebugMode)
                            go.Debug(gameTime, _spriteBatch);
                    }
                _spriteBatch.End();
                
                //2) draw.GUI
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                    foreach(IGameObject go in drawingGOs)
                        go.GUI(gameTime, _spriteBatch);
                _spriteBatch.End();
            }
            else
            {
                WhileLoadDraw(gameTime, _spriteBatch);
            }
        }

        public virtual void OnLoad(int stage)
        {

        }

        public virtual void ChangingScene(int number)
        {

        }

        public virtual void WhileLoadDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public virtual void WhileLoadUpdate(GameTime gameTime)
        {

        }

    } 

    public delegate void ChangeScene(int number);

}