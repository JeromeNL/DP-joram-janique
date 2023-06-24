
using Business_Logic.GameLogic.Reader;
using Business_Model.Abstractions;

namespace Business_Logic.GameLogic.Solver
{
    public static class MainSolver
    {
        public static void SolveSudoku(Sudoku a, FileInfo fileInfo)
        {
            SolveContext context = new SolveContext();

            List<string> regularSudokuExtensions = new List<string>
            {
                ".4x4",
                ".6x6",
                ".9x9"
            };
            Console.Clear();

            if (regularSudokuExtensions.Contains(fileInfo.Extension))
            {
                
                context.SetSolveStrategy(new JigsawSolverStrategy());

            }
            else if (fileInfo.Extension.Equals(".samurai"))
            {
                context.SetSolveStrategy(new SamuraiSolverStrategy());
            }
            else if (fileInfo.Extension.Equals(".jigsaw"))
            {
                context.SetSolveStrategy(new JigsawSolverStrategy());
            }
            else
            {
                throw new DirectoryNotFoundException();
            }
        context.GetSolver(a);
        }
    }
}