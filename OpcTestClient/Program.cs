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

            //TestInterleaving test1 = new TestInterleaving();
            //test1.RunTest("C1.D1.K1", "testIntNarrowValues", writeVals1, writeVals2, 200);

            //creates an array with values 1-100
            //don't start int array with 0, that is the default value for Simulator devices
            int[] writeVals = new int[100];
            for (int i = 0; i < 100; i++)
                {
                writeVals[i] = i + 1;
                }

            //TestForwardToSql test2 = new TestForwardToSql();
            //test2.RunTest<int>("C1.D1.K1", "testIntNarrowValues", writeVals, 200);             

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

            //TestForwardToSql test3 = new TestForwardToSql();
            //test3.RunTest<string>("C1.D1.S1", "testStringNarrowVals", stringVals, 200);

            //Data Type testing
            bool[] Bool = { true, false, true, false, false, true, true };
            byte[] Byte = { 0, 1, 2, 4, 7, 123, 127, 128, 250, 255 };
            sbyte[] Char = { -128, -127, -100, -1, 0, 1, 2, 100, 126, 127 };
            ushort[] Word = { 0, 1, 2, 3, 32766, 32767, 32768, 40000, 65534, 65535 };
            short[] Short = { -32768, -32767, -100, -1, 0, 1, 100, 5432, 32766, 32767 };
            uint[] DWord = { 0, 1, 12, 16, 13462346, 76857969, 4294967295 };
            int[] Long = { -2147483648, -123124, 0, 1235146, 2147483647 };
            float[] Float = { 47.5F, 24.3515F };
            double[] Double = { 1.23D, 1234.56D };
            string[] String = { "apple", "dog", "cat", "kenny" };

            //TestForwardToSql testBool = new TestForwardToSql();
            //testBool.RunTest<bool>("C1.D1.Bool", "allDataTypeNarrow", Bool, 200);

            TestForwardToSql testByte = new TestForwardToSql();
            testByte.RunTest<byte>("C1.D1.Byte", "allDataTypeNarrow", Byte, 200);

            TestForwardToSql testChar = new TestForwardToSql();
            testChar.RunTest<sbyte>("C1.D1.Char", "allDataTypeNarrow", Char, 200);

            TestForwardToSql testWord = new TestForwardToSql();
            testWord.RunTest<ushort>("C1.D1.Word", "allDataTypeNarrow", Word, 200);

            TestForwardToSql testShort = new TestForwardToSql();
            testShort.RunTest<short>("C1.D1.Short", "allDataTypeNarrow", Short, 200);

            TestForwardToSql testDWord = new TestForwardToSql();
            testDWord.RunTest<uint>("C1.D1.DWord", "allDataTypeNarrow", DWord, 200);

            TestForwardToSql testLong = new TestForwardToSql();
            testLong.RunTest<int>("C1.D1.Long", "allDataTypeNarrow", Long, 200);

            TestForwardToSql testFloat = new TestForwardToSql();
            testFloat.RunTest<float>("C1.D1.Float", "allDataTypeNarrow", Float, 200);

            TestForwardToSql testDouble = new TestForwardToSql();
            testDouble.RunTest<double>("C1.D1.Double", "allDataTypeNarrow", Double, 200);

            TestForwardToSql testString = new TestForwardToSql();
            testString.RunTest<string>("C1.D1.String", "allDataTypeNarrow", String, 200);


            //SimpleFileCopy.Copy("test1.txt", @"C:\Users\Admin\Desktop", @"C:\Users\Admin\Desktop\test");
            //SimpleFileDelete.Delete(@"C:\Users\Admin\Desktop\test\test1.txt");

            // this makes sure the console doesnt close immediatly
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            }
        }
    }