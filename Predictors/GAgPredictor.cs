using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorAdaptiv.Predictors.Interfaces;

namespace PredictorAdaptiv.Predictors
{
    public class GAgPredictor : IPredictor
    {
        private readonly int[] predictionTable;
        private readonly int historyBits;
        private int gbhr; // Global Branch History Register

        public GAgPredictor(int historyBits)
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

        public bool Predict(int currentAddress)
        {
            int index = gbhr;
            return predictionTable[index] >= 2; // Taken if counter >= 2
        }

        public void Update(int currentAddress, bool taken)
        {
            int index = gbhr;

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
