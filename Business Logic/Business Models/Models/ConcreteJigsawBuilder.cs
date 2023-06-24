using Business_Logic.GameLogic.Reader;
using Business_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Business_Models.Models
{
    public static class ConcreteJigsawBuilder
    {
        public static JigsawSudoku CreateJigsawSudokuFromFile(FileInfo fileInfo)
        {
            JigsawSudoku sudoku = new JigsawSudoku();
            SudokuLoadVisitor visitor = new SudokuLoadVisitor();
            sudoku.AcceptLoad(visitor, fileInfo);
            return sudoku;
        }
    }
}
