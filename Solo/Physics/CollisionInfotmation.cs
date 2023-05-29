using Microsoft.Xna.Framework;
using Solo.Entities;

namespace Solo.Physics
{
    public static class CollisionInformation
    {
        /// <summary>
        /// Возвращает нормаль ближайшей грани s2 к центру s1, если у двух и более граней растояние до центра равно, то суммирует нормали и 
        /// возвращает суму векторов (нормалей) и приводит её (сумму) к единичному виду.
        /// </summary> 
        public static Vector2 GetNormal(Shape s1, Shape s2)
        {
            Edge edge;
            Vector2 normal = Vector2.Zero;
            Vector2 center = s1.Position;
            int length = s2.GetVertiesQty();
            float nearestDistance = Tools.DistanceBetweenVertices(s2.Position, center);            
            float distance = 0;

            for (int i = 0; i < length - 1; i++)
            {
                edge = new Edge(s2.GetGlobalVertex(i), s2.GetGlobalVertex(i + 1));
                distance = Tools.DistanceBetweenVertices(edge.Middle, center);
                if (distance < nearestDistance)
                    nearestDistance = distance;
            }
            edge = new Edge(s2.GetGlobalVertex(length - 1), s2.GetGlobalVertex(0));
            distance = Tools.DistanceBetweenVertices(edge.Middle, center);
            if (distance < nearestDistance)
                nearestDistance = distance;

            for (int i = 0; i < length - 1; i++)
            {
                edge = new Edge(s2.GetGlobalVertex(i), s2.GetGlobalVertex(i + 1));
                distance = Tools.DistanceBetweenVertices(edge.Middle, center);
                if (distance == nearestDistance)
                    normal += edge.Normal;
            }
            edge = new Edge(s2.GetGlobalVertex(length - 1), s2.GetGlobalVertex(0));
            distance = Tools.DistanceBetweenVertices(edge.Middle, center);
            if (distance == nearestDistance)
                normal += edge.Normal;

            return Tools.Basis(normal);
        }

        protected class Edge
        {
            public Vector2 A { get; }
            public Vector2 B { get; }
            public Vector2 Middle { get; }
            public Vector2 Normal { get;}

            public Edge(Vector2 a, Vector2 b)
            {
                A = a;
                B = b;
                Middle = new Vector2(((A.X + B.X) / 2), ((A.Y + B.Y) / 2));
                Normal = Tools.EdgeToNormal(A, B);                
            }
        }
    }
}