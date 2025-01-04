using PredictorAdaptiv.Predictors;

namespace PredictorAdaptiv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> filesPathsList = new List<string>()
            {
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FBUBBLE.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FPERM.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FPUZZLE.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FQUEENS.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FMATRIX.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FSORT.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FTOWER.tra",
                "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FTREE.tra",
            };

            foreach (string filePath in filesPathsList)
            {
                string fileName = Path.GetFileName(filePath);
                var branches = BranchFileReader.ReadTraFile(filePath);

                // Inițializare evaluare
                var evaluator = new BranchPredictorEvaluator(branches);

                // Configurare predictori
                int historyBits = 4;
                var gagPredictor = new GAgPredictor(historyBits);
                var pagPredictor = new PAgPredictor(historyBits);
                var gsharePredictor = new GSharePredictor(historyBits);

                // Evaluare
                Console.WriteLine($"Comparing Predictors for {fileName}:");

                (double gagAccuracy, int gagDifficult) = evaluator.EvaluatePredictor(gagPredictor);
                Console.WriteLine($"GAg - Accuracy: {gagAccuracy:P2}, Difficult Branches: {gagDifficult}");

                (double pagAccuracy, int pagDifficult) = evaluator.EvaluatePredictor(pagPredictor);
                Console.WriteLine($"PAg - Accuracy: {pagAccuracy:P2}, Difficult Branches: {pagDifficult}");

                (double gshareAccuracy, int gshareDifficult) = evaluator.EvaluatePredictor(gsharePredictor);
                Console.WriteLine($"GShare - Accuracy: {gshareAccuracy:P2}, Difficult Branches: {gshareDifficult}");
            }

        }
    }
}
