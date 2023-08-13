using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Solo.Entities 
{
    public abstract class Shape : IShape
    {
        /// <summary>
        /// Координаты центра фигуры
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _position;
            } 
            set
            {
                _position = value;
                _drawRectangle.X = (int)_position.X;
                _drawRectangle.Y = (int)_position.Y;
            }
        }
        public float X 
        {
            get
            {
                return _position.X;
            } 
            set
            {
                _position.X = value;
                _drawRectangle.X = (int)_position.X;
            }
        }
        public float Y 
        {
            get
            {
                return _position.Y;
            } 
            set
            {
                _position.Y = value;
                _drawRectangle.Y = (int)_position.Y;
            }
        }
        /// <summary>
        /// Угол в радианах
        /// </summary>
        public float Angle
        {
            get
            {
                return _angle;
            }
            set
            {
                _angle = value;
                RotatePoints();
            }
        }

        protected Vector2 _position;
        protected Vector2[] _verties;
        protected Vector2[] _baseVerties;
        protected float _angle;
        protected Color _color;
        protected Vector2 _size;
        protected Vector2 _pivot;
        protected Rectangle _drawRectangle; 
        protected Rectangle _sourceRectangle;
        protected Texture2D _texture;

        public Shape(int x, int y, int w, int z)
        {
            _color = Color.White;
            _angle = 0f;
        }

        public virtual void Update(GameTime gameTime)
        {}
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, _drawRectangle, _sourceRectangle, _color, _angle, _pivot, SpriteEffects.None, 1f);
            }
        }
        public Vector2 GetGlobalVertex(int number)
        {
            return _verties[number] + Position;
        }
        public int GetVertiesQty()
        {
            return _verties.Length;
        }
        public void SetColor(Color color)
        {
            _color = color;            
        }
        public void SetTexture(GraphicsDeviceManager graphics)
        {
            GenerateTexture(graphics);
        }
        protected virtual void SetBaseVerties() 
        {}

        protected void RotatePoints()
        {
            for (int i = 0; i < _verties.Length; i++)
                _verties[i] = new Vector2(
                (float)(_baseVerties[i].X  * Math.Cos(_angle) - _baseVerties[i].Y * Math.Sin(_angle)),
                (float)(_baseVerties[i].X  * Math.Sin(_angle) + _baseVerties[i].Y * Math.Cos(_angle))
                );
        }        
        protected virtual void GenerateTexture(GraphicsDeviceManager graphics)
        {           
            _texture = new Texture2D(graphics.GraphicsDevice, (int)_size.X + 2, (int)_size.Y + 2);
            Color[] data = new Color[_texture.Width * _texture.Height];
            _texture.SetData(data);
            for (int i = 0; i < _verties.Length - 1; i++)
                Tools.DrawLine(_texture, _color, _verties[i] + _pivot, _verties[i + 1] + _pivot);
            Tools.DrawLine(_texture, _color, _verties[_verties.Length - 1] + _pivot, _verties[0] + _pivot);
        }
    }
}