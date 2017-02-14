using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Geone.Test.BLL;

namespace Geone.Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private PolygonInersection _intersection = new PolygonInersection();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btn.IsEnabled = false;
            DateTime startTime = DateTime.Now;
            _intersection.Start((progress, complete) =>
            {
                Dispatcher.Invoke((ThreadStart)(() =>
                {
                    pb.Value = progress;
                    if (complete)
                    {
                        pb.Value = 0;
                        btn.IsEnabled = true;
                    }

                    TimeSpan time = DateTime.Now - startTime; ;
                    txtTime.Text = "耗时：" + time.TotalSeconds + "s";
                }));

            });
        }
    }
}
