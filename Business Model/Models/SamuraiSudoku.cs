using Business_Model.Abstractions;

namespace Business_Model.Models
{
    public class SamuraiSudoku : Sudoku
    {
        public void AcceptLoad(ISudokuLoadVisitor visitor, string filePath)
        {
            visitor.VisitLoadSamuraiSudoku(this, filePath);
        }

        public override void AcceptPrint(ISudokuPrintVisitor visitor)
        {
            visitor.VisitPrintSudoku(this);
        }
    }


}

