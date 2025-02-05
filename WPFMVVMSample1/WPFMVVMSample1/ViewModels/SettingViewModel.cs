using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFMVVMSample1.Commands;
using WPFMVVMSample1.Models;

namespace WPFMVVMSample1.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private TodoItem2 selectedItem;
        private TodoItem2 editingItem;

        public ObservableCollection<TodoItem2> TodoItems { get; set; }

        public TodoItem2 SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if(selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public TodoItem2 EditingItem
        {
            get
            {
                return editingItem;
            }
            set
            {
                if(editingItem != value)
                {
                    editingItem = value;
                    OnPropertyChanged(nameof(EditingItem));
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand MarkCompleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public SettingViewModel()
        {
            // 초기 데이터
            TodoItems = new ObservableCollection<TodoItem2>
            {
                new TodoItem2 { Id = 1, Title = "테스트 내용1", Description = "Milk, Bread, Cheese", DueDate = DateTime.Today.AddDays(1), IsCompleted = false },
                new TodoItem2 { Id = 2, Title = "테스트 내용2", Description = "Discuss project details", DueDate = DateTime.Today.AddDays(2), IsCompleted = false }
            };

            //SelectedItem = TodoItems.FirstOrDefault();

            // 커맨드 초기화
            AddCommand = new RelayCommand(param => AddTodo());
            DeleteCommand = new RelayCommand(param => DeleteTodo(), param => SelectedItem != null);
            MarkCompleteCommand = new RelayCommand(param => MarkComplete(), param => SelectedItem != null && !SelectedItem.IsCompleted);
            EditCommand = new RelayCommand(param => EditTodo(), param => SelectedItem != null);

            // SaveCommand: EditingItem이 존재하고, 유효성 검증에서 Title에 오류가 없을 경우 활성화
            SaveCommand = new RelayCommand(param => SaveTodo(),
                param => EditingItem != null &&
                         // INotifyDataErrorInfo를 구현했으므로 Valid errors for Title should be empty.
                         !((EditingItem as INotifyDataErrorInfo)?.HasErrors ?? true));

            CancelCommand = new RelayCommand(param => CancelEdit(), param => EditingItem != null);
        }

        private void AddTodo()
        {
            var newTodo = new TodoItem2 { Id = TodoItems.Count + 1, DueDate = DateTime.Today, Title = "New Task" };
            TodoItems.Add(newTodo);
            SelectedItem = newTodo;
        }

        private void DeleteTodo()
        {
            if (SelectedItem != null)
            {
                TodoItems.Remove(SelectedItem);
                SelectedItem = null;
            }
        }

        private void MarkComplete()
        {
            if (SelectedItem != null)
                SelectedItem.IsCompleted = true;
        }

        private void EditTodo()
        {
            if (SelectedItem != null)
            {
                // 복사본 생성: 실제 편집은 복사본에서 진행하고, Save시 원본을 업데이트
                EditingItem = new TodoItem2
                {
                    Id = SelectedItem.Id,
                    Title = SelectedItem.Title,
                    Description = SelectedItem.Description,
                    DueDate = SelectedItem.DueDate,
                    IsCompleted = SelectedItem.IsCompleted
                };
            }
        }

        private void SaveTodo()
        {
            if (EditingItem != null)
            {
                var item = TodoItems.FirstOrDefault(x => x.Id == EditingItem.Id);
                if (item != null)
                {
                    item.Title = EditingItem.Title;
                    item.Description = EditingItem.Description;
                    item.DueDate = EditingItem.DueDate;
                    item.IsCompleted = EditingItem.IsCompleted;
                }
                EditingItem = null;
            }
        }

        private void CancelEdit()
        {
            EditingItem = null;
        }

    }
}
