using Business_Logic.GameLogic;
using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Reader;
using Business_Model.Abstractions;
using Business_Model.Interfaces;

namespace Business_Model.Models
{
    public class RegularSudoku : Sudoku, ICloneableSudoku
    {
        public void AcceptLoad(ISudokuLoadVisitor visitor, FileInfo filePath)
        {
            visitor.VisitLoadRegularSudoku(this, filePath);
        }

        public RegularSudoku() { 
        
        }


        public RegularSudoku(RegularSudoku sudokuToCopy)
        {
            this.timer = sudokuToCopy.timer;
            this.CurrentState = sudokuToCopy.CurrentState;
            this.components = sudokuToCopy.components;
            this.sharedLeaves = sudokuToCopy.sharedLeaves;
        }


        public override ISudoku Clone()
        {
            return new RegularSudoku(this);
        }


        public override void AcceptPrint(ISudokuPrintVisitor visitor)
        {
            visitor.VisitPrintSudoku(this);
        }
    }


}

