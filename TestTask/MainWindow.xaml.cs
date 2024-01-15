using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestTask
{
    public partial class MainWindow : Window
    {
        private static int n = 15; //количество строк и столбцов
        private Button[,] buttons = new Button[n, n];
        private Dictionary<Button, List<Button>> graph = new Dictionary<Button, List<Button>>();

        public MainWindow()
        {
            InitializeComponent();
            GeneratedMap();
        }

        private void GeneratedMap()
        {
            //создание клеток
            for (int i = 0; i < n; i++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                mainGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < n; j++)
                {
                    Button button = new Button();
                    button.Click += Button_Click;
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                    mainGrid.Children.Add(button);
                    buttons[i, j] = button;
                }
            }
            for (int i = 0; i < n * n / 3; i++)
            {
                Random random = new Random();
                buttons[random.Next(0, n), random.Next(0, n)].Background = Brushes.Black;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    List<Button> neighbors = new List<Button>();
                    if (j + 1 >= 0 && j < n - 1)
                        if (buttons[i, j + 1].Background != Brushes.Black)
                            neighbors.Add(buttons[i, j + 1]);
                    if (j - 1 >= 0 && j < n - 1)
                        if (buttons[i, j - 1].Background != Brushes.Black)
                            neighbors.Add(buttons[i, j - 1]);
                    if (i + 1 >= 0 && i < n - 1)
                        if (buttons[i + 1, j].Background != Brushes.Black)
                            neighbors.Add(buttons[i + 1, j]);
                    if (i - 1 >= 0 && i < n - 1)
                        if (buttons[i - 1, j].Background != Brushes.Black)
                            neighbors.Add(buttons[i - 1, j]);

                    graph.Add(buttons[i, j], neighbors);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonEnd = (Button)sender;

            Button startPoint = buttons[0, 0];
            Button endPoint = buttonEnd;

            Dictionary<Button, Button> visited = BFS(startPoint, endPoint);
            DrawWay(visited, startPoint, endPoint);
        }

        private Dictionary<Button, Button> BFS(Button startPoint, Button endPoint)
        {
            Queue<Button> queue = new Queue<Button>();
            queue.Enqueue(startPoint);

            Dictionary<Button, Button> visited = new Dictionary<Button, Button>();

            while (queue.Count > 0)
            {
                Button curNode = queue.Dequeue();
                if (curNode == endPoint)
                    return visited;

                List<Button> nextNodes = graph[curNode];
                foreach (Button nextNode in nextNodes)
                {
                    if (!visited.ContainsKey(nextNode))
                    {
                        queue.Enqueue(nextNode);
                        visited[nextNode] = curNode;
                    }
                }
            }

            return null;
        }

        private void DrawWay(Dictionary<Button, Button> visited, Button startPoint, Button endPoint)
        {
            if (visited != null)
            {
                Button curNode = endPoint;
                while (curNode != startPoint)
                {
                    curNode = visited[curNode];
                    SetColor(curNode, Brushes.Green);
                }
                SetColor(endPoint, Brushes.GreenYellow);
                SetColor(startPoint, Brushes.DarkSeaGreen);
            }
            else
            {
                MessageBox.Show("Пути нет");
            }
        }

        private void SetColor(Button button, SolidColorBrush color)
        {
            button.Background = color;
        }
    }
}