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

        public bool RunTest<T>(string tagName, string tableName, T[] writeValues1, T[] writeValues2, int writeInterval)
            {
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] { tagName });

            SqlSimple Sql = new SqlSimple();
            Sql.ConnectToDb();
            Sql.Open();
            Sql.QueryDb("DELETE FROM [kenneth.weigel].dbo." + tableName);

            int writeValues1Length = writeValues1.Length;
            for (int i = 0; i < writeValues1Length; i++)
                {
                OpcObj.AddWriteValuesToGroup(new T[] { writeValues1[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //shows last item in writeValues array for verification
            OpcObj.SynchRead();

            //pauses so user can press enter to continue once DB connection is restored.
            Console.WriteLine("Click enter after server connection to DB is restored to begin interleaving");
            Console.ReadLine();

            //round 2
            int writeValues2Length = writeValues2.Length;
            for (int i = 0; i < writeValues2Length; i++)
                {
                OpcObj.AddWriteValuesToGroup(new T[] { writeValues2[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //shows last item in writeValues array for verification
            OpcObj.SynchRead();

            //pauses so user can press enter to continue once DB connection is restored.
            Console.WriteLine("Click enter after enough time has passed for all buffered data to be logged to DB");
            Console.ReadLine();

            DataTable dbResults = Sql.QueryDb("SELECT * FROM dbo." + tableName);

            //concatenates the write value arrays
            T[] writeValuesAll = new T[writeValues1Length + writeValues2Length];
            writeValues1.CopyTo(writeValuesAll, 0);
            writeValues2.CopyTo(writeValuesAll, writeValues1Length);

            bool results = CompareResults.VerifyDbData<T>(dbResults, writeValuesAll);

            Console.WriteLine("click enter to delete all rows from table");
            Console.ReadLine();

            Sql.QueryDb("DELETE FROM [kenneth.weigel].dbo." + tableName);

            Sql.Close();

            Console.WriteLine("test is complete, press enter to continue ");
            Console.ReadLine();

            return results;
            }
        }
    }
