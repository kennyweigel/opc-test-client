using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//DB
using System.Data;

namespace OpcTestClient
    {
    static class CompareResults
        {
        public static bool VerifyDbData<T>(DataTable dataTable, T[] CorrectValues)
            {
            int indexLoggedValues = 0;
            int numRows = dataTable.Rows.Count;
            T[] LoggedValues = new T [numRows];
            
            for (int i = 0; i < numRows; i++)
                {
                Console.WriteLine(dataTable.Rows[i][3]);
                LoggedValues[indexLoggedValues] = Parse<T>(dataTable.Rows[i][3].ToString());
                indexLoggedValues++;
                }

            if (Enumerable.SequenceEqual(CorrectValues, LoggedValues))
                {
                Console.WriteLine("match");
                return true;
                }
            else
                {
                Console.WriteLine("no match");
                return false;
                }
            }

        private static T Parse<T>(string sourceValue) //where T : IConvertible
            {
            return (T)Convert.ChangeType(sourceValue, typeof(T));
            }
        }
    }