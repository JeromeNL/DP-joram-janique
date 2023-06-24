using Business_Logic.GameLogic;
using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Reader;
using Business_Model.Abstractions;
using Business_Model.Interfaces;
using System.IO;

namespace Business_Model.Models
{
    public class JigsawSudoku : Sudoku, ICloneableSudoku
    {
        public JigsawSudoku()
        {

        }
        
        public JigsawSudoku(JigsawSudoku sudokuToCopy)
        {
            this.timer = sudokuToCopy.timer;
            this.CurrentState = sudokuToCopy.CurrentState;
            this.components = sudokuToCopy.components;
            this.sharedLeaves = sudokuToCopy.sharedLeaves;
        }

        public override ISudoku Clone()
        {
            return new JigsawSudoku(this);
        }

        public void AcceptLoad(ISudokuLoadVisitor visitor, FileInfo fileInfo)
        {
            visitor.VisitLoadJigsawSudoku(this, fileInfo);
        }

        public override void AcceptPrint(ISudokuPrintVisitor visitor)
        {
            visitor.VisitPrintSudoku(this);
        }
    }
}

