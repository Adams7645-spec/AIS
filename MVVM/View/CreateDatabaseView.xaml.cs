using AIS.Core.DatabaseHandling;
using AIS.MVVM.Model;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIS.MVVM.View
{
    public partial class CreateDatabaseView : UserControl
    {
        public ObservableCollection<DataBaseModel> _databases = new ObservableCollection<DataBaseModel>();
        private int ?_currentSelectedDBIndex = null;
        public CreateDatabaseView()
        {
            InitializeComponent();
            SetLocalDBData();
            BuildDataGridView();
        }
        private void SetLocalDBData()
        {
            _databases = DatabaseHandler.GetDBData();
        }

        private void AddDBButton_Click(object sender, RoutedEventArgs e)
        {
            CreateDBContainer.Visibility = Visibility.Visible;
        }

        private void BuildDataGridView()
        {
            DBDataGrid.Columns.Add(new DataGridTextColumn { Header = "Database name", Binding = new Binding("Name") });
            DatabaseHandler.CurrentDataGrid = DBDataGrid;
        }

        private void UpdateDataGrid()
        {
            DBDataGrid.Items.Clear();
            SetLocalDBData();

            foreach (var item in _databases)
            {
                DBDataGrid.Items.Add(new { Name = $"{item.Name}.db" });
            }
        }

        private void CreateDB_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseHandler.DBPath == null)
            {
                MessageBox.Show("Fick a folder first.", "Warning", MessageBoxButton.OK);
                return;
            }

            if (DatabaseHandler.CreateDatabaseFileStatus(CreateDBTextBox.Text))
            {
                CreateDBContainer.Visibility = Visibility.Hidden;
                UpdateDataGrid();
                return;
            }
            CreateDBTextBox.Clear();
        }

        private void DeleteDBButton_Click(object sender, RoutedEventArgs e)
        {
            if (DatabaseHandler.DBPath == null)
            {
                MessageBox.Show("Fick a folder first.", "Warning", MessageBoxButton.OK);
                return;
            }

            if (_currentSelectedDBIndex == null)
                return;

            if (DatabaseHandler.DeleteDatabaseFileStatus(_databases[(int)_currentSelectedDBIndex].Name))
            {
                UpdateDataGrid();
                return;
            }
        }

        private void DBDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            _currentSelectedDBIndex = DBDataGrid.SelectedIndex;
            DatabaseHandler.CurrentDatabaseModel = _databases[(int)_currentSelectedDBIndex];
        }
    }
}
