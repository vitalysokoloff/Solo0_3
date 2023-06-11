using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public interface ISprite : IEntity
    {
        public void AnimationStart();
        public void AnimationStop();
        public void AnimationReset();
        public void On();
        public void Off();
        public bool GetState();
        public void OnMove(Vector2 position);
        public void OnRotate(float angle);
        public void Resize(float multiplier);
    }
}