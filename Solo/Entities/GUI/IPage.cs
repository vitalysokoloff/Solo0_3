using System.Collections.Generic;

namespace Solo.Entities
{
    public interface IPage : IEntity
    {     
        public List<IControl> Controls {get;}
        public IGUI Parent {get; set;}   
        public void Add(IControl control); // Добавляет элемент гуи
        public void Delete(int n); // Удаляет элемент гуи
        public void Clear(); // Очищает страницу от всех гуи
        public void Activate(); // Подписывает все контролы страницы на событие
        public void Deactivate(); // Отписывает все контролы страницы на событие
    }
}