using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcTestClient
    {
    //from MS
    class SimpleFileCopy
        {
        public static void Copy(string fileName, string sourcePath, string destPath)
            {
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(destPath, fileName);
            
            // To copy a folder's contents to a new location: 
            // Create a new target folder, if necessary. 
            if (!System.IO.Directory.Exists(destPath))
            {
                System.IO.Directory.CreateDirectory(destPath);
            }

            // To copy a file to another location and  
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);
            }
        }
    }
