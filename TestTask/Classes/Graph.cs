using System.Collections.Generic;

namespace TestTask.Classes
{
    public class Graph
    {
        public Dictionary<Point, List<Point>> VectorGraph { get; set; } = new Dictionary<Point, List<Point>>();
    }
}