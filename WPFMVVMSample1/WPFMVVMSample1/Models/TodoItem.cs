using System.ComponentModel;

namespace WPFMVVMSample1.Models
{
    /// <summary>
    /// 모델 클래스
    ///     -  속성 변경 통지와 간단한 데이터 유효성 검사를 구현
    /// </summary>
    
    public class TodoItem : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        private string? title;
        private string? description;
        private DateTime dueDate;
        private bool isCompleted;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string? Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string? Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;
                OnPropertyChanged(nameof(dueDate));
            }
        }

        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        // IDataErrorInfo 구현 - 예: Title은 필수 항목입니다.
        public string? this[string columnName]
        {
            get
            {
                if(columnName == nameof(Title))
                {
                    if (String.IsNullOrWhiteSpace(Title))
                        return "Title is required";
                }
                
                if (columnName == nameof(Description))
                {
                    if (String.IsNullOrWhiteSpace(Description))
                        return "Title is required";
                }

                return null;
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
