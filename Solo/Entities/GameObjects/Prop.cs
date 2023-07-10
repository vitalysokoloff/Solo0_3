using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    /// <summary>
    /// Это как правило "активные" игровые объекты. Спрайт не заливает всю площадь браша, а является имено что спрайтом
    /// Коллайдер может быть произвольным шейпом.
    /// </summary>
     public interface Prop : IGameObject
    {
    }
}