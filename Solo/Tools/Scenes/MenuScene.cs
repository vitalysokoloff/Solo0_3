using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solo.Collections;
using Solo.Entities;

namespace Solo
{
    public class MenuScene : Scene
    {
        protected GUI _gui;
        public MenuScene(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics) : 
            base(settings, camera, content, graphicsDevice, graphics)
        {
            Heap gameConfig = _settings.Config.GetHeap("game");
            int width = gameConfig.GetInt("original-width");
            int height = gameConfig.GetInt("original-height");
            string name = gameConfig.GetString("name");
            SpriteFont font = SConsole.Font;
            Vector2 nameSize = font.MeasureString(name);

            GUIStyle _style = new GUIStyle(_graphics, font);
            
            _gui = new GUI();
            Page main = new Page();            
            Label title = new Label(new Rectangle((int)(width / 2 - nameSize.X / 2), (int)(height / 2 - nameSize.Y), (int)nameSize.X, (int)nameSize.Y), _style, name);
            main.Add(title);
            Button play = new Button(new Rectangle(width / 2 - 50, height / 2 + 10, 100, (int)nameSize.Y), _style, "Играть");            
            main.Add(play);
            Button setting = new Button(new Rectangle(width / 2 - 50, height / 2 + 12 + (int)nameSize.Y, 100, (int)nameSize.Y), _style, "Настройки");            
            main.Add(setting);
            Button exit = new Button(new Rectangle(width / 2 - 50, height / 2 + 14 + (int)nameSize.Y * 2, 100, (int)nameSize.Y), _style, "Выход");            
            main.Add(exit);
            _gui.AddPage("main", main); 
            _gui.SetPage("main");           
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            _gui.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
        }
    }
}