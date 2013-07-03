using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//DB
using System.Data;

namespace OpcTestClient
    {
    class TestForwardToSqlMultiDatatypes
        {
        public TestForwardToSqlMultiDatatypes()
            {
            }

        public bool RunTest(
            string tableName,
            int writeInterval,
            string dataSource = "DBASE_SERVER",
            string userId = "whosurdaddy",
            string password = "whosurdaddy", 
            string initialCatalog = "Kenneth.Weigel")
            
            {
            bool[] Bool = { true, false, true, false, false, true, true };
            byte[] Byte = { 0, 1, 2, 4, 7, 123, 127, 128, 250, 255 };
            sbyte[] Char = { -128, -127, -100, -1, 0, 1, 2, 100, 126, 127 };
            ushort[] Word = { 0, 1, 2, 3, 32766, 32767, 32768, 40000, 65534, 65535 };
            short[] Short = { -32768, -32767, -100, -1, 0, 1, 100, 5432, 32766, 32767 };
            uint[] DWord = { 0, 1, 12, 16, 13462346, 76857969, 4294967295 };
            //int[] Long = { -2147483648, -123124, 0, 1235146, 2147483647 };
            int[] Long = new int[20];
            for (int i = 0; i < 20; i++)
                {
                Long[i] = i;
                }
            float[] Float = { 47.5F, 24.3515F };
            double[] Double = { 1.23D, 1234.56D };
            string[] String = { "apple", "dog", "cat", "kenny" };
            
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            
            //bytes
            OpcObj.CreateSubscription("GroupByte");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Byte" });
            int byteLen = Byte.Length;
            for (int i = 0; i < byteLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Byte[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //chars
            OpcObj.CreateSubscription("GroupChar");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Char" });
            int charLen = Char.Length;
            for (int i = 0; i < charLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Char[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //words
            OpcObj.CreateSubscription("GroupWord");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Word" });
            int wordLen = Word.Length;
            for (int i = 0; i < wordLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Word[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //shorts
            OpcObj.CreateSubscription("GroupShort");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Short" });
            int shortLen = Short.Length;
            for (int i = 0; i < shortLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Short[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //dwords
            OpcObj.CreateSubscription("GroupDWord");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.DWord" });
            int dwordLen = DWord.Length;
            for (int i = 0; i < dwordLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { DWord[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //longs
            OpcObj.CreateSubscription("GroupLong");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Long" });
            int longLen = Long.Length;
            for (int i = 0; i < longLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Long[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //floats
            OpcObj.CreateSubscription("GroupFloat");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Float" });
            int floatLen = Float.Length;
            for (int i = 0; i < floatLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Float[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //doubles
            OpcObj.CreateSubscription("GroupDouble");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.Double" });
            int doubleLen = Double.Length;
            for (int i = 0; i < doubleLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { Double[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }
            //strings
            OpcObj.CreateSubscription("GroupString");
            OpcObj.AddItemsToGroup(new string[] { "C1.D1.String" });
            int stringLen = String.Length;
            for (int i = 0; i < stringLen; i++)
                {
                OpcObj.AddWriteValuesToGroup(new[] { String[i] });
                OpcObj.SynchWrite();
                System.Threading.Thread.Sleep(writeInterval);
                }

            //pauses so user can press enter to continue once DB connection is restored.
            Console.WriteLine("Click enter after server connection to DB is restored and enough time has passed for all buffered data to be logged to DB");
            Console.ReadLine();

            SqlSimple Sql = new SqlSimple();
            Sql.ConnectToDb(dataSource, userId, password, initialCatalog);
            Sql.Open();

            DataTable dbResults = Sql.QueryDb("SELECT * FROM dbo." + tableName);

            string[] byteVals = Byte.Select(x => x.ToString()).ToArray();
            string[] charVals = Char.Select(x => x.ToString()).ToArray();
            string[] wordVals = Word.Select(x => x.ToString()).ToArray();
            string[] shortVals = Short.Select(x => x.ToString()).ToArray();
            string[] dwordVals = DWord.Select(x => x.ToString()).ToArray();
            string[] longVals = Long.Select(x => x.ToString()).ToArray();
            string[] floatVals = Float.Select(x => x.ToString()).ToArray();
            string[] doubleVals = Double.Select(x => x.ToString()).ToArray();
            string[] stringVals = String;
            string[] all = byteVals.Concat(charVals).Concat(wordVals).Concat(shortVals).Concat(dwordVals).Concat(longVals).Concat(floatVals).Concat(doubleVals).Concat(stringVals).ToArray();

            bool results = CompareResults.VerifyDbData<string>(dbResults, all);

            Console.WriteLine("click enter to delete all rows from table");
            Console.ReadLine();

            Sql.QueryDb("DELETE FROM [" + initialCatalog + "].dbo." + tableName);
            Sql.Close();

            return results;
            }
        }
    }
