using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DAL.Interfaces
{
    public interface RepoInterface<T>
    {
        static string _dir = Directory.GetCurrentDirectory();
        static string _dbStr = _dir.Substring(0, _dir.LastIndexOf("\\")) + "\\AlbumsSongs.db";
        static string connString = $"Data Source={_dbStr};Version=3;";

        
        public T GetItemById(int id);
        public T SaveItem(T item);
        public int DeleteItem(int id);
        public T UpdateItem(T item);
        public IEnumerable<T> GetAllItems();

        public DataTable QueryDB(string sqlString)
        {
            var resTable = new DataTable();
            using (var connection = new SQLiteConnection(connString))
            {
                using (var cmd = new SQLiteCommand(sqlString, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(resTable);
                    }
                }
            }
            return resTable;
        }
      
        public int InsertIntoDB(string sqlString)
        {
            int id;
            using (var connection = new SQLiteConnection(connString))
            {
                using (var command = new SQLiteCommand(sqlString, connection))
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }

                sqlString = "SELECT last_insert_rowid()";

                using (var command = new SQLiteCommand(sqlString, connection))
                {
                    id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return id;
        }
    }
}