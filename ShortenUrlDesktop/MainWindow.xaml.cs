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

namespace ShortenUrlDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DependencyProperty ViewModelProperty;

        internal ShortenUrlViewModel ViewModel
        {
            get => (ShortenUrlViewModel)GetValue(ViewModelProperty);
            private set => SetValue(ViewModelProperty, value);
        }

        static MainWindow()
        {
            ViewModelProperty = DependencyProperty.Register(nameof(ViewModel), typeof(ShortenUrlViewModel), typeof(MainWindow));
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
