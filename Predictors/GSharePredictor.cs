using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorAdaptiv.Predictors.Interfaces;

namespace PredictorAdaptiv.Predictors
{
    public class GSharePredictor : IPredictor
    {
        private readonly int[] predictionTable;
        private readonly int historyBits;
        private int gbhr; // Global Branch History Register

        public GSharePredictor(int historyBits)
        {
            this.historyBits = historyBits;
            int tableSize = (int)Math.Pow(2, historyBits);
            predictionTable = new int[tableSize];
            gbhr = 0;

            // Initialize prediction table with weakly taken (1)
            for (int i = 0; i < tableSize; i++)
            {
                predictionTable[i] = 1; // Weakly taken
            }
        }

        private int GetIndex(int address)
        {
            int maskedAddress = address & (1 << historyBits) - 1; // Mask to relevant bits
            return maskedAddress ^ gbhr; // XOR with GBHR
        }

        public bool Predict(int address)
        {
            int index = GetIndex(address);
            return predictionTable[index] >= 2; // Taken if counter >= 2
        }

        public void Update(int address, bool taken)
        {
            int index = GetIndex(address);

            // Update saturating counter
            if (taken)
            {
                if (predictionTable[index] < 3) predictionTable[index]++;
            }
            else
            {
                if (predictionTable[index] > 0) predictionTable[index]--;
            }

            // Update global branch history register
            gbhr = (gbhr << 1 | (taken ? 1 : 0)) & (1 << historyBits) - 1;
        }
    }

}
