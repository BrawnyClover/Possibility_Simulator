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

        public MainWindow()
        {
            
            InitializeComponent();

            
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Owner = this;
            double right = Math.Round(Convert.ToDouble(textRight.Text),1);
            double left = Math.Round(Convert.ToDouble(textLeft.Text),1);
            double up = Math.Round(Convert.ToDouble(textUp.Text),1);
            double down = Math.Round(Convert.ToDouble(textDown.Text),1);
            int num = Convert.ToInt32(textNum.Text);
          }
    }
}
