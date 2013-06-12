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
            int[] writeVals1 = new int[100];
            for (int i = 0; i < 100; i++)
            {
                writeVals1[i] = i + 1;
            }

            int[] writeVals2 = new int[100];
            for (int i = 0; i < 100; i++)
            {
                writeVals2[i] = i + 56;
            }
            
            TestInterleaving test1 = new TestInterleaving();
            test1.RunTest("C1.D1.K1", "testIntNarrowValues", writeVals1, writeVals2, 200);

            //creates an array with values 1-100
            //don't start int array with 0, that is the default value for Simulator devices
            int[] writeVals = new int[100];
            for (int i = 0; i < 100; i++)
                {
                writeVals[i] = i+1;
                }

            TestForwardToSql test2 = new TestForwardToSql();
            test2.RunTest<int>("C1.D1.K1", "testIntNarrowValues", writeVals, 200);

            //creates an array of string values
            //dont start string array with "String 1", that is the default value fro Simulator devices
            string[] stringVals = {"Store","and","Forward","As","a","Controls","Engineer","I","want","the","DataLogger","to","buffer","collected","data","So","I","don't","lose","data",
                                      "if","the","SQL","server","goes","down","10","EPIC","Notes","Buffer","To","File","As","a","Controls","Engineer","I","want","data","buffered","to",
                                      "a","file","when","I","lose","connectivity","to","the","SQL","server","So","I","don't","lose","the","data","when","the","SQL","server","gets",
                                      "busy","or","goes","down","10","On","a","lost","connection,","ALL","data","that","would","go","to","the","database","is","buffered","to","a","file",
                                      "When","the","connection","is","OK,","no","data","is","in","the","buffered","file","File","is","on","the","local","file","system","Storage","format",
                                      "doesn't","need","to","be","consumable","by","3rd","parties","GSK","Memory","not","necessary","do","need","file","persisted","between","runs",
                                      "Forward","to","SQL","As","a","Controls","Engineer","I","want","buffered","data","to","be","sent","to","the","SQL","server","when","connectivity",
                                      "returns","Buffered","data","ends","up","in","SQL","after","a","reconnect","Data","order","does","not","change","from","existing",
                                      "implementationtBuffered","File","Location","As","a","Controls","Engineer","I","want","to","be","able","to","configure","where","to","save","the",
                                      "buffered","file","So","I","can","use","the","appropriate","disk","and","directory","Location","only","needs","to","be","on","same","PC","as","the",
                                      "KEPServerEX","Don't","need","to","copy","existing","buffered","data","to","new","location","if","location","is","changed","Once","changed,","if",
                                      "there","is","existing","buffered","data","in","the","new","location,","this","should","be","forwarded","Optional","Store","and","Forward","As","a",
                                      "Controls","Engineer","I","want","to","disable","store","and","forward","Because","I","have","an","existing","installation","that","doesn't","need",
                                      "it","Old","projects","have","the","option","disabled","Enabled","by","default","for","new","log","grouptFile","Size","Limits","As","a","Controls",
                                      "Engineer","I","want","to","limit","the","data","buffered","So","I","don't","run","out","of","disk","space","User","can","limit","the","buffer",
                                      "by","size","in","MB","User","is","shown","a","rough","estimate","of","the","maximum","storage","time,","given","the","size","User","is","warned",
                                      "via","the","event","log","when","data","is","lost","If","the","user","reduces","the","size","and","the","existing","buffer","exceeds","the",
                                      "size,","it's","OK","to","lose","data","Log","group","drops","new","data","when","the","limit","is","reachedt","GSK:","Stop","logging","data",
                                      "when","limit","is","reached","(new","data","is","lost)","Buffering","System","Tag","As","a","Controls","Engineer","I","want","to","use","a",
                                      "provided","System","Tag","to","indicate","when","buffering","is","happening","So","I","can","use","in","applications","to","send","an","alert",
                                      "that","the","database","connection","has","been","lost","Per","log","group","Tag","is","true","when","there","is","buffered","data,","false",
                                      "otherwise","Secure","Buffered","Files","As","a","Controls","Engineer","I","want","files","saved","with","Administrative","persmission","only",
                                      "To","limit","who","can","edit","and","delete","the","files","Saved","with","Admin","rights","Clearing","Buffered","Data","As","a","Controls",
                                      "Engineer","I","want","to","clear","buffered","data","So","I","can","reset","the","buffering","when","testing","3","Can","clear","all","buffered",
                                      "data","Can","clear","data","for","one","log","group","GSK","Okay","with","manually","deleteing","buffer","files","to","achieve","this"};

            TestForwardToSql test3 = new TestForwardToSql();
            test3.RunTest<string>("C1.D1.S1", "testStringNarrowVals", stringVals, 200);

            // this makes sure the console doesnt close immediatly
            Console.ReadLine();
            }
        }
    }