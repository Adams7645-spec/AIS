using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AIS.Core;
using AIS.Core.DatabaseHandling;
using Microsoft.Win32;

namespace AIS
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PickFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            var result = dialog.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                DatabaseHandler.DBPath = dialog.FolderName;
                DatabaseHandler.LoadDBFilesInDataGrid(DatabaseHandler.CurrentDataGrid);
            }
        }
    }
}