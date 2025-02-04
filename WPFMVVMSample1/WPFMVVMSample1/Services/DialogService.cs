using WPFMVVMSample1.Views;

namespace WPFMVVMSample1.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowDialog()
        {
            var popup = new PopupWindow();
            return popup.ShowDialog() ?? false;
        }
    }
}
