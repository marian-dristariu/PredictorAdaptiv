using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorAdaptiv
{
    public class PAgPredictor : IPredictor
    {
        private readonly Dictionary<int, int[]> predictionTables;
        private readonly Dictionary<int, int> localHistories;
        private readonly int historyBits;

        public PAgPredictor(int historyBits)
        {
            this.historyBits = historyBits;
            predictionTables = new Dictionary<int, int[]>();
            localHistories = new Dictionary<int, int>();
        }

        private int[] GetPredictionTable(int address)
        {
            if (!predictionTables.ContainsKey(address))
            {
                int tableSize = (int)Math.Pow(2, historyBits);
                predictionTables[address] = new int[tableSize];
                for (int i = 0; i < tableSize; i++)
                    predictionTables[address][i] = 1; // Weakly taken
            }
            return predictionTables[address];
        }

        private int GetLocalHistory(int address)
        {
            if (!localHistories.ContainsKey(address))
                localHistories[address] = 0; // Initialize with no history
            return localHistories[address];
        }

        public bool Predict(int address)
        {
            var table = GetPredictionTable(address);
            int history = GetLocalHistory(address);
            return table[history] >= 2; // Taken if counter >= 2
        }

        public void Update(int address, bool taken)
        {
            var table = GetPredictionTable(address);
            int history = GetLocalHistory(address);

            // Update saturating counter
            if (taken)
            {
                if (table[history] < 3) table[history]++;
            }
            else
            {
                if (table[history] > 0) table[history]--;
            }

            // Update local history
            localHistories[address] = ((history << 1) | (taken ? 1 : 0)) & ((1 << historyBits) - 1);
        }
    }
}
