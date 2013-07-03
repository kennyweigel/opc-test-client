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
            string dataSource = "DBASE_SERVER",
            string userId = "whosurdaddy",
            string password = "whosurdaddy", 
            string initialCatalog = "Kenneth.Weigel")
            {
            Connection = new SqlConnection("Data Source="+dataSource+";user id="+userId+";password="+password+";Initial Catalog="+initialCatalog+";");
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
