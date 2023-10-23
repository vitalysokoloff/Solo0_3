using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface ICollider : IEntity
    {
        // Колайдер перемещает и вертит шейп, генерирует текстуру шейпа
        public IGameObject Parent {get; set;}
        public void On();
        public void Off();
         public void SetTexture(GraphicsDeviceManager graphics);
        public bool GetState();
        public void OnMove(Vector2 position);
        public void OnRotate(float angle);
        public Shape GetShape();        
    }
}