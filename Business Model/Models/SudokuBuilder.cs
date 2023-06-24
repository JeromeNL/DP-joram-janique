using Business_Model.Interfaces;

namespace Business_Model.Models
{
    public class SudokuBuilder
    {
        private ISudoku sudoku;

        public RegularSudoku CreateRegularSudokuFromFile(string filePath)
        {
            RegularSudoku sudoku = new RegularSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, filePath);
            return sudoku;
        }

        public JigsawSudoku CreateJigsawSudokuFromFile(string filePath)
        {
            JigsawSudoku sudoku = new JigsawSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, filePath);
            return sudoku;
        }
        public SamuraiSudoku CreateSamuraiSudokuFromFile(string filePath)
        {
            SamuraiSudoku sudoku = new SamuraiSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, filePath);
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

