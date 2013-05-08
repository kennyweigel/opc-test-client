﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcTestClient
    {
    class OpcSimple
        {
        //constructor
        public OpcSimple()
            {
            }

        public Opc.Da.Server OpcServer { get; private set; }
        public Opc.Da.Subscription OpcSubscription { get; private set; }
        public Opc.Da.Item[] OpcItems { get; private set; }
        public Opc.Da.ItemValue[] OpcItemValues { get; private set; }

        //connects to OPC server
        //input: string of server url
        public void ConnectToServer(string serverUrl)
            {
            Opc.URL url = new Opc.URL(serverUrl);
            OpcCom.Factory fact = new OpcCom.Factory();
            OpcServer = new Opc.Da.Server(fact, null);
            OpcServer.Connect(url, new Opc.ConnectData(new System.Net.NetworkCredential()));
            }

        //create OPC subscription (Group)
        //input: group name string
        public void CreateSubscription(string groupName)
            {
            Opc.Da.SubscriptionState groupState = new Opc.Da.SubscriptionState();
            groupState.Name = groupName;
            groupState.Active = false;
            OpcSubscription = (Opc.Da.Subscription)OpcServer.CreateSubscription(groupState);
            }

        //add items to the group
        //input: string array of server item names
        public void AddItemsToGroup(string[] itemNames)
            {
            int len = itemNames.Length;
            OpcItems = new Opc.Da.Item[len];
            for (int i = 0; i < len; i++)
                {
                OpcItems[i] = new Opc.Da.Item();
                OpcItems[i].ItemName = itemNames[i];
                }
            OpcSubscription.AddItems(OpcItems);
            }
        
        //adds write values to a group
        //input: array int write values
        //will need to figure out how to override method to handle different data types
        public void AddWriteValuesToGroup(int[] writeVals)
            {
            int len = writeVals.Length;
            OpcItemValues = new Opc.Da.ItemValue[len];
            for (int i = 0; i < len; i++)
                {
                OpcItemValues[i] = new Opc.Da.ItemValue();
                OpcItemValues[i].ServerHandle = OpcSubscription.Items[i].ServerHandle;
                OpcItemValues[i].Value = writeVals[i];
                }
            }

        //exectutes a synch write to server with contents of group writevalues on group items
        public void SynchWrite()
            {
            Opc.IdentifiedResult[] writeResult = OpcSubscription.Write(OpcItemValues);
            int len = writeResult.Length;
            for (int i = 0; i < len; i++)
                {
                Console.WriteLine("write:\t{0}\twrite result: {1}", writeResult[0].ItemName, writeResult[0].ResultID);
                }
            }
        
        //executes a synch read to server to read items in group OpcSubscription
        public void SynchRead()
            {
            Opc.Da.ItemValueResult[] readResult = OpcSubscription.Read(OpcSubscription.Items);
            Console.WriteLine("Read:\t{0}\tvalue:{1}", readResult[0].ItemName, readResult[0].Value);
            }
        }
    }
