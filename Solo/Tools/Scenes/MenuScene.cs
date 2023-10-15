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
            Vector2 settingsTitleSize = font.MeasureString("  Настройки  ");

            GUIStyle _style = new GUIStyle(_graphics, font);
            
            _gui = new GUI();
           
            Page main = new Page();            
            Label title = new Label(new Rectangle((int)(width / 2 - nameSize.X / 2), (int)(height / 2 - nameSize.Y), (int)nameSize.X, (int)nameSize.Y), _style, name);
            main.Add(title);
            Button play = new Button(new Rectangle(width / 2 - 50, height / 2 + 10, 100, (int)nameSize.Y), _style, "Играть");            
            main.Add(play);
            Button setting = new Button(new Rectangle(width / 2 - 50, height / 2 + 12 + (int)nameSize.Y, 100, (int)nameSize.Y), _style, "Настройки");            
            setting.AButtonAction = () =>
            {
                _gui.SetPage("settings");
            };
            main.Add(setting);
            Button exit = new Button(new Rectangle(width / 2 - 50, height / 2 + 14 + (int)nameSize.Y * 2, 100, (int)nameSize.Y), _style, "Выход");            
            main.Add(exit);
            exit.AButtonAction = () =>
            {
                ChangeScene?.Invoke(-1);
            };           
            
            Page settingsMenu = new Page();
            Label title2 = new Label(new Rectangle((int)(width / 2 - settingsTitleSize.X / 2), (int)(height / 2 - nameSize.Y), (int)settingsTitleSize.X, (int)nameSize.Y), _style, "Настройки");
            settingsMenu.Add(title2);
            Box volumeBox = new Box(new Rectangle(width / 2 - 75, height / 2 + 9, 150, (int)nameSize.Y + 2), _style); 
            settingsMenu.Add(volumeBox);
            Label sound = new Label(new Rectangle(width / 2 - 75, height / 2 + 10, 100, (int)nameSize.Y), _style, "Звук: " + (int)(_settings.SoundVolume * 100)); 
            settingsMenu.Add(sound);
            Button soundMinus = new Button(new Rectangle(width / 2 + 25, height / 2 + 10, 25, (int)nameSize.Y), _style, "-");            
            soundMinus.AButtonAction = () =>
            {
                _settings.SetSoundVolume(_settings.SoundVolume - 0.1f);
                sound.SetText("Звук: " + (int)(_settings.SoundVolume * 100));
            };
            settingsMenu.Add(soundMinus);
            Button soundPlus = new Button(new Rectangle(width / 2 + 50, height / 2 + 10, 25, (int)nameSize.Y), _style, "+");  
            soundPlus.AButtonAction = () =>
            {
                _settings.SetSoundVolume(_settings.SoundVolume + 0.1f);
                sound.SetText("Звук: " + (int)(_settings.SoundVolume * 100));
            };          
            settingsMenu.Add(soundPlus);

            _gui.AddPage("main", main); 
            _gui.AddPage("settings", settingsMenu);
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