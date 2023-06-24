using Business_Model.Abstractions;

namespace Business_Logic.GameLogic.Player
{
    public interface IPlayStrategy
    {
        void PlaySudoku(Sudoku sudoku, FileInfo fileInfo);
    }



}


