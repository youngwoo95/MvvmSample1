using System.Windows;

namespace WPFMVVMSample1.Views
{
    /// <summary>
    /// PopupWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopupWindow : Window
    {
        public PopupWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
