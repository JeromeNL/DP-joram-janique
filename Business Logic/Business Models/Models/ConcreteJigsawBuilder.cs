using Business_Logic.GameLogic.Reader;
using Business_Model.Models;

namespace Business_Logic.Business_Models.Models
{
    public static class ConcreteJigsawBuilder
    {
        public static JigsawSudoku CreateJigsawSudokuFromFile(FileInfo fileInfo)
        {
            JigsawSudoku sudoku = new JigsawSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, fileInfo);
            return sudoku;
        }
    }
}
