using Microsoft.Xna.Framework;
using Solo.Entities;

namespace Solo.Entities 
{
    public interface IGUI : IEntity
    {
        public event GUIDelegate GUIevent;
        public void AddPage(string name, IPage page); // Добавляет страницу с контролами
        public void DeletePage(string name); // Удаляет страницу с контролами
        public void SetPage(string name); // Устанавливает указанную страницу активной
        public void GetPage(string name); // Возвращает указанную страницу
    }
}