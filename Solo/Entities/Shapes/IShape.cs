using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface IShape : IEntity
    {
        public Vector2 GetGlobalVertex(int number);
        public int GetVertiesQty();
    }
}