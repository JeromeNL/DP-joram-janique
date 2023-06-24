using Business_Logic.Business_Models.Models;
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
            return ConcreteRegularBuilder.CreateRegularSudokuFromFile(fileInfo);
        }

        public JigsawSudoku CreateJigsawSudokuFromFile(FileInfo fileInfo)
        {
          return ConcreteJigsawBuilder.CreateJigsawSudokuFromFile(fileInfo);
        }

        public SamuraiSudoku CreateSamuraiSudokuFromFile(FileInfo fileInfo)
        {
           return ConcreteSamuraiBuilder.CreateSamuraiSudokuFromFile(fileInfo);
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

