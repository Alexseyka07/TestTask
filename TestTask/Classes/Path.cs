using System.Collections.Generic;

namespace TestTask.Classes
{
    public class Path
    {
        public Dictionary<Point, Point> VisitedPoints { get; set; } = new Dictionary<Point, Point>();

        public List<Point> WayPoints { get; set; } = new List<Point>();
    }
}