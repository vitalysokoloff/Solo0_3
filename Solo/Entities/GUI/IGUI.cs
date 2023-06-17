using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface IGUI : IEntity
    {
        public void AddPage(string name); // Добавляет страницу с контролами
        public void DeletePage(string name); // Удаляет страницу с контролами
        public void SetPage(string name); // Устанавливает указанную страницу активной
        public void GetPage(string name); // Возвращает указанную страницу
    }
}