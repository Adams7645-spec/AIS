using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace AIS.Core.DatabaseHandling
{
    public record DataBaseModel
    {
        private string _name;
        private string _path;
        private string _connectionString;
        public string Name { get => _name; }
        public string Path { get => _path; }
        public string ?ConnectionString { get => _connectionString; }

        public DataBaseModel(string dbName, string ?extension) 
        {
            _name = dbName;
            _path = string.Concat(DatabaseHandler.DBPath, "\\", dbName, extension);
            _connectionString = string.Concat($"Data Source={_path};Version=3;New=True;Compress=True;");
        }
    }
}