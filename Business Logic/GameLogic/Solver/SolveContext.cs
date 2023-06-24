using Business_Model.Abstractions;

namespace Business_Logic.GameLogic.Solver
{
    class SolveContext
    {
        private ISolveStrategy _Strategy;

        public SolveContext()
        {
        }

        public SolveContext(ISolveStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public void SetSolveStrategy(ISolveStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public void GetSolver(Sudoku a)
        {
            _Strategy.SolveSudoku(a);
        }
    }
}
