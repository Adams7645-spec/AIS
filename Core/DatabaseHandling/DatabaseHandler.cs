using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Xml.Linq;

namespace AIS.Core.DatabaseHandling
{
    public static class DatabaseHandler
    {
        public static ObservableCollection<DataBaseModel> DataBases = new ObservableCollection<DataBaseModel>();
        public static string ?DBPath { get; set; }
        public static DataGrid ?CurrentDataGrid { get; set; }
        public static DataBaseModel ?CurrentDatabaseModel { get; set; }

        public static void CreateTableOnDB(SQLiteConnection connection)
        {
            SQLiteCommand liteCommand = connection.CreateCommand();
            string commandPrompt = "CREATE TABLE TestTable (FirstColumn VARCHAR(30), SecondColumn INT);";
            liteCommand.CommandText = commandPrompt;
            liteCommand.ExecuteNonQuery();
            connection.Close();
        }

        public static void InsertDataInTable(SQLiteConnection connection)
        {
            SQLiteCommand liteCommand = connection.CreateCommand();
            string commandPrompt = "INSERT INTO TestTable (FirstColumn, SecondColumn) VALUES ('Test Text', 65);";
            liteCommand.CommandText = commandPrompt;
            liteCommand.ExecuteNonQuery();
            connection.Close();
        }

        public static DataGrid GetDBTablesData(SQLiteConnection connection)
        {
            SQLiteDataReader dataReader;
            SQLiteCommand command;
            command = connection.CreateCommand();
            command.CommandText = @"SELECT name FROM sqlite_master WHERE type = 'table'";

            DataGrid grid = new DataGrid();
            grid.Columns.Add(new DataGridTextColumn { Header = "Table name", Binding = new Binding("Name") });

            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string readedData = dataReader.GetString(0);
                grid.Items.Add(new {Name = $"{readedData}"});
            }
            connection.Close();
            return grid;
        }

        public static bool CreateDatabaseFileStatus(string dbName)
        {
            if (isAbleToCreate(dbName))
            {
                var temp = new DataBaseModel(dbName, ".db");
                DataBases.Add(temp);
                File.Create(temp.Path);
                return true;
            } 
            return false;
        }

        public static bool DeleteDatabaseFileStatus(string dbName) 
        { 
            if(isAbleToDelete(dbName))
            {
                var item = DataBases.FirstOrDefault(item => item.Name.Equals(dbName));
                File.Delete(item.Path);
                DataBases.Remove(item);
                return true;
            }
            return false;
        }

        public static void LoadDBFilesInDataGrid(DataGrid dataGrid)
        {
            dataGrid.Items.Clear();
            var dbDirList = Directory.GetFiles(DBPath, "*.db");
            if (dbDirList.Length == 0)
                return;

            var dbNameList = new List<string>();

            foreach (var dbFile in dbDirList)
            {
                var name = System.IO.Path.GetFileName(dbFile);
                var db = new DataBaseModel(name, null);
                DataBases.Add(db);
                dbNameList.Add(name);
            }

            foreach (var item in dbNameList)
            {
                dataGrid.Items.Add(new { Name = $"{item}" });
            }
        }

        public static ObservableCollection<DataBaseModel> GetDBData()
        {
            return DataBases;
        }

        public static SQLiteConnection GetConnection(string connectionString)
        {
            SQLiteConnection liteConnection = new SQLiteConnection(connectionString);

            try
            {
                liteConnection.Open();
            }
            catch (Exception)
            {
                throw;
            }

            return liteConnection;
        }

        private static bool isAbleToCreate(string dbName)
        {
            return !DataBases.Contains(new DataBaseModel(dbName, null)) && !string.IsNullOrEmpty(dbName);
        }

        private static bool isAbleToDelete(string dbName)
        {
            return DataBases.FirstOrDefault(item => item.Name.Contains(dbName)) != null;
        }
    }
}
