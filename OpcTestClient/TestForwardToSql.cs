using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//DB Stuff
using System.Data;

namespace OpcTestClient
    {
    class TestForwardToSql
        {
        public TestForwardToSql()
            {
            }

        //disconnect server from DB then run test
        public bool RunTest<T>(
            string tableName,
            int writeInterval,
            string tagName,
            T[] writeValues,
            string dataSource = "DBASE_SERVER",
            string userId = "whosurdaddy",
            string password = "whosurdaddy",
            string initialCatalog = "Kenneth.Weigel")
            {
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] { tagName });

            int writeValuesLength = writeValues.Length;
            for (int i = 0; i < writeValuesLength; i++)
                {
                OpcObj.AddWriteValuesToGroup(new T[] { writeValues[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //shows last item in writeValues array for verification
            OpcObj.SynchRead();

            //pauses so user can press enter to continue once DB connection is restored.
            Console.WriteLine("Click enter after server connection to DB is restored and enough time has passed for all buffered data to be logged to DB");
            Console.ReadLine();

            SqlSimple Sql = new SqlSimple();
            Sql.ConnectToDb(dataSource, userId, password, initialCatalog);
            Sql.Open();
            DataTable dbResults = Sql.QueryDb("SELECT * FROM [" + initialCatalog + "].dbo." + tableName);

            Console.WriteLine("test is complete, press enter to continue ");
            Console.ReadLine();
            Sql.QueryDb("DELETE FROM [" + initialCatalog + "].dbo." + tableName);
            Sql.Close();

            bool results = CompareResults.VerifyDbData<T>(dbResults, writeValues);
            return results;
            }
        }
    }
