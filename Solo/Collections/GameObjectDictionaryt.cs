using System.Collections.Generic;
using Solo.Entities;

namespace Solo.Collections
{
    public class GameObjectDictionary
    {
        protected Dictionary<string, IGameObject> _list;

        public GameObjectDictionary(Dictionary<string, IGameObject> list)
        {
            _list = list;
        }

        public GameObjectDictionary()
        {
            _list = new Dictionary<string, IGameObject>();
        }

        //public void Add() добавить игровой объект
        // получить игровой объект
        // получить список всех ключей
    }
}