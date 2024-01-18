using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestTask.Classes;

namespace TestTask
{
    public partial class MainWindow : Window
    {
        private static int count = 50; //количество строк и столбцов
        private Button[,] buttons = new Button[count, count];
        private Graph graph;
        private Classes.Point[,] points;

        public MainWindow()
        {
            InitializeComponent();
            Map map = new Map();
            points = map.GeneratedMap(count);
            points = map.SetObstacles(points, count * count / 3);
            graph = map.InitializeGraph(points);
            DrawGrid();
        }

        private void DrawGrid()
        {
            for (int x = 0; x < count; x++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                mainGrid.RowDefinitions.Add(new RowDefinition());
                for (int y = 0; y < count; y++)
                {
                    Button button = new Button();
                    if (points[x, y].IsObstacle)
                        button.Background = Brushes.Black;
                    button.Click += Button_Click;
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    mainGrid.Children.Add(button);
                    buttons[x, y] = button;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonEnd = (Button)sender;

            Classes.Point startPoint = new Classes.Point(new Vector2(0, 0));
            Classes.Point endPoint = new Classes.Point(GetPositionButton(buttonEnd));

            Searching(startPoint, endPoint);
        }

        private void Searching(Classes.Point startPoint, Classes.Point endPoint)
        {
            GridPathfinder gridPathfinder = new GridPathfinder();

            Path path = gridPathfinder.BFS(startPoint, endPoint, graph);
            if (path != null)
            {
                DrawWay(path, startPoint, endPoint);
            }
            else MessageBox.Show("Пути нет");
        }

        private void DrawWay(Path path, Classes.Point startPoint, Classes.Point endPoint)
        {
            foreach (var point in path.WayPoints)
            {
                SetColor(GetButton(point), Brushes.Green);
            }

            SetColor(GetButton(endPoint), Brushes.GreenYellow);
            SetColor(GetButton(startPoint), Brushes.DarkSeaGreen);
        }

        private void SetColor(Button button, SolidColorBrush color)
        {
            button.Background = color;
        }

        private Button GetButton(Classes.Point point)
        {
            return buttons[(int)point.Position.X, (int)point.Position.Y];
        }

        private Vector2 GetPositionButton(Button buttonEnd)
        {
            for (int x = 0; x < count; x++)
            {
                for (int y = 0; y < count; y++)
                {
                    if (buttons[x, y] == buttonEnd) 
                        return new Vector2(x, y);
                }
            }
            return new Vector2(0, 0);
        }
    }
}