using Microsoft.Xna.Framework;

namespace Solo.Entities
{
    public interface IPage : IEntity
    {        
        public void App(); // Добавляет элемент гуи
        public void Delete(); // Удаляет элемент гуи
        public void Clear(); // Очищает страницу от всех гуи
        public void Activate(); // Подписывает все контролы страницы на событие
        public void Deactivate(); // Отписывает все контролы страницы на событие
    }
}