using System.Collections.ObjectModel;
using System.Windows.Input;
using WPFMVVMSample1.Commands;
using WPFMVVMSample1.Models;

namespace WPFMVVMSample1.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        // 모델 클래스
        private TodoItem selecteditem;
        private TodoItem editingitem;

        public ObservableCollection<TodoItem> TodoItems { get; set; }

        public TodoItem SelectedItem
        {
            get
            {
                return selecteditem;
            }
            set
            {
                selecteditem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public TodoItem EditingItem
        {
            get
            {
                return editingitem;
            }
            set
            {
                editingitem = value;
                OnPropertyChanged(nameof(editingitem));
            }
        }

        // 커맨드
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand MarkCompleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public HomeViewModel()
        {
            // 초기 데이터 - 실제 환경에서는 뎅터베이스나 API를 통해 로드
            TodoItems = new ObservableCollection<TodoItem>
            {
                new TodoItem{ Id = 1, Title = "Buy groceries", Description = "Mile, Bread, Cheese", DueDate = DateTime.Today.AddDays(1), IsCompleted = false},
                new TodoItem{ Id = 2, Title = "Call John", Description = "Discuss project details", DueDate = DateTime.Today.AddDays(2), IsCompleted = false},
            };
            
            // 커맨드 초기화
            AddCommand = new RelayCommand(param => AddTodo(), param => true);
            DeleteCommand = new RelayCommand(param => DeleteTodo(), param => SelectedItem != null);
            MarkCompleteCommand = new RelayCommand(param => MarkComplete(), param => SelectedItem != null && !SelectedItem.IsCompleted);
            EditCommand = new RelayCommand(param => EditTodo(), param => SelectedItem != null);

            //  SaveCommand는 EditingItem이 존재하며, Title 유효성 검사(Title이 비어있으면 안됨)가 통과되어야 활성화된다.
            SaveCommand = new RelayCommand(param => SaveTodo(), 
                param => EditingItem != null && 
                String.IsNullOrWhiteSpace(EditingItem[nameof(EditingItem.Title)]));
                
            



            CancelCommand = new RelayCommand(param => CancelEdit(), param => EditingItem != null);
        }

        private void AddTodo()
        {
            // 새 항목 생성 - ID는 현재 개수 + 1 (실제 환경에서는 고유 키를 별도로 관리)
            var newTodo = new TodoItem { Id = TodoItems.Count + 1, DueDate = DateTime.Today, Title = "New Task" };
            TodoItems.Add(newTodo);
            selecteditem = newTodo;
        }

        private void DeleteTodo()
        {
            if(SelectedItem != null)
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
            if(SelectedItem != null)
            {
                EditingItem = new TodoItem
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
            if(EditingItem != null)
            {
                // 컬렉션에서 해당 항목 찾아 업데이트
                var item = TodoItems.FirstOrDefault(x => x.Id == EditingItem.Id);
                if(item != null)
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
