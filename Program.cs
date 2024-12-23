namespace PredictorAdaptiv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Marian Dristariu\\Downloads\\Branch_G-20241105T175402Z-001\\Branch_G\\SimMark\\tra\\FBUBBLE.tra"; // Specifică calea către fișier
            var branches = BranchFileReader.ReadTraFile(filePath);

            // Inițializare evaluare
            var evaluator = new BranchPredictorEvaluator(branches);

            // Configurare predictori
            int historyBits = 4;
            var gagPredictor = new GAgPredictor(historyBits);
            var pagPredictor = new PAgPredictor(historyBits);
            var gsharePredictor = new GSharePredictor(historyBits);

            // Evaluare
            Console.WriteLine("Comparing Predictors:");

            (double gagAccuracy, int gagDifficult) = evaluator.EvaluatePredictor(gagPredictor);
            Console.WriteLine($"GAg - Accuracy: {gagAccuracy:P2}, Difficult Branches: {gagDifficult}");

            (double pagAccuracy, int pagDifficult) = evaluator.EvaluatePredictor(pagPredictor);
            Console.WriteLine($"PAg - Accuracy: {pagAccuracy:P2}, Difficult Branches: {pagDifficult}");

            (double gshareAccuracy, int gshareDifficult) = evaluator.EvaluatePredictor(gsharePredictor);
            Console.WriteLine($"GShare - Accuracy: {gshareAccuracy:P2}, Difficult Branches: {gshareDifficult}");
        }
    }
}
