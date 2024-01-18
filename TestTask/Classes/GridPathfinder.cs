using System.Collections.Generic;

namespace TestTask.Classes
{
    public class GridPathfinder
    {
        public Path BFS(Point startPoint, Point endPoint, Graph graph)
        {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(startPoint);

            var path = new Path();

            while (queue.Count > 0)
            {
                var currentPoint = queue.Dequeue();
                if (currentPoint.Position == endPoint.Position)
                    return CreatureWay(path, startPoint, endPoint);
                var nextPoints = graph.VectorGraph[SearchPointInGraph(currentPoint, graph)];
                foreach (var nextNode in nextPoints)
                {
                    if (!path.VisitedPoints.ContainsKey(nextNode))
                    {
                        queue.Enqueue(nextNode);
                        path.VisitedPoints[nextNode] = currentPoint;
                    }
                }
            }

            return null;
        }

        private Point SearchPointInGraph(Point point, Graph graph)
        {
            foreach (var visitedPoint in graph.VectorGraph.Keys)
            {
                if (visitedPoint.Position == point.Position)
                    return visitedPoint;
            }
            return point;
        }

        private Point SearchPointInPath(Point point, Path path)
        {
            foreach (var visitedPoint in path.VisitedPoints.Keys)
            {
                if (visitedPoint.Position == point.Position)
                    return visitedPoint;
            }
            return point;
        }

        private Path CreatureWay(Path path, Point startPoint, Point endPoint)
        {
            var currentPoint = endPoint;
            while (currentPoint != startPoint)
            {
                currentPoint = path.VisitedPoints[SearchPointInPath(currentPoint, path)];
                path.WayPoints.Add(currentPoint);
            }
            return path;
        }
    }
}