using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//For SQL queries
using System.Data;
using System.Data.SqlClient;

namespace OpcTestClient
    {
    class TestInterleaving
        {
        public TestInterleaving()
            {
            }

        public int[] CorrectValues { get; private set; }
        public int[] LoggedValues { get; private set; }
        public DataTable DbDataTable { get; private set; }

        //writes values 0->maxValue to OPC Server, then pauses until user presses enter which
        //will allow them to reconnect to the DB, then writes values maxValue->0 to OPC Server
        //finally the DB will be queried to make sure that entries 0->maxValue,maxValue->0 are
        //correct and in order
        // input: maxValue is max value chosen by the user, writeInterval: time between writes to
        // prevent DataLogger missing data
        // output: boolean value indicating test pass/failure
        public bool RunTest(int minValue1, int maxValue1, int minValue2, int maxValue2, int writeInterval)
            {            
            //CorrectValues = new List<int>();
            int indexCorrectValues = 0;
            int lengthCorrectValues = (maxValue1 - minValue1) + (maxValue2 - minValue2);
            CorrectValues = new int[lengthCorrectValues];

            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.K1" });

            for (int i = minValue1; i < maxValue1; i++)
                {
                //CorrectValues.Add(i);
                CorrectValues[indexCorrectValues] = i;
                indexCorrectValues++;
                OpcObj.AddWriteValuesToGroup(new int[] { i });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            OpcObj.SynchRead();
            //pauses so user can press enter to continue once DB connection is restored.
            Console.WriteLine("press enter after reconnecting log group to DB");
            Console.ReadLine();

            for (int i = minValue2; i < maxValue2; i++)
                {
                //CorrectValues.Add(i);
                CorrectValues[indexCorrectValues] = i;
                indexCorrectValues++;
                OpcObj.AddWriteValuesToGroup(new int[] { i });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            OpcObj.SynchRead();

            Console.WriteLine("press enter to query DB");
            Console.ReadLine();

            DbDataTable = new DataTable();
            DbDataTable = QueryDb();

            bool result = VerifyDbData(DbDataTable);
            
            return result;
            }


//Everything below here will be implemented by SqlSimple class
        private DataTable QueryDb()
            {
            // Create a connection
            string dataSource = "Data Source = DBASE_SERVER;";
            string userId = "user id=whosurdaddy;";
            string password = "password=whosurdaddy;";
            string initialCatalog = "Initial Catalog = Kenneth.Weigel;";
            SqlConnection dbConnection = new SqlConnection(dataSource + userId + password + initialCatalog);
            dbConnection.Open();
            
            string query = "SELECT * FROM dbo.testMissingValues";
            SqlCommand queryCommand = new SqlCommand(query, dbConnection);
            // Use the above SqlCommand object to create a SqlDataReader object.
            SqlDataReader queryCommandReader = queryCommand.ExecuteReader();
            // Create a DataTable object to hold all the data returned by the query.
            DataTable dataTable = new DataTable();
            // Use the DataTable.Load(SqlDataReader) function to put the results of the query into a DataTable.
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

            //deletes all data from DB
            string delete = "DELETE FROM [Kenneth.Weigel].[dbo].[testMissingValues]";
            SqlCommand deleteCommand = new SqlCommand(delete, dbConnection);
            deleteCommand.ExecuteReader();
            // Close the connection
            dbConnection.Close();
            return dataTable;
            }
        
        private bool VerifyDbData(DataTable dataTable)
            {
            int indexLoggedValues = 0;
            int numRows = dataTable.Rows.Count;
            LoggedValues = new int[numRows];

            for (int i = 0; i < numRows; i++)
                {
                Console.WriteLine(dataTable.Rows[i][3]);
                //int tre = dataTable.Rows[i][3].ToString();
                //Console.WriteLine(tre);

                LoggedValues[indexLoggedValues] = Convert.ToInt32(dataTable.Rows[i][3].ToString());
                indexLoggedValues++;
                }

            if (Enumerable.SequenceEqual(CorrectValues, LoggedValues))
                {
                Console.WriteLine("match");
                return true;
                }
            else
                {
                Console.WriteLine("no match");
                return false;
                }
            }
        }
    }
