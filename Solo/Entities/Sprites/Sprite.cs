using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Solo;

namespace Solo.Entities 
{
    public class Sprite : ISprite
    {
        public float Layer { get; set; }
        public IGameObject Parent 
        { 
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
                _parent.MoveEvent += OnMove;
                _parent.RotateEvent += OnRotate;
                _drawRectangle.X = (int)(_position.X + _parent.Postion.X);
                _drawRectangle.Y = (int)(_position.X + _parent.Postion.Y);
            }
        }
        
        public Timer AnimationTimer;
        public Color SpriteColor;        

        protected IGameObject _parent;
        protected Texture2D _texture;
        protected bool _state;
        protected Rectangle _sourceRectangle;
        protected Rectangle _drawRectangle;
        protected Vector2 _position;        
        protected Point _size;
        protected Vector2 _pivot;
        protected float _angle;
        protected int _framesQty;
        protected int _frameNumber;

        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Point size)
        {
            _texture = texture;
            SpriteColor = Color.White;
            _sourceRectangle = sourceRectangle;
            _position = position;
            _angle = 0;
            _size = size;
            _pivot = new Vector2(_size.X / 2, _size.Y / 2);
            _drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y);            
            _framesQty = 1;
            _frameNumber = 0;
            Layer = 1f;
            On();
        }

        public void AnimationStart()
        {
            AnimationTimer.Start();
        }

        public void AnimationStop()
        {
            AnimationTimer.Stop();
        }

        public void AnimationReset()
        {
            AnimationTimer.Reset();
        }

        public void OnMove(Vector2 position)
        {
            _drawRectangle.X = (int)(position.X + _position.X);
            _drawRectangle.Y = (int)(position.Y + _position.Y);
        }

        public void OnRotate(float angle)
        {
            _angle = angle;
        }

         public void On()
        {
            _state = true;
        }

        public void Off()
        {
            _state = false;
        }

        public bool GetState()
        {
            return _state;
        }

        public void Resize(float multiplier)
        {
            _drawRectangle.Width = (int)(_size.X * multiplier);
            _drawRectangle.Height = (int)(_size.Y * multiplier);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_state)
                spriteBatch.Draw(_texture, _drawRectangle, _sourceRectangle, SpriteColor, _angle, _pivot, SpriteEffects.None, Layer);
        }

        protected void FrameMoveRight()
        {
            if (_frameNumber < _framesQty)
                _frameNumber++;
            if (_frameNumber >= _framesQty)
                _frameNumber = 0;

            _sourceRectangle.X = _frameNumber * _size.X;
        }

        protected void FrameMoveLeft()
        {
            if (_frameNumber >= 0)
                _frameNumber++;
            if (_frameNumber < 0)
                _frameNumber = _framesQty;

            _sourceRectangle.X = _frameNumber * _size.X;
        }
    }
}