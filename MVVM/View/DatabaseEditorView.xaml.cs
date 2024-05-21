using AIS.Core.DatabaseHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIS.MVVM.View
{
    /// <summary>
    /// Interaction logic for DatabaseEditorView.xaml
    /// </summary>
    public partial class DatabaseEditorView : UserControl
    {
        public DatabaseEditorView()
        {
            InitializeComponent();
            TablesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Tables name", Binding = new Binding("Name") });
        }

        private void TablesDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (DatabaseHandler.CurrentDatabaseModel.ConnectionString == string.Empty)
            {
                MessageBox.Show("Select database first.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TablesDataGrid.ItemsSource = null;
            TablesDataGrid.ItemsSource = DatabaseHandler.GetDBTablesData(DatabaseHandler.GetConnection(DatabaseHandler.CurrentDatabaseModel.ConnectionString)).ItemsSource;
        }

        private void CreateTableButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHandler.CreateTableOnDB(DatabaseHandler.GetConnection(DatabaseHandler.CurrentDatabaseModel.ConnectionString));
        }
    }
}
