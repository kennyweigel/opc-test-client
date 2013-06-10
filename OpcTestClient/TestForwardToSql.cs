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
        public bool RunTest<T>(T[] writeValues, int writeInterval)
            {
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.K1" });

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
            Console.WriteLine("Click enter after server connection to DB is restored " +
                "and enough time has passed for all buffered data to be logged to DB");
            Console.ReadLine();

            SqlSimple Sql = new SqlSimple();
            Sql.ConnectToDb();
            Sql.Open();

            DataTable dbResults = Sql.QueryDb("SELECT * FROM dbo.testMissingValues");

            bool results = CompareResults.VerifyDbData<T>(dbResults, writeValues);

            Sql.QueryDb("DELETE FROM [kenneth.weigel].dbo.testMissingValues");

            Sql.Close();

            return results;
            }
        }
    }
