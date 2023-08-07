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
        public int FramesQty { get; set;}
        public int FrameNumber { get; set;}
        
        public Timer AnimationTimer { get; set;}
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
            FramesQty = 1;
            FrameNumber = 0;
            AnimationTimer = Timer.GetDefault();
            Layer = 1f;            
            On();
        }

        public Sprite(SMaterial material, Vector2 position, Point size)
        {
            _texture = material.Texture;
            SpriteColor = Color.White;
            _sourceRectangle = material.SourceRectangle;
            _position = position;
            _angle = 0;
            _size = size;
            _pivot = new Vector2(_size.X / 2, _size.Y / 2);
            _drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y);            
            FramesQty = 1;
            FrameNumber = 0;
            AnimationTimer = Timer.GetDefault();
            Layer = 1f;            
            On();
        }

        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Point size, int frameNumber, int framesQty, Timer animationTimer, bool startAnimationInitially)
        {
            _texture = texture;
            SpriteColor = Color.White;
            _sourceRectangle = sourceRectangle;
            _position = position;
            _angle = 0;
            _size = size;
            _pivot = new Vector2(_size.X / 2, _size.Y / 2);
            _drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y);            
            FramesQty = framesQty;
            FrameNumber = frameNumber;
            AnimationTimer = animationTimer;
            Layer = 1f;
            On();

            if (startAnimationInitially)
                AnimationStart();
        }

        public Sprite(SMaterial material, Vector2 position, Point size, int frameNumber, int framesQty, Timer animationTimer, bool startAnimationInitially)
        {
            _texture = material.Texture;
            SpriteColor = Color.White;
            _sourceRectangle = material.SourceRectangle;
            _position = position;
            _angle = 0;
            _size = size;
            _pivot = new Vector2(_size.X / 2, _size.Y / 2);
            _drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _size.X, _size.Y);            
            FramesQty = framesQty;
            FrameNumber = frameNumber;
            AnimationTimer = animationTimer;
            Layer = 1f;
            On();

            if (startAnimationInitially)
                AnimationStart();
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

        public Point GetSize()
        {
            return _size;
        }

        public void Resize(float multiplier)
        {
            _drawRectangle.Width = (int)(_size.X * multiplier);
            _drawRectangle.Height = (int)(_size.Y * multiplier);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (AnimationTimer.Beap(gameTime))
                FrameMoveRight();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_state)
                spriteBatch.Draw(_texture, _drawRectangle, _sourceRectangle, SpriteColor, _angle, _pivot, SpriteEffects.None, Layer);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Point delta)
        {
            if (_state)
            {
                Rectangle _new = new Rectangle(_drawRectangle.X + delta.X, _drawRectangle.Y + delta.Y, _drawRectangle.Width, _drawRectangle.Height);
                spriteBatch.Draw(_texture, _new, _sourceRectangle, SpriteColor, _angle, _pivot, SpriteEffects.None, Layer);
            }
        }

        protected void FrameMoveRight()
        {
            if (FrameNumber < FramesQty)
                FrameNumber++;
            if (FrameNumber >= FramesQty)
                FrameNumber = 0;

            _sourceRectangle.X = FrameNumber * _size.X;
        }

        protected void FrameMoveLeft()
        {
            if (FrameNumber >= 0)
                FrameNumber++;
            if (FrameNumber < 0)
                FrameNumber = FramesQty;

            _sourceRectangle.X = FrameNumber * _size.X;
        }
    }
}