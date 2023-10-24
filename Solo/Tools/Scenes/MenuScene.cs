using System.Threading;
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
        protected Point[] _resolutions;
        protected int _resolutionPointer;
        public MenuScene(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics) : 
            base(settings, camera, content, graphicsDevice, graphics)
        {
            Heap game = Settings.Config.GetHeap("game");
            int width = game.GetInt("original-width");
            int height = game.GetInt("original-height");
            string name = game.GetString("name");            
            SpriteFont font = SConsole.Font;
            Vector2 nameSize = font.MeasureString(name);
            Vector2 settingsTitleSize = font.MeasureString("  Настройки  ");

            _resolutions = new Point[]
            {
                new Point(1280, 720),
                new Point(1600, 900),
                new Point(1920, 1080)
            };
            Point curResolution = Settings.GetResolution(_graphics);
            _resolutionPointer = curResolution == _resolutions[0]? 0 : curResolution == _resolutions[1]? 1 : 2;

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
            Button manual = new Button(new Rectangle(width / 2 - 50, height / 2 + 14 + (int)nameSize.Y * 2, 100, (int)nameSize.Y), _style, "Управление");            
            main.Add(manual);
            Button exit = new Button(new Rectangle(width / 2 - 50, height / 2 + 16 + (int)nameSize.Y * 3, 100, (int)nameSize.Y), _style, "Выход");            
            main.Add(exit);
            exit.AButtonAction = () =>
            {
                ChangeScene?.Invoke(-1);
            };          
            
            Page settingsMenu = new Page();
            Label title2 = new Label(new Rectangle((int)(width / 2 - settingsTitleSize.X / 2), (int)(height / 2 - nameSize.Y), (int)settingsTitleSize.X, (int)nameSize.Y), _style, "Настройки");
            settingsMenu.Add(title2);           
            Label sound = new Label(new Rectangle(width / 2 - 75, height / 2 + 10, 100, (int)nameSize.Y), _style, "Звук: " + (int)(Settings.SoundVolume * 100)); 
            settingsMenu.Add(sound);
            Button soundMinus = new Button(new Rectangle(width / 2 + 25, height / 2 + 10, 25, (int)nameSize.Y), _style, "-");            
            soundMinus.AButtonAction = () =>
            {
                Settings.SetSoundVolume(Settings.SoundVolume - 0.1f);
                sound.SetText("Звук: " + (int)(Settings.SoundVolume * 100));
            };
            settingsMenu.Add(soundMinus);
            Button soundPlus = new Button(new Rectangle(width / 2 + 50, height / 2 + 10, 25, (int)nameSize.Y), _style, "+");  
            soundPlus.AButtonAction = () =>
            {
                Settings.SetSoundVolume(Settings.SoundVolume + 0.1f);
                sound.SetText("Звук: " + (int)(Settings.SoundVolume * 100));
            };          
            settingsMenu.Add(soundPlus);
            Label music = new Label(new Rectangle(width / 2 - 75, height / 2 + 12 + (int)nameSize.Y, 100, (int)nameSize.Y), _style, "Музыка: " + (int)(Settings.MusicVolume * 100)); 
            settingsMenu.Add(music);
            Button musicMinus = new Button(new Rectangle(width / 2 + 25, height / 2 + 12 + (int)nameSize.Y, 25, (int)nameSize.Y), _style, "-");            
            musicMinus.AButtonAction = () =>
            {
                Settings.SetMusicVolume(Settings.MusicVolume - 0.1f);
                music.SetText("Музыка: " + (int)(Settings.MusicVolume * 100));
            };
            settingsMenu.Add(musicMinus);
            Button musicPlus = new Button(new Rectangle(width / 2 + 50, height / 2 + 12 + (int)nameSize.Y, 25, (int)nameSize.Y), _style, "+");  
            musicPlus.AButtonAction = () =>
            {
                Settings.SetMusicVolume(Settings.MusicVolume + 0.1f);
                music.SetText("Музыка: " + (int)(Settings.MusicVolume * 100));
            };          
            settingsMenu.Add(musicPlus);

            Label res = new Label(new Rectangle(width / 2 - 50, height / 2 + 17  + (int)nameSize.Y * 2, 100, (int)nameSize.Y), _style, _resolutions[_resolutionPointer].ToString()); 
            settingsMenu.Add(res);
            Button lowRes = new Button(new Rectangle(width / 2 - 75, height / 2 + 17  + (int)nameSize.Y * 2, 25, (int)nameSize.Y), _style, "[<]");            
            lowRes.AButtonAction = () =>
            {
                _resolutionPointer--;
                _resolutionPointer = _resolutionPointer < 0? 0 : _resolutionPointer;
                Settings.SetResolution(_graphics, _camera, _resolutions[_resolutionPointer]);
                res.SetText(_resolutions[_resolutionPointer].ToString()); 
                Thread.Sleep(1000); // Костыль потому что разрешение экрана не успевает обновиться  
                _gui.Shift(Settings.GUIOffset);
            };
            settingsMenu.Add(lowRes);            
            Button upRes = new Button(new Rectangle(width / 2 + 50, height / 2 + 17  + (int)nameSize.Y * 2, 25, (int)nameSize.Y), _style, "[>]");            
            upRes.AButtonAction = () =>
            {
                _resolutionPointer++;
                _resolutionPointer = _resolutionPointer > _resolutions.Length - 1? _resolutions.Length - 1 : _resolutionPointer;
                Settings.SetResolution(_graphics, _camera, _resolutions[_resolutionPointer]);
                res.SetText(_resolutions[_resolutionPointer].ToString());
                Thread.Sleep(1000); // Костыль потому что разрешение экрана не успевает обновиться             
                _gui.Shift(Settings.GUIOffset);               
            };
            settingsMenu.Add(upRes);
            SwitchButton full = new SwitchButton(new Rectangle(width / 2 - 75, height / 2 + 19  + (int)nameSize.Y * 3, 150, (int)nameSize.Y), _style, "На весь экран", Settings.IsFullScreen); 
            full.AButtonAction = () =>
            {
                Settings.SetFullScreen(graphics, camera, !Settings.IsFullScreen);
            };
            settingsMenu.Add(full);
            SwitchButton opening = new SwitchButton(new Rectangle(width / 2 - 75, height / 2 + 24  + (int)nameSize.Y * 4, 150, (int)nameSize.Y), _style, "Начальная заставка", Settings.IsOpenning); 
            opening.AButtonAction = () =>
            {
                Settings.SetOpening(!Settings.IsOpenning);
            };
            settingsMenu.Add(opening);
            SwitchButton devconsole = new SwitchButton(new Rectangle(width / 2 - 75, height / 2 + 29  + (int)nameSize.Y * 5, 150, (int)nameSize.Y), _style, "Консоль разработчика", Settings.IsConsole); 
            devconsole.AButtonAction = () =>
            {
                Settings.SetConsole(!Settings.IsConsole);
            };
            settingsMenu.Add(devconsole);
            Button save = new Button(new Rectangle(width / 2 - 75, height / 2 + 34  + (int)nameSize.Y * 6, 150, (int)nameSize.Y), _style, "Назад");            
            save.AButtonAction = () =>
            {
               Settings.Save(graphics);
               _gui.SetPage("main");           
            };
            settingsMenu.Add(save);
            
            _gui.AddPage("main", main); 
            _gui.AddPage("settings", settingsMenu);
            _gui.SetPage("main"); 
            _gui.Shift(Settings.OriginalGUIOffset);        

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _gui.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (_isContentLoaded)
            {
                _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
                _gui.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
            }
        }
    }
}