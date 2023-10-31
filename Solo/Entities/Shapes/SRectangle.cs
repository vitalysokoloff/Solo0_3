using Microsoft.Xna.Framework;

namespace Solo.Entities 
{
    public class SRectangle : Shape
    {
        /// <summary>
        /// Прямоугольник
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="w">Ширина</param>
        /// <param name="z">Высота</param>
       public SRectangle(int x, int y, int w, int z) : base(x, y, w, z)
       {
            _baseVerties = new Vector2[4];
            _verties = new Vector2[4];
            _size = new Vector2(w, z);
            _pivot = new Vector2(w / 2, z / 2);
            Position = new Vector2(x, y);
            _drawRectangle = new Rectangle((int)(Position.X), (int)(Position.Y), w + 1, z + 1);
            _sourceRectangle = new Rectangle(0, 0, w + 1, z + 1);
            SetBaseVerties();
       }

        public SRectangle(Rectangle rect) : base(rect)
        {
            int x = rect.X;
            int y = rect.Y;
            int w = rect.Width;
            int z = rect.Height;
            _baseVerties = new Vector2[4];
            _verties = new Vector2[4];
            _size = new Vector2(w, z);
            _pivot = new Vector2(w / 2, z / 2);
            Position = new Vector2(x, y);
            _drawRectangle = new Rectangle((int)(Position.X), (int)(Position.Y), w + 1, z + 1);
            _sourceRectangle = new Rectangle(0, 0, w + 1, z + 1);
            SetBaseVerties();
        }

       protected override void SetBaseVerties()
        {
            _baseVerties[0] = Vector2.Zero - _pivot;
            _baseVerties[1] = new Vector2(_size.X, 0) - _pivot;
            _baseVerties[2] = new Vector2(_size.X, _size.Y) - _pivot;
            _baseVerties[3] = new Vector2(0, _size.Y) - _pivot;
            _verties[0] = _baseVerties[0];
            _verties[1] = _baseVerties[1];
            _verties[2] = _baseVerties[2];
            _verties[3] = _baseVerties[3];
        }
    }
}