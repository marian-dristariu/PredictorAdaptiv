using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictorAdaptiv.Predictors.Interfaces
{
    public interface IPredictor
    {
        bool Predict(int address);
        void Update(int address, bool taken);
    }

}
