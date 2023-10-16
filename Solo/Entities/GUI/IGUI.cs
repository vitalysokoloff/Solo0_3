using Microsoft.Xna.Framework;
using Solo.Input;

namespace Solo.Entities 
{
    public interface IGUI : IEntity
    {
        public ISInput Input {get;}
        public PlayerIndex Index {get;}
        public event GUIDelegate GUIevent;
        public void AddPage(string name, IPage page); // Добавляет страницу с контролами
        public void DeletePage(string name); // Удаляет страницу с контролами
        public string[] GetKeys();
        public void SetPage(string name); // Устанавливает указанную страницу активной
        public IPage GetPage(string name); // Возвращает указанную страницу
        public void Shift(Point offset);
    }
}