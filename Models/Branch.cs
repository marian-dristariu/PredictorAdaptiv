using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorAdaptiv.Models
{
    public class Branch
    {
        public string BranchType { get; set; } // BT, BS, BM, NT, etc.
        public int CurrentAddress { get; set; } // AdrCrt
        public int DestinationAddress { get; set; } // AdrDest

        public Branch(string branchType, int currentAddress, int destinationAddress)
        {
            BranchType = branchType;
            CurrentAddress = currentAddress;
            DestinationAddress = destinationAddress;
        }

        public override string ToString()
        {
            return $"{BranchType} {CurrentAddress} {DestinationAddress}";
        }
    }
}
