using Business_Model.Models;

namespace Business_Logic.GameLogic.Printer
{
    public interface ISudokuPrintVisitor
    {
        void VisitPrintSudoku(RegularSudoku sudoku);
        void VisitPrintSudoku(SamuraiSudoku sudoku);
        void VisitPrintSudoku(JigsawSudoku sudoku);
    }


}

