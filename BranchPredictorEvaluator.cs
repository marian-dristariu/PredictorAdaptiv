using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictorAdaptiv.Models;
using PredictorAdaptiv.Predictors.Interfaces;

namespace PredictorAdaptiv
{
    public class BranchPredictorEvaluator
    {
        private readonly List<Branch> branches;

        public BranchPredictorEvaluator(List<Branch> branches)
        {
            this.branches = branches;
        }

        public (double accuracy, int difficultBranches) EvaluatePredictor(IPredictor predictor)
        {
            int correctPredictions = 0;
            int difficultBranches = 0;
            bool? previousOutcome = null;

            foreach (var branch in branches)
            {
                bool isTaken = branch.BranchType.StartsWith("B");
                bool prediction = predictor.Predict(branch.CurrentAddress);

                // Count correct predictions
                if (prediction == isTaken)
                    correctPredictions++;

                // Detect difficult branches
                if (previousOutcome.HasValue && previousOutcome != isTaken)
                    difficultBranches++;

                previousOutcome = isTaken;

                // Update predictor
                predictor.Update(branch.CurrentAddress, isTaken);
            }

            double accuracy = (double)correctPredictions / branches.Count;
            return (accuracy, difficultBranches);
        }
    }

}
