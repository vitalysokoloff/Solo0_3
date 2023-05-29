using System;
using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public class SRegularPolygon : Shape
    {
        protected float _radius;
        protected int _length;
        /// <summary>
        /// Правильный многоугольник
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="z">Количество вершин</param>
        /// <param name="w">Радиус</param>
        public SRegularPolygon(int x, int y, int w, int z) : base(x, y, w, z)
        {
            _length = z;
            _radius = w;
            _baseVerties = new Vector2[_length];
            _verties = new Vector2[_length];
            _size = new Vector2(_radius * 2, _radius * 2);
            _pivot = new Vector2(_radius, _radius);
            _position = new Vector2(x, y);
            _drawRectangle = new Rectangle((int)X, (int)Y, (int)_size.X, (int)_size.Y);
            _sourceRectangle = new Rectangle(0, 0, (int)_size.X, (int)_size.Y);
            SetBaseVerties();
        }

        protected override void SetBaseVerties()
        {
            // Используется радиус описанной окружности
            float a = (float)(360 * Math.PI / 180) / _length ; // угол альфа, через который друг от друга находятся вершины правильного многоугольника

            for (int i = 0; i < _length ; i++)
            {
                _baseVerties[i] = new Vector2( // получаем координаты вершин x=x0+r*cosA, y=y0+r*sinA 
                    (float)(_radius * Math.Cos(0 + a * i)),
                    (float)(_radius * Math.Sin(0 + a * i))
                    );
                _verties[i] = _baseVerties[i];
            }
        } 
    }
}