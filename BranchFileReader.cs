using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorAdaptiv.Models;

namespace PredictorAdaptiv
{
    public class BranchFileReader
    {
        public static List<Branch> ReadTraFile(string filePath)
        {
            var branches = new List<Branch>();

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(' ');
                    if (parts.Length == 3)
                    {
                        string branchType = parts[0];
                        int currentAddress = int.Parse(parts[1]);
                        int destinationAddress = int.Parse(parts[2]);

                        branches.Add(new Branch(branchType, currentAddress, destinationAddress));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return branches;
        }
    }
}
