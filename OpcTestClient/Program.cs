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
            TestInterleaving test1 = new TestInterleaving();
            
            test1.RunTest(0, 10, 12, 22, 100);

            // this makes sure the console doesnt close immediatly
            Console.ReadLine();
            }
        }
    }