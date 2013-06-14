using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcTestClient
{
    //from MS
    class SimpleFileDelete
        {
        public static void Delete(string compFilePath)
            {
            if (System.IO.File.Exists(compFilePath))
                {
                // Use a try block to catch IOExceptions, to 
                // handle the case of the file already being 
                // opened by another process. 
                try
                    {
                    System.IO.File.Delete(compFilePath);
                    }
                catch (System.IO.IOException e)
                    {
                    Console.WriteLine(e.Message);
                    return;
                    }
                }
            }
        }
}
