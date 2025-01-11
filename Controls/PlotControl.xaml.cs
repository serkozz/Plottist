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

namespace Plottist.Controls
{
    /// <summary>
    /// Логика взаимодействия для PlotControl.xaml
    /// </summary>
    public partial class PlotControl : UserControl
    {
        public PlotControl()
        {
            InitializeComponent();
        }

        public void SetData(double[] xs, double[] ys)
        {
            plot.Plot.Clear();
            plot.Plot.Add.Scatter(xs, ys);
            plot.Refresh();
        }
    }
}
