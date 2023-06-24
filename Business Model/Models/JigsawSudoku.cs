using Business_Model.Abstractions;

namespace Business_Model.Models
{
    public class JigsawSudoku : Sudoku
    {
        public void AcceptLoad(ISudokuLoadVisitor visitor, string filePath)
        {
            visitor.VisitLoadJigsawSudoku(this, filePath);
        }

        public override void AcceptPrint(ISudokuPrintVisitor visitor)
        {
            visitor.VisitPrintSudoku(this);
        }
    }


}

