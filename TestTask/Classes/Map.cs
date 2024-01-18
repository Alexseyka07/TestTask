using System;
using System.Collections.Generic;
using System.Numerics;

namespace TestTask.Classes
{
    public class Map
    {
        private int count;

        public Point[,] GeneratedMap(int count)
        {
            this.count = count;
            Point[,] points = new Point[count, count];
            for (int x = 0; x < count; x++)
            {
                for (int y = 0; y < count; y++)
                {
                    Point point = new Point()
                    {
                        Position = new Vector2(x, y)
                    };
                    points[x, y] = point;
                }
            }
            return points;
        }

        public Point[,] SetObstacles(Point[,] points, int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                AddObstacle(points[random.Next(0, this.count), random.Next(0, this.count)]);
            }
            return points;
        }

        public Graph InitializeGraph(Point[,] points)
        {
            var graph = new Graph();
            for (int x = 0; x < count; x++)
            {
                for (int y = 0; y < count; y++)
                {
                    var neighbors = new List<Point>();
                    if (y + 1 >= 0 && y < count - 1)
                        if (points[x, y + 1].IsObstacle != true)
                            neighbors.Add(new Point(new Vector2(x, y + 1)));
                    if (y - 1 >= 0 && y < count - 1)
                        if (points[x, y - 1].IsObstacle != true)
                            neighbors.Add(new Point(new Vector2(x, y - 1)));
                    if (x + 1 >= 0 && x < count - 1)
                        if (points[x + 1, y].IsObstacle != true)
                            neighbors.Add(new Point(new Vector2(x + 1, y)));
                    if (x - 1 >= 0 && x < count - 1)
                        if (points[x - 1, y].IsObstacle != true)
                            neighbors.Add(new Point(new Vector2(x - 1, y)));

                    graph.VectorGraph.Add(points[x, y], neighbors);
                }
            }
            return graph;
        }

        private void AddObstacle(Point point)
        {
            point.IsObstacle = true;
        }
    }
}