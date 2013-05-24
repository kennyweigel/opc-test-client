using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//DEBUG
using System.Data;
using System.Data.SqlClient;

namespace OpcTestClient
    {
    class Program
        {
        static void Main(string[] args)
            {
            //// Create a connection
            //string dataSource = "Data Source = DBASE_SERVER;";
            //string userId = "user id=whosurdaddy;";
            //string password = "password=whosurdaddy;";
            //string initialCatalog = "Initial Catalog = Kenneth.Weigel;";
            //SqlConnection dbConnection = new SqlConnection(dataSource+userId+password+initialCatalog);
            //dbConnection.Open();
            //string query = "SELECT * FROM dbo.testMissingValues";
            //SqlCommand queryCommand = new SqlCommand(query, dbConnection);
            //// Use the above SqlCommand object to create a SqlDataReader object.
            //SqlDataReader queryCommandReader = queryCommand.ExecuteReader();
            //// Create a DataTable object to hold all the data returned by the query.
            //DataTable dataTable = new DataTable();
            //// Use the DataTable.Load(SqlDataReader) function to put the results of the query into a DataTable.
            //dataTable.Load(queryCommandReader);

            
            ////Print your  Column Headers
            //String columns = string.Empty;
            //foreach (DataColumn column in dataTable.Columns)
            //    {
            //    columns += column.ColumnName + " | ";
            //    }
            //Console.WriteLine(columns);

            ////Print the data
            //int numRows = dataTable.Rows.Count;
            //for (int i = 0; i < numRows; i++)
            //    {
            //    String rowText = string.Empty;
            //    foreach (DataColumn column in dataTable.Columns)
            //        {
            //        rowText += dataTable.Rows[i][column.ColumnName] + " | ";
            //        }
            //    Console.WriteLine(rowText);
            //    }

            //// Close the connection
            //dbConnection.Close();

            
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] {"C1.D1.K1"});

            for (int i = 0; i < 50; i++)
                {
                OpcObj.AddWriteValuesToGroup(new int[] { i });
                OpcObj.SynchWrite();
                //adds a delay between writes
                System.Threading.Thread.Sleep(50);
                }
            OpcObj.SynchRead();
            //Console.WriteLine("ASYNCH");
            //OpcObj.AsynchRead(1);
            //OpcObj.AsynchWrite(2);
            
            // this makes sure the console doesnt close immediatly
            Console.ReadLine();
            }
        }
    }