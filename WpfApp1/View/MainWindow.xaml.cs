using System.Windows;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WpfApp1.App app = WpfApp1.App.Current as App;
            app.InitializeComponent();
        }
    }
}
