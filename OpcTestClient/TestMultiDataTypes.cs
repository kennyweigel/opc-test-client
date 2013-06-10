using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcTestClient
    {
    class TestMultiDataTypes
        {
        public TestMultiDataTypes()
            {
            }

        public void RunTest<T>(T[] writeValues, int writeInterval)
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
            OpcObj.SynchRead();
            
            //pauses so user can press enter to continue once DB connection is restored.
            Console.ReadLine();

            //DbDataTable = new DataTable();
            //DbDataTable = QueryDb();

            //bool result = VerifyDbData(DbDataTable);
            
            //return result;
            
            }
        }
    }
