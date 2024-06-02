using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TPW.Logika;
using TPW.View;

namespace TPW.Prezentacja.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            ScreenVal = "Wprowadź liczbę";
            SimulationStartCommand = new RelayCommand(SimulationStart);
        }

        private void SimulationStart(object obj)
        {
            SimWindow simWindow = new SimWindow(int.Parse(ScreenVal));
            //simWindow.Show();

            Application.Current.MainWindow.Close();
        }


        private string _screenVal;

        public string ScreenVal
        {
            get
            {
                return _screenVal;
            }
            set
            {
                _screenVal = value;
                OnPropertyChanged();
            }
        }

        public ICommand SimulationStartCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
