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
            OpcSimple OpcObj = new OpcSimple();
            OpcObj.ConnectToServer("opcda://localhost/Kepware.KEPServerEX.V5");
            OpcObj.CreateSubscription("Group");
            OpcObj.AddItemsToGroup(new string[] {"C1.D1.K1"});

            for (int i = 0; i < 50; i++)
                {
                OpcObj.AddWriteValuesToGroup(new int[] { i });
                OpcObj.SynchWrite();
                //adds a delay between writes
                System.Threading.Thread.Sleep(100);
                }
            OpcObj.SynchRead();
            Console.WriteLine("ASYNCH");
            OpcObj.AsynchRead(1);
            OpcObj.AsynchWrite(2);
            // this makes sure the console doesnt close immediatly
            Console.ReadLine();
            }
        }
    }