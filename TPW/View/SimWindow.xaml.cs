using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using TPW.ViewModel;

namespace TPW.View
{
    /// <summary>
    /// Logika interakcji dla klasy SimWindow.xaml
    /// </summary>
    public partial class SimWindow : MetroWindow
    {
        private readonly SimWindow _self;

        public SimWindow(int n)
        {
            InitializeComponent();
            _self = this;
            DataContext = new SimViewModel(n, _self);
        }
    }

}
