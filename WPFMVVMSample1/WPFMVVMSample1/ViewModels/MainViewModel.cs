using System.Windows.Data;
using System.Windows.Input;
using WPFMVVMSample1.Commands;
using WPFMVVMSample1.Services;

namespace WPFMVVMSample1.ViewModels
{
    /// <summary>
    /// 메인화면에 PopupWindow 버튼을 생성할거기 때문에 MainViewModel에 상속받는다.
    /// </summary>
    public class MainViewMode : ViewModelBase 
    {
        // Window Popup
        private readonly IDialogService DialogService;

        /// <summary>
        /// Home 버튼 이벤트
        /// </summary>
        public ICommand ShowHomeCommand { get; }
        /// <summary>
        /// Setting 버튼 이벤트
        /// </summary>
        public ICommand ShowSettingCommand { get; }

        /// <summary>
        /// Popup 버튼 이벤트
        /// </summary>
        public ICommand ShowPopupCommand { get; }


        private ViewModelBase currentviewmodel;

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentviewmodel;
            }
            set
            {
                currentviewmodel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        // 기본 생성자 (매개변수 없음)
        public MainViewMode()
        {
            // DI 없이 기본값으로 설정하거나
            // 필요시 내부에서 DialogService의 기본 구현을 생성할 수도 있다.
            // ex. DialogService = new DialogService();

            // 예제에서는 임시로 null 체크 후 기본 화면 설정
            CurrentViewModel = new HomeViewModel();
            ShowHomeCommand = new RelayCommand(param => CurrentViewModel = new HomeViewModel());
            ShowSettingCommand = new RelayCommand(param => CurrentViewModel = new SettingViewModel());
            ShowPopupCommand = new RelayCommand(param => ShowPopup());
        }

        // 생성자에서 IDialogService를 주입받는다.
        public MainViewMode(IDialogService dialogService)
        {
            DialogService = dialogService;

            // 초기 화면은 HomeViewModel로 설정
            CurrentViewModel = new HomeViewModel();

            ShowHomeCommand = new RelayCommand(param => CurrentViewModel = new HomeViewModel());
            ShowSettingCommand = new RelayCommand(param => CurrentViewModel = new SettingViewModel());
            ShowPopupCommand = new RelayCommand(param => ShowPopup());
        }

        // 팝업을 띄우기 위한 메서드이다.
        private void ShowPopup()
        {
            // 다이얼로그 서비스를 통해 팝업 창을 띄우고 결과를 받는다.
            bool result = DialogService.ShowDialog();

            // 필요에 따라 result(팝업의 DialogResult)를 처리할 수 있다.
            // ex. if(result) { ... }
        }
    }
}

/*
 MVVM 패턴에서는 비즈니스 로직, 데이터 처리, 상태 관리, 커맨드 실행 등과 같은 기능 로직은 ViewModel에서 구현한다.

ViewModel
-> 애플리케이션의 핵심 로직과 데이터를 관리한다.
-> 사용자 입력에 대한 반응(ex. ICommand를 통한 커맨드 실행), 데이터 바인딩, 상태, 업데이트 등의 기능 로직을 포함한다.
-> 테스트 가능하도록 순수한 로직으로 구성하는 것이 좋다.

View (XAML 및 코드비하인드) 
-> 사용자 인터페이스(UI) 표시와 간단한 UI 관련 작업만 처리한다.
-> 버튼 클릭 같은 이벤트는 커맨드를 통해 ViewModel로 전달하고, UI와 직접적으로 연결된 간단한 로직(ex. 팝업창 닫기 등)은 코드비하인드에서 처리할 수 있다.
 */