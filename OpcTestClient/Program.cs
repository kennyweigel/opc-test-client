using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcTestClient
    {
    class Program
        {
        static void Main(string[] args)
            {
            //int[] writeVals1 = new int[100];
            //for (int i = 0; i < 100; i++)
            //    {
            //    writeVals1[i] = i + 1;
            //    }
            //int[] writeVals2 = new int[100];
            //for (int i = 0; i < 100; i++)
            //    {
            //    writeVals2[i] = i + 56;
            //    }
            //TestInterleaving test1 = new TestInterleaving();
            //test1.RunTest("C1.D1.K1", "testIntNarrowValues", writeVals1, writeVals2, 200);


            //Data Type testing individual DBs
            //bool[] Bool = { true, false, true, false, false, true, true };
            //byte[] Byte = { 0, 1, 2, 4, 7, 123, 127, 128, 250, 255 };
            //sbyte[] Char = { -128, -127, -100, -1, 0, 1, 2, 100, 126, 127 };
            //ushort[] Word = { 0, 1, 2, 3, 32766, 32767, 32768, 40000, 65534, 65535 };
            //short[] Short = { -32768, -32767, -100, -1, 0, 1, 100, 5432, 32766, 32767 };
            //uint[] DWord = { 0, 1, 12, 16, 13462346, 76857969, 4294967295 };
            //int[] Long = { -2147483648, -123124, 0, 1235146, 2147483647 };
            //float[] Float = { 47.5F, 24.3515F };
            //double[] Double = { 1.23D, 1234.56D };
            //string[] String = { "apple", "dog", "cat", "kenny" };
            int[] testVals = new int[200];
            for (int i = 0; i < 200; i++)
                {
                testVals[i] = i + 1;
                }
            TestForwardToSql testWord = new TestForwardToSql();
            testWord.RunTest<int>("table1", 200, "C1.D1.K1", testVals, "KEN-SERVER08R2\\SQLEXPRESS", "test", "test", "test");


            //start multiple tag data types forwarding
            //TestForwardToSqlMultiDatatypes testasdf = new TestForwardToSqlMultiDatatypes();
            //testasdf.RunTest("allDataTypesNarrow", 200,"KEN-SERVER08R2\\SQLEXPRESS", "test", "test", "test");
            //end


            // this makes sure the console doesnt close immediatly
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            }
        }
    }