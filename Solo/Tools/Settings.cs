using Microsoft.Xna.Framework;
using Solo.Collections;
using Solo.Entities;

namespace Solo
{
    public class Settings
    {
        public Heap Config { get; set; }
        public float SoundVolume 
        {
            get
            {
                return Config.GetHeap("audio").GetInt("sound");
            }
            set
            {
                Config.GetHeap("audio").Add("sound", value);
            }
        }
        public float MusicVolume 
        {
            get
            {
                return Config.GetHeap("audio").GetInt("music");
            }
            set
            {
                Config.GetHeap("audio").Add("music", value);
            }
        }

        public Settings(Heap config)
        {
            Config = config; 
        }

        public virtual void Init(GraphicsDeviceManager graphics, Camera camera)
        {
            // Window settings
            Heap window = Config.GetHeap("window");
            graphics.PreferredBackBufferWidth = window.GetInt("width");
            graphics.PreferredBackBufferHeight = window.GetInt("height");
            graphics.IsFullScreen = window.GetBool("fullscreen");
            Reset(graphics, camera);  
        }

        public virtual void SetResolution(GraphicsDeviceManager graphics, Camera camera, int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            Reset(graphics, camera);
        }

        public virtual Point GetResolution(GraphicsDeviceManager graphics)
        {
            return new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public virtual void SetFullScreen(GraphicsDeviceManager graphics, Camera camera, bool isFullScreen)
        {
            graphics.IsFullScreen = isFullScreen; 
            Reset(graphics, camera);
        }

        public virtual void Reset(GraphicsDeviceManager graphics, Camera camera)
        {
            graphics.ApplyChanges();
            SConsole.Position = new Vector2(15, graphics.PreferredBackBufferHeight / 2);
            camera.Center = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            camera.Scale = new Vector3(graphics.PreferredBackBufferWidth / (float)Config.GetHeap("game").GetInt("original-width"), 
                                        graphics.PreferredBackBufferHeight / (float)Config.GetHeap("game").GetInt("original-height"), 
                                        1);
        }

        public void Save(GraphicsDeviceManager graphics)
        {
            Heap window = Config.GetHeap("window");
            window.Add("width", graphics.PreferredBackBufferWidth);
            window.Add("height", graphics.PreferredBackBufferHeight);
            window.Add("fullscreen", graphics.IsFullScreen); 
            Config.Save("config.heap");
        }
    }
}