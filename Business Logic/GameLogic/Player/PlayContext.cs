using Business_Model.Abstractions;

namespace Business_Logic.GameLogic.Player
{
    class PlayContext
    {
        private IPlayStrategy _Strategy;

        public PlayContext()
        {
        }

        public PlayContext(IPlayStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public void SetPlayStrategy(IPlayStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public void GetPlay(Sudoku a, FileInfo fileInfo)
        {
            _Strategy.PlaySudoku(a, fileInfo);
        }
    }
}
