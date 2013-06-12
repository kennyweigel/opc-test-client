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
        public bool RunTest<T>(string tagName, string tableName, T[] writeValues, int writeInterval)
            {
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] { tagName });

            SqlSimple Sql = new SqlSimple();
            Sql.ConnectToDb();
            Sql.Open();
            Sql.QueryDb("DELETE FROM [kenneth.weigel].dbo." + tableName);

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

            DataTable dbResults = Sql.QueryDb("SELECT * FROM dbo." + tableName);

            bool results = CompareResults.VerifyDbData<T>(dbResults, writeValues);

            Console.WriteLine("click enter to delete all rows from table");
            Console.ReadLine();
            
            Sql.QueryDb("DELETE FROM [kenneth.weigel].dbo."+tableName);

            Sql.Close();

            Console.WriteLine("test is complete, press enter to continue ");
            Console.ReadLine();

            return results;
            }
        }
    }
