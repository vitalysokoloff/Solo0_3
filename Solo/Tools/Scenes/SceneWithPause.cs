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
    public class SceneWithPause : Scene
    { 
        protected bool _pause;
        protected GUI _gui;
        protected int _factor;
        protected KeysInput _input;

        public SceneWithPause(Settings settings, Camera camera, ContentManager content, GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics) : 
            base (settings, camera,  content, graphicsDevice, graphics)
        {            
            _pause = false;
            _factor = 1;
            _input = new KeysInput();
            _input.Add("pause", new Key(Keys.Escape));
            _input.Add("pause", new Key(Buttons.Back));
            Heap game = _settings.Config.GetHeap("game");
            int width = game.GetInt("original-width");
            int height = game.GetInt("original-height");         
            SpriteFont font = SConsole.Font;
            Vector2 textSize = font.MeasureString("  Пауза  ");

            GUIStyle _style = new GUIStyle(_graphics, font);
            
            _gui = new GUI();
           
            Page main = new Page();            
            Label title = new Label(new Rectangle((int)(width / 2 - textSize.X / 2), (int)(height / 2 - textSize.Y), (int)textSize.X, (int)textSize.Y), _style, "  Пауза  ");
            main.Add(title);  
            _gui.AddPage("pause", main);  
            Button play = new Button(new Rectangle(width / 2 - 50, height / 2 + 10, 100, (int)textSize.Y), _style, "Продолжить");            
            play.AButtonAction = () =>
            {
                Pausing();
            };
            main.Add(play);
            Button back = new Button(new Rectangle(width / 2 - 50, height / 2 + 12 + (int)textSize.Y, 100, (int)textSize.Y), _style, "Главное меню");            
            back.AButtonAction = () =>
            {
                ChangeScene?.Invoke(-1);
            };
            main.Add(back);        
        }

        public override void Update(GameTime gameTime)
        {
            if (!_pause)
            {
                base.Update(gameTime);                
            }
            else
            {
                _gui.Update(gameTime);
            }
            if (_input.IsPressed("pause"))
                Pausing();
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isContentLoaded)
            {
                List<IGameObject> drawingGOs = new List<IGameObject>();
                foreach (string k in GOs.Keys)
                {
                    if (_camera.DrawRectangle.Intersects(GOs[k].DrawRect) && GOs[k].IsExist)
                        drawingGOs.Add(GOs[k]);
                }

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
                    _gui.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
            }
            else
            {
                WhileLoadDraw(gameTime, _spriteBatch);
            }
        }

        protected void Pausing()
        {
            _pause = !_pause;
            _gui.Shift(new Point(_settings.OriginalGUIOffset.X * _factor, _settings.OriginalGUIOffset.Y * _factor));
            _factor *= -1;
            if (_pause)
                _gui.SetPage("pause");
            else
                _gui.SetPage(null);
            SConsole.WriteLine("pause:" + _pause);
        }
    } 

}