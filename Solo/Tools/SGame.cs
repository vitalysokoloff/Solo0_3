using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo.Collections;
using Solo.Entities;

namespace Solo
{
    public class SGame : Game
    {
        protected Camera _camera;
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;
        protected Settings _settings;
        protected SpriteFont _font;
        protected SConsoleManager _console;
        

        public SGame()
        {
            Heap config = Heap.Open("config.heap");
            _graphics = new GraphicsDeviceManager(this);
            _camera = new Camera();
            Heap gameConfig = config.GetHeap("game");
            Content.RootDirectory = gameConfig.GetString("root-directory");
            IsMouseVisible =  gameConfig.GetBool("mouse-visibility");
            _settings = new Settings(config); 
            _settings.Init(_graphics, _camera);
        }

        protected override void Initialize()
        {
            SConsole.On();
            SConsole.Position = new Vector2(400, _graphics.PreferredBackBufferHeight - 10);
            _console = new SConsoleManager(_graphics, _camera, _font, _settings);            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("font");
            SConsole.Font = _font;              
        }

        protected override void Update(GameTime gameTime)
        {  
            base.Update(gameTime);
            _console.Update(gameTime);
        }
    }
}