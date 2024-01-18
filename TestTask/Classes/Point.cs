using System.Numerics;

namespace TestTask.Classes
{
    public class Point
    {
        public Vector2 Position { get; set; }

        public bool IsObstacle { get; set; }

        public Point()
        {
        }

        public Point(Vector2 position)
        {
            Position = position;
        }
    }
}