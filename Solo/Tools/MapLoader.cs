using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Solo.Collections;
using Solo.Entities;

namespace Solo
{
    public class MapLoader
    {
        public event LoadStageEvent StageEvent;
        public GOMaker MakeGameObjects;

        protected Scene _scene;
        protected Heap _map;
        protected string _path;

        public MapLoader(Scene scene, string path)
        {
            _scene = scene;
            _path = path;
        }

        public void Load()
        {
            // Видимо нужно это делать в отдельном потоке
            // Чтени хип файла стаге 0
            StageEvent?.Invoke(0);
            _map = Heap.Open(_path);            
            
            // Загрузка тестур стаге 1
            StageEvent?.Invoke(1);
            Heap texturePathes = _map.GetHeap("textures");
            foreach (string k in texturePathes.GetStringsKeys())
            {
                Texture2D texture = _scene.Content.Load<Texture2D>(_scene.TexturesDirectory + "\\" + texturePathes.GetString(k));
                _scene.Textures.Add(k, texture);
            }
            
            // Загрузка звуков стаге 2
            StageEvent?.Invoke(2);
            Heap soundPathes = _map.GetHeap("sounds");
            foreach (string k in soundPathes.GetStringsKeys())
            {
                SoundEffect sound = _scene.Content.Load<SoundEffect>(_scene.AudioDirectory + "\\" + soundPathes.GetString(k));
                _scene.Sounds.Add(k, sound);
            }
            
            // Создание метриалов стаге 3
            StageEvent?.Invoke(3);
            Heap materialInfos = _map.GetHeap("materials");
            foreach(string k in materialInfos.GetHeapKeys())
            {
                Heap materialInfo = materialInfos.GetHeap(k);
                SMaterial material = new SMaterial()
                {
                    Texture = _scene.Textures[materialInfo.GetString("texture")], // проверка на нулл?
                    SourceRectangle = new Rectangle(materialInfo.GetPoint("location"), materialInfo.GetPoint("size")), 
                    Sound = _scene.Sounds[materialInfo.GetString("sound")] // проверка на нулл?
                };
                _scene.Materials.Add(k, material);
            }
            
            // Создание игровых объектов стаге 4
            StageEvent?.Invoke(4);
            MakeGameObjects();
            StageEvent?.Invoke(5);
            // Всё готово стаге 5
        }       

        protected void AddGO(IGameObject go)
        {
            _scene.GOs.Add(go.Name, go);
        }
    }

    public delegate void LoadStageEvent(int stage);
    public delegate void GOMaker();
}