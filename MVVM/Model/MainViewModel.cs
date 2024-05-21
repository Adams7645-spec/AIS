using AIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS.MVVM.Model
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand CreateDatabaseViewCommand { get; set; }
        public RelayCommand DatabaseEditorViewCommand { get; set; }
        public CreateDatabaseModel CreateDatabaseVM { get; set; }
        public DatabaseEditorViewModel DatabaseEditorVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CreateDatabaseVM = new CreateDatabaseModel();
            DatabaseEditorVM = new DatabaseEditorViewModel();
            CurrentView = CreateDatabaseVM;

            CreateDatabaseViewCommand = new RelayCommand(o =>
            {
                CurrentView = CreateDatabaseVM;
            });

            DatabaseEditorViewCommand = new RelayCommand(o =>
            {
                CurrentView = DatabaseEditorVM;
            });
        }
    }
}
