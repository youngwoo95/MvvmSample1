using System.ComponentModel;

namespace WPFMVVMSample1.ViewModels
{
    /// <summary>
    /// 팝업을 위해 ViewModel의 Base가 되는 클래스를 만들었음.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
