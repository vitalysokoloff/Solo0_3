using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    /// <summary>
    /// Реализует игровой обект браш, это к примеру стены, фоны декорации, спрайт у брашей "замащивает"(заливает) весь игровой обект.
    /// Браш имеет прямоугольный коллайдер размером с сам браш.
    /// </summary>
     public interface Brush : IGameObject
    {
    }
}