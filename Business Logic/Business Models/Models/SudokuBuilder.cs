using Business_Logic.GameLogic.Reader;
using Business_Model.Interfaces;
using System.IO;

namespace Business_Model.Models
{
    public class SudokuBuilder
    {
        private ISudoku sudoku;

        public RegularSudoku CreateRegularSudokuFromFile(FileInfo fileInfo)
        {
            RegularSudoku sudoku = new RegularSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, fileInfo);
            return sudoku;
        }

        public JigsawSudoku CreateJigsawSudokuFromFile(FileInfo fileInfo)
        {
            JigsawSudoku sudoku = new JigsawSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, fileInfo);
            return sudoku;
        }
        public SamuraiSudoku CreateSamuraiSudokuFromFile(FileInfo fileInfo)
        {
            SamuraiSudoku sudoku = new SamuraiSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, fileInfo);
            return sudoku;
        }


        public SudokuBuilder CreateRegularSudoku()
        {
            sudoku = new RegularSudoku();
            return this;
        }

        public SudokuBuilder CreateSamuraiSudoku()
        {
            sudoku = new SamuraiSudoku();
            return this;
        }

        public SudokuBuilder CreateJigsawSudoku()
        {
            sudoku = new JigsawSudoku();
            return this;
        }

        public ISudoku Build()
        {
            return sudoku;
        }
    }


}

