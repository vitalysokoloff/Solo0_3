using System.Collections.Generic;
using System.Linq;
using Solo.Entities;

namespace Solo
{
    public class ColliderManager
    {
        protected List<IGameObject> _actors;
        public ColliderManager()
        {
            _actors = new List<IGameObject>();
        }

        public void AddActor(IGameObject go)
        {
            if (!_actors.Contains(go))
                _actors.Add(go);
        }

        public void DeleteActor(IGameObject go)
        {
            if (_actors.Contains(go))
                _actors.Remove(go);
        }

        public void Colliding(List<IGameObject> GOList)
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                for (int j = 0; j < GOList.Count; j++)
                {
                    if (GOList[j] != _actors[i] && GOList[j].Collider != null)
                    {
                        _actors[i].CheckCollision(GOList[j]); // Реализовать это в браше, Реализовать это в пропе. В меотде обработка и получение информации о столкновении, 
                                                              // если есть столкновение, то вызов метода онКолайд и передача ему данных о объекте с которым столкнулся.
                    }
                }
            }
        }
    }
}