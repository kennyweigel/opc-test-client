using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//for SQL
using System.Data;
using System.Data.SqlClient;

namespace OpcTestClient
    {
    class SqlSimple
        {
        public SqlSimple()
            {
            }

        public SqlConnection Connection { get; private set; }
        
        //uses my default connection parameters
        public void ConnectToDb(
            string dataSource = "Data Source = DBASE_SERVER;",
            string userId = "user id=whosurdaddy;",
            string password = "password=whosurdaddy;", 
            string initialCatalog = "Initial Catalog = Kenneth.Weigel;")
            {
            Connection = new SqlConnection(dataSource + userId + password + initialCatalog);
            }

        public void Open()
            {
            Connection.Open();
            }

        public void Close()
            {
            Connection.Close();
            }

        public DataTable QueryDb(string query)
            {
            SqlCommand queryCommand = new SqlCommand(query, Connection);
            SqlDataReader queryCommandReader = queryCommand.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(queryCommandReader);

            //Print your  Column Headers
            String columns = string.Empty;
            foreach (DataColumn column in dataTable.Columns)
                {
                columns += column.ColumnName + " | ";
                }
            Console.WriteLine(columns);
            //Print the data
            int numRows = dataTable.Rows.Count;
            for (int i = 0; i < numRows; i++)
                {
                String rowText = string.Empty;
                foreach (DataColumn column in dataTable.Columns)
                    {
                    rowText += dataTable.Rows[i][column.ColumnName] + " | ";
                    }
                Console.WriteLine(rowText);
                }

            return dataTable;
            }
        }
    }
