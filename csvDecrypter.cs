using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace crypter
{
    internal class csvDecrypter
    {
        internal static DataTable dt;
        internal static async Task readCsv(string input)
        {
            string[] headers = { "P_UserID", "M_UserID", "UpdatedBalance", "TimeOfTransaction", "P_TransactionID" };

            List<string> valueSet1 = new List<string>();
            List<string> valueSet2 = new List<string>();
            List<string> valueSet3 = new List<string>();

            string[] lines = File.ReadAllLines(input);

            var values1 = lines.Select(l => new { FirstColumn = l.Split(',').First(), Values = l.Split(',').Skip(1).Select(v => int.Parse(v)) });
            var values2 = lines.Select(l => new { FirstColumn = l.Split(',').First(), Values = l.Split(',').Skip(2).Select(v => int.Parse(v)) });
            var values3 = lines.Select(l => new { FirstColumn = l.Split(',').First(), Values = l.Split(',').Skip(3).Select(v => int.Parse(v)) });
            var values4 = lines.Select(l => new { FirstColumn = l.Split(',').First(), Values = l.Split(',').Skip(3).Select(v => int.Parse(v)) });

            foreach (var item in values1)
            {
                valueSet1.Add(item.FirstColumn);
            }

            foreach (var item in values2)
            {
                valueSet2.Add(item.FirstColumn);
            }

            foreach (var item in values3)
            {
                valueSet3.Add(item.FirstColumn);
            }

            //TODO - Fix this (Cannot convert type string[] to type string)
            var c1_Index = valueSet1.FirstOrDefault(headers);
            var c2_Index = valueSet2.FirstOrDefault(headers);
            var c3_Index = valueSet3.FirstOrDefault(headers);

            //TODO - Write the resulting content back to the file.
        }
    }
}
