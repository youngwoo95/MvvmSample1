using System.Windows;
using WPFMVVMSample1.Services;
using WPFMVVMSample1.ViewModels;

namespace WPFMVVMSample1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // MainViewModel에 DialogService 인스턴스를 주입한다.
            DataContext = new MainViewMode(new DialogService());
        }
    }
}