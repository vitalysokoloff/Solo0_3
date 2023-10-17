using Microsoft.Xna.Framework;
using Solo.Collections;
using Solo.Entities;

namespace Solo
{
    public class Settings
    {
        public Heap Config { get; set; }
        public Point GUIOffset { get; set;}
        public float SoundVolume 
        {
            get
            {
                return Config.GetHeap("audio").GetFloat("sound");
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
                return Config.GetHeap("audio").GetFloat("music");
            }
            set
            {
                Config.GetHeap("audio").Add("music", value);
            }
        }
        public bool DebugMode
        {
            get
            {
                return Config.GetHeap("game").GetBool("debug-mode");
            }
            set
            {
                Config.GetHeap("game").Add("debug-mode", value);
            }
        }
        public bool GodMode
        {
            get
            {
                return Config.GetHeap("game").GetBool("god-mode");
            }
            set
            {
                Config.GetHeap("game").Add("god-mode", value);
            }
        }
        public bool IsFullScreen
        {
            get
            {
                return Config.GetHeap("window").GetBool("fullscreen");
            }
        }

        public Settings(Heap config)
        {
            Config = config;
            SConsole.Configs.Add("log", new Heap());            
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

        public virtual void SetResolution(GraphicsDeviceManager graphics, Camera camera, Point point)
        {
            SetResolution(graphics, camera, point.X, point.Y);
        }

        public virtual Point GetResolution(GraphicsDeviceManager graphics)
        {
            return new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
        }

        public virtual void SetFullScreen(GraphicsDeviceManager graphics, Camera camera, bool isFullScreen)
        {
            graphics.IsFullScreen = isFullScreen; 
            Heap window = Config.GetHeap("window");            
            Reset(graphics, camera);
            window.Add("fullscreen", graphics.IsFullScreen);
            SConsole.WriteLine(IsFullScreen); 
        }

        public virtual void Reset(GraphicsDeviceManager graphics, Camera camera)
        {
            graphics.ApplyChanges();
            SConsole.Position = new Vector2(15, graphics.PreferredBackBufferHeight / 2);
            camera.Center = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            camera.Scale = new Vector3(graphics.PreferredBackBufferWidth / (float)Config.GetHeap("game").GetInt("original-width"), 
                                        graphics.PreferredBackBufferHeight / (float)Config.GetHeap("game").GetInt("original-height"), 
                                        1);
            
            Heap window = Config.GetHeap("window"); 
            Point curRes = new Point(window.GetInt("width") / 2, window.GetInt("height") / 2);                       
            window.Add("width", graphics.PreferredBackBufferWidth);
            window.Add("height", graphics.PreferredBackBufferHeight);
            Point newRes = new Point(window.GetInt("width") / 2, window.GetInt("height") / 2);
            GUIOffset = newRes - curRes;
            SConsole.WriteLine("new resolution:" + GetResolution(graphics) + " GUIOffset: " + GUIOffset);
        }

        public virtual void SetMusicVolume(float f)
        {
            f = f > 1? 1 : f < 0? 0 : f;
            Config.GetHeap("audio").Add("music", f);
            SConsole.WriteLine("new value:" + MusicVolume);
        }
        
        public virtual void SetSoundVolume(float f)
        {
            f = f > 1? 1 : f < 0? 0 : f;
            Config.GetHeap("audio").Add("sound", f);
            SConsole.WriteLine("new value:" + SoundVolume);
        }

        public virtual void SetOpening(bool b)
        {
            Config.GetHeap("game").Add("opening-logos", b);
            SConsole.WriteLine("ok");
        }

        public virtual void SetLog(string name, string value)
        {
            SConsole.Configs.GetHeap("log").Add(name, value);
        }

        public virtual void GetMusicVolume()
        {
            SConsole.WriteLine(Config.GetHeap("audio").GetFloat("music"));
        }

        public virtual void GetSoundVolume()
        {
            SConsole.WriteLine(Config.GetHeap("audio").GetFloat("sound"));
        }

        public virtual void GetLog()
        {
            SConsole.WriteLine(SConsole.Configs.GetHeap("log"));
        }

        public void Save(GraphicsDeviceManager graphics)
        {            
            Config.Save("config.heap");
            SConsole.WriteLine("ok");
        }
    }
}