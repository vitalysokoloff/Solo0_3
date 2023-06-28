namespace Solo.Entities
{
    public interface IPage : IEntity
    {        
        public void Add(string name, IControl control); // Добавляет элемент гуи
        public void Delete(string name); // Удаляет элемент гуи
        public void Clear(); // Очищает страницу от всех гуи
        public string[] GetKeys();
        public void Activate(); // Подписывает все контролы страницы на событие
        public void Deactivate(); // Отписывает все контролы страницы на событие
    }
}