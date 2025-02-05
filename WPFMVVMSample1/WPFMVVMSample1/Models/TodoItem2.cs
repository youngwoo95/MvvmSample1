using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFMVVMSample1.Models
{
    /// <summary>
    /// TodoItem2 모델
    /// - 속성 변경 통지와 유효성 검증을 INotifyDataErrorInfo를 통해 수정
    /// </summary>
    public class TodoItem2 : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private int id;
        private string? title;
        private string? description;
        private DateTime dueDate;
        private bool isCompleted;
        
        // 속성별 오류를 관리하는 Dictionary
        private readonly Dictionary<string, List<string>> _errors = new();

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }

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
                if(title != value)
                {
                    title = value;
                    OnPropertyChanged();
                    ValidateTitle();
                }
            }
        }

        public string? Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    ValidateDescription();
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                if (dueDate != value)
                {
                    dueDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyDataErrorInfo Implementaion
        
        // 전체 오류가 있으면 true
        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        protected void OnErrorChanged([CallerMemberName]string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // 특정 속성의 오류 목록 반환 (오류가 없으면 빈 컬렉션 반환)
        public IEnumerable GetErrors(string? propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                return null!;
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : new List<string>();
        }

        // 오류 추가
        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Add(error);
                OnErrorChanged(propertyName);
            }
        }

        // 오류 제거
        private void ClearErrors(string propertName)
        {
            if(_errors.ContainsKey(propertName))
            {
                _errors.Remove(propertName);
                OnErrorChanged(propertName);
            }
        }
        #endregion

        #region Validation Methods
        private void ValidateTitle()
        {
            const string propertyName = nameof(Title);
            ClearErrors(propertyName);

            if (string.IsNullOrWhiteSpace(Title))
                AddError(propertyName, "Title is required.");
            else if (Title.Length < 3)
                AddError(propertyName, "Title must be at least 3 characters long.");
        }

        private void ValidateDescription()
        {
            const string propertyName = nameof(Description);
            ClearErrors(propertyName);

            if (string.IsNullOrWhiteSpace(Description))
                AddError(propertyName, "Description is required.");
            // 추가적인 검증 로직 가능
        }

        #endregion

    }
}
