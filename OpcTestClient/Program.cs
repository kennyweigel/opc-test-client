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
            //TestInterleaving test1 = new TestInterleaving();
            //test1.RunTest(0, 10, 12, 22, 100);

            //TestStoreMaxSize test2 = new TestStoreMaxSize();
            //test2.RunTest(0, 1000, 100);

            //TestMultiDataTypes test3 = new TestMultiDataTypes();
            //string[] arr = { "hello", "Kenny" };
            //test3.RunTest<string>(arr, 200);

            int[] writeVals = new int[100];
            for (int i = 0; i < 100; i++)
                {
                writeVals[i] = i;
                }



            TestForwardToSql test4 = new TestForwardToSql();
            test4.RunTest<int>(writeVals, 100);

            // this makes sure the console doesnt close immediatly
            Console.ReadLine();
            }
        }
    }