using System.Configuration;
using System.Data;
using System.Windows;
using TPW.Model;

namespace TPW
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Mathematics math1 = new Mathematics();

            math1.add(3.90, 0.11);

            math1.subtract(3.25, 0.35);

            math1.multiply(2.14, 3.15);

            math1.divide(10.50, 2.80);

            base.OnStartup(e);
        }
      
    }

}
