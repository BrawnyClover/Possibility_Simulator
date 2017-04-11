using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Reflection;


namespace GrindingEx
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public struct positionData
    {
        public int x,y;
        public positionData(int tx, int ty)
        {
            x = tx;
            y = ty;
        }
    };

    public partial class MainWindow : Window
    {
        int[] probability = new int[1005];
        positionData[] route = new positionData[5];
        positionData[] data = new positionData[100];
        static int count = 0;
        public int calcX = 0; public int calcY = 0;
        static bool flag = false;
        static bool numflag = false;
        
        
        char[] name = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public MainWindow()
        {
            
            InitializeComponent();
            drawAxis();
            data[0].x = -1; data[0].y = -1;
            route[0].x = 20; route[0].y = 0;
            route[1].x = -20; route[1].y = 0;
            route[2].x = 0; route[2].y = -20;
            route[3].x = 0; route[3].y = 20;
            scrollViewer.ScrollToVerticalOffset(15865);
            scrollViewer.ScrollToHorizontalOffset(15850);
            Plotter.MouseDown += new MouseButtonEventHandler(Plotter_MouseDown);

        }
        void drawCircle(int x, int y, Brush br)
        {
            Ellipse myEllipse = new Ellipse();
            myEllipse.Margin = new Thickness(x-5, y-5, 0, 0);

            myEllipse.Width = 12;
            myEllipse.Height = 12;
            myEllipse.Fill = br;
            Plotter.Children.Add(myEllipse);
        }
            void drawCircle(int x, int y)
        {
            Random r = new Random();
            var values = typeof(Brushes).GetProperties().
            Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();
            var brushNames = values.Select(v => v.Name);
            Random rnd = new Random(Guid.NewGuid().ToString().GetHashCode());
            Brush br = PickRandomBrush(rnd);

            Ellipse myEllipse = new Ellipse();
            myEllipse.Margin = new Thickness(x + 2.5, y + 2.5, 0, 0);

            myEllipse.Width = 15;
            myEllipse.Height = 15;
            myEllipse.Fill = br;
            myEllipse.Name = name[count].ToString();
            TextBlock ellName = new TextBlock();
            ellName.Margin = new Thickness(x + 9, y + 5, 0, 0);
            ellName.Text = myEllipse.Name;
            Plotter.Children.Add(myEllipse);
            Plotter.Children.Add(ellName);
        }

        void Plotter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (flag == true)
            {
                MessageBoxResult result = MessageBox.Show("click denied!", "Click Button First!", MessageBoxButton.OKCancel);
                return;
            }

            Point prePosition;
            prePosition = e.GetPosition(Plotter);
            position.Text = "x : " + prePosition.X.ToString() +"\n"+ "y : " + prePosition.Y.ToString();
            calcX = (((int)Math.Round(prePosition.X) / 20) * 20);
            calcY = (((int)Math.Round(prePosition.Y) / 20) * 20);
            positionC.Text = "x : " + calcX.ToString() + " y : " + calcY.ToString();
            positionData temp = new positionData(calcX, calcY);

            var t = find(temp);

            if (t == 0)
            {
                drawCircle(calcX, calcY);
                data[count].x = calcX;
                data[count].y = calcY;
                count++;
                num.Text = count.ToString();
                flag = true;
            }
            else
            {

            }

        }
        private void drawAxis()
        {

            for (int i = 10; i <= Plotter.Width; i += 20)
            {
                Line l = new Line();
                l.X1 = 0; l.Y1 = i;

                l.X2 = Plotter.Width; l.Y2 = i;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 1;
                Plotter.Children.Add(l);
            }
            for (int i = 10; i <= Plotter.Height; i += 20)
            {
                Line l = new Line();
                l.X1 = i; l.Y1 = 0;

                l.X2 = i; l.Y2 = Plotter.Height;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 1;
                Plotter.Children.Add(l);
            }
            Line m = new Line();
            m.X1 = 0;
            m.Y1 = Plotter.Height / 2+10;
            m.X2 = Plotter.Width;
            m.Y2 = Plotter.Height / 2+10;
            m.Stroke = Brushes.Black;
            m.StrokeThickness = 2;
            Plotter.Children.Add(m);
            Line n = new Line();
            n.X1 = Plotter.Width / 2+10;
            n.Y1 = 0;
            n.X2 = Plotter.Width / 2+10;
            n.Y2 = Plotter.Height;
            n.Stroke = Brushes.Black;
            n.StrokeThickness = 2;
            Plotter.Children.Add(n);
        }
        int find(positionData temp)
        {
            int i;
            for (i = 0; i < count; i++)
            {
                if (data[i].x == temp.x && data[i].y == temp.y) return i + 1;
            }
            return 0;
        }

        void probabilitize(double r, double l, double u, double d)
        {
            int i;
            for (i = 0; i < r * 10; i++)
            {
                probability[i] = 1;
            }
            int t = i;
            for (i = t; i < t + l * 10; i++)
            {
                probability[i] = 2;
            }
            t = i;
            for (i = t; i < t + u * 10; i++)
            {
                probability[i] = 3;
            }
            t = i;
            for (i = t; i < t + d * 10; i++)
            {
                probability[i] = 4;
            }
        }
        private Brush PickRandomBrush(Random rnd)
        {
            Brush result = Brushes.Transparent;
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);
            return result;
        }
        void drawLine(int rx, int ry, int x, int y, int cnt, Brush br, int end)
        {
            
            Line m = new Line();
            m.X1 = rx;
            m.Y1 = ry;
            m.X2 = x;
            m.Y2 = y;
            m.Stroke = br;
            m.StrokeThickness = 4;
            Plotter.Children.Add(m);
            if (numflag == true)
            {
                TextBlock routeCnt = new TextBlock();
                routeCnt.Margin = new Thickness(x, y, 0, 0);
                routeCnt.Text = (cnt + 1).ToString();
                Plotter.Children.Add(routeCnt);
            }
            if (cnt + 1 == end)
            {
                drawCircle(x, y, Brushes.Black);
            }
        }
       
        void walk(int num)
        {
            int i;
            int[] rec = new int[5];
            rec[1] = 0; rec[2] = 0; rec[3] = 0; rec[4] = 0;
            Random r = new Random();
            var values = typeof(Brushes).GetProperties().
            Select(p => new { Name = p.Name, Brush = p.GetValue(null) as Brush }).ToArray();
            var brushNames = values.Select(v => v.Name);
            int rx = calcX + 10;
            int ry = calcY + 10;
            Random rnd = new Random(Guid.NewGuid().ToString().GetHashCode());
            Brush br = PickRandomBrush(rnd);
            for (i = 0; i < num; i++)
            {
                int temp = r.Next(1, 1000);
                drawLine(rx, ry, rx + route[probability[temp] - 1].x, ry + route[probability[temp] - 1].y, i,br,num);
                rx = rx + route[probability[temp] - 1].x;
                ry = ry + route[probability[temp] - 1].y;
                rec[probability[temp]]++;
            }
            cntUp.Text = rec[3].ToString();
            cntDown.Text = rec[4].ToString();
            cntLeft.Text = rec[2].ToString();
            cntRight.Text = rec[1].ToString();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            double right = Math.Round(Convert.ToDouble(textRight.Text),1);
            double left = Math.Round(Convert.ToDouble(textLeft.Text),1);
            double up = Math.Round(Convert.ToDouble(textUp.Text),1);
            double down = Math.Round(Convert.ToDouble(textDown.Text),1);
            int num = Convert.ToInt32(textNum.Text);
            probabilitize(right, left, up, down);
            walk(num);
          }
        string textbox_base;
        object list;
        private void Text_MouseDown(object sender, RoutedEventArgs e)
        {
            if (list != null && ((TextBox)list).Text == "") ((TextBox)list).Text = textbox_base;
            textbox_base = ((TextBox)sender).Text;
            ((TextBox)sender).Text = "";
            list = sender;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            numflag = !numflag;
        }
    }
}
