using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestTask
{
    public partial class MainWindow : Window
    {
        //количество строк и столбцов
        private static int n = 15;     
        private Button[,] buttons = new Button[n, n]; //массив содержит все клетки поля
        private Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>(); //словарь содержит в себе граф, где ключ - это имя, а значение соседие клетки ключа

        public MainWindow()
        {
            InitializeComponent();
            GeneratedMap();
        }

        private void GeneratedMap()
        {
            //создание клеток
            int num = 0;
            for (int i = 0; i < n; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            } 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Button button = new Button()
                    {
                        Name = $"Bt_{num}",
                    };
                    button.Click += Button_Click;
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                    mainGrid.Children.Add(button);
                    buttons[i, j] = button;
                    num++;
                }
            }
            Random random = new Random();
            for (int i = 0; i < n * n / 3; i++)
            {
                string name = $"Bt_{random.Next(5, n * n)}";
                foreach (var button in buttons)
                {
                    if (button.Name == name) 
                    {
                        button.IsHitTestVisible = false;
                        button.Background = Brushes.Black; break; 
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    List<string> neighbors = new List<string>();
                    if(j + 1 >= 0 && j < n-1)
                        if (buttons[i, j + 1].Background != Brushes.Black)
                            neighbors.Add(buttons[i, j + 1].Name);
                    if (j - 1 >= 0 && j < n - 1)
                        if (buttons[i, j - 1].Background != Brushes.Black)
                            neighbors.Add(buttons[i, j - 1].Name);
                    if (i + 1 >= 0 && i < n - 1)
                        if (buttons[i + 1, j].Background != Brushes.Black)
                            neighbors.Add(buttons[i + 1, j].Name);
                    if (i - 1 >= 0 && i < n - 1)
                        if (buttons[i - 1, j].Background != Brushes.Black)
                            neighbors.Add(buttons[i - 1, j].Name);
                   
                    graph.Add(buttons[i, j].Name, neighbors);
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonEnd = (Button)sender;

            
            string start = buttons[0, 0].Name;
            string end = buttonEnd.Name;

            Dictionary<string, string> visited = BFS(start, end, graph);
            if(visited != null)
            {
                string curNode = end;
                while (curNode != start)
                {
                    curNode = visited[curNode];
                    SetColor(curNode, Brushes.Green);
                }
                SetColor(end, Brushes.GreenYellow);
                SetColor(start, Brushes.DarkSeaGreen);
            }
            else
            {
                MessageBox.Show("Пути нет");
            }
        }

        private static Dictionary<string, string> BFS(string start, string goal, Dictionary<string, List<string>> graph)
        {
           
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(start);

            Dictionary<string, string> visited = new Dictionary<string, string>();

            while (queue.Count > 0)
            {
                string curNode = queue.Dequeue();
                if (curNode == goal)
                {
                    return visited;
                }
                    

                List<string> nextNodes = graph[curNode];
                foreach (string nextNode in nextNodes)
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

        private void SetColor(string buttonName, SolidColorBrush color)
        {
            foreach (var button in buttons)
            {
                if (button.Name == buttonName)
                {
                    button.Background = color;
                    break;
                }
            }
        }
    }
}