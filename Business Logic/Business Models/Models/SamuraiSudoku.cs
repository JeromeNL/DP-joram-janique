using Business_Logic.GameLogic;
using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Reader;
using Business_Model.Abstractions;
using Business_Model.Interfaces;

namespace Business_Model.Models
{
    public class SamuraiSudoku : Sudoku, ICloneableSudoku
    {

        public SamuraiSudoku()
        {

        }


        public SamuraiSudoku(SamuraiSudoku sudokuToCopy)
        {
            this.timer = sudokuToCopy.timer;
            this.CurrentState = sudokuToCopy.CurrentState;
            this.components = sudokuToCopy.components;
            this.sharedLeaves = sudokuToCopy.sharedLeaves;
        }


        public override ISudoku Clone()
        {
            return new SamuraiSudoku(this);
        }


        public void AcceptLoad(ISudokuLoadVisitor visitor, FileInfo fileInfo)
        {
            visitor.VisitLoadSamuraiSudoku(this, fileInfo);
        }


        public override void AcceptPrint(ISudokuPrintVisitor visitor)
        {
            visitor.VisitPrintSudoku(this);
        }
    }


}

