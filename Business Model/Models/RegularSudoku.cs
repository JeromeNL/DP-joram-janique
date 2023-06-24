using Business_Model.Abstractions;

namespace Business_Model.Models
{
    public class RegularSudoku : Sudoku
    {
        public void AcceptLoad(ISudokuLoadVisitor visitor, string filePath)
        {
            visitor.VisitLoadRegularSudoku(this, filePath);
        }

        public override void AcceptPrint(ISudokuPrintVisitor visitor)
        {
            visitor.VisitPrintSudoku(this);
        }

    }


}

