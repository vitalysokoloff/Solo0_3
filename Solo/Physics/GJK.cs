using Microsoft.Xna.Framework;
using Solo.Entities;

namespace Solo.Physics
{
    public static class GJK
    {
        //https://github.com/kroitor/gjk.c
        /// <summary>
        /// Проверяет столкнулись ли два выпуклых объекта или нет. Используя алгоритм GJK
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CheckCollision(IShape s1, IShape s2)
        {
            int index = 0; // индекс текущей точки симплекса
            Vector2 a, b, c, d, ao, ab, ac, abperp, acperp;
            Vector2[] simplex = new Vector2[3];
            // начальное направление от центра 1-го формы к центру 2-го формы
            d = averagePoint(s1) - averagePoint(s2);
            // если начальное направление равно нулю — устанавливаем его на любую произвольную ось (мы выбираем X)
            if (d == Vector2.Zero)
                d.X = 1;

            a = simplex[0] = support(s1, s2, d);

            if (dotProduct(a, d) <= 0)
            {
                return false; // нет колизии
            }

            d = negate(a); // Следующее направление поиска всегда направлено к исходной точке, поэтому следующее направление поиска является отрицательным (a).

            while (true)
            {
                a = simplex[++index] = support(s1, s2, d);

                if (dotProduct(a, d) <= 0)
                {
                    return false; // нет колизии
                }

                ao = negate(a);

                if (index < 2)
                {
                    b = simplex[0];
                    ab = b - a;
                    d = tripleProduct(ab, ao, ab);
                    if (lengthSquared(d) == 0)
                        d = perpendicular(ab);
                    continue;
                }

                b = simplex[1];
                c = simplex[0];
                ab = b - a;
                ac = c - a;

                acperp = tripleProduct(ab, ac, ac);

                if (dotProduct(acperp, ao) >= 0)
                {
                    d = acperp;
                }
                else
                {
                    abperp = tripleProduct(ac, ab, ab);

                    if (dotProduct(abperp, ao) < 0)
                    {
                       return true; // collision                        
                    }                        

                    simplex[0] = simplex[1];

                    d = abperp;
                }

                simplex[1] = simplex[2];
                --index;
            }
        }

        private static Vector2 averagePoint(IShape s1)
        {
            Vector2 avg = new Vector2(0, 0);
            for (int i = 0; i < s1.GetVertiesQty(); i++)
            {
                avg.X += s1.GetGlobalVertex(i).X;
                avg.Y += s1.GetGlobalVertex(i).Y;
            }
            avg.X /= s1.GetVertiesQty();
            avg.Y /= s1.GetVertiesQty();
            return avg;
        }

        private static Vector2 support(IShape s1, IShape s2, Vector2 d)
        {

            // самая дальняя точка первого тела в произвольном направлении
            int i = indexOfFurthestPoint(s1, d);

            // самая дальняя точка второго тела в противоположном направлении
            int j = indexOfFurthestPoint(s2, negate(d));

            // вычитание (сумма Минковского) двух точек, чтобы увидеть, перекрываются ли тела
            return s1.GetGlobalVertex(i) - s2.GetGlobalVertex(j);
        }

        private static int indexOfFurthestPoint(IShape s1, Vector2 d)
        {
            float maxProduct = dotProduct(d, s1.GetGlobalVertex(0));
            int index = 0;
            for (int i = 1; i < s1.GetVertiesQty(); i++)
            {
                float product = dotProduct(d, s1.GetGlobalVertex(i));
                if (product > maxProduct)
                {
                    maxProduct = product;
                    index = i;
                }
            }
            return index;
        }

        private static Vector2 tripleProduct(Vector2 a, Vector2 b, Vector2 c)
        {
            Vector2 r;

            float ac = a.X * c.X + a.Y * c.Y;
            float bc = b.X * c.X + b.Y * c.Y;

            r.X = b.X * ac - a.X * bc;
            r.Y = b.Y * ac - a.Y * bc;
            return r;
        }


        private static Vector2 negate(Vector2 p) { p.X = -p.X; p.Y = -p.Y; return p; }
        private static Vector2 perpendicular(Vector2 v) { return new Vector2(v.Y, -v.X); }
        private static float dotProduct(Vector2 a, Vector2 b) { return a.X * b.X + a.Y * b.Y; }
        private static float dotProduct(Vector2 a, Point b)
        {
            Vector2 v = new Vector2(b.X, b.Y);
            return a.X * v.X + a.Y * v.Y;
        }
        private static float lengthSquared(Vector2 v) { return v.X * v.X + v.Y * v.Y; }
    }
}
