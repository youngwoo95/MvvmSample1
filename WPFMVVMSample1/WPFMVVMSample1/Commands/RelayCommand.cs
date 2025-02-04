using System.Windows.Input;

namespace WPFMVVMSample1.Commands
{
    /// <summary>
    /// ICommand 인터페이스의 간단한 구현체이다. - 거의 이게 베이스라고 보면됨.
    /// 실행 로직과 실행 가능 여부를 결정하는 조건을 지정할 수 있다.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// RelayCommand 생성자이다.
        /// </summary>
        /// <param name="execute">명령이 실행될 때 호출되는 Action</param>
        /// <param name="canExecute">명령의 실행 가능 여부를 반환하는 조건 (옵션)</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// 명령이 실행 가능한지 여부를 결정한다.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// 명령 실행 시 호출된다.
        /// </summary>
        /// <param name="parameter">커맨드 매개변수</param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// CanExecute 상태가 변경되었음을 알리기 위한 이벤트이다.
        /// CommandManager가 이 이벤트를 이용해 커맨드의 활성 상태를 재평가한다.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
