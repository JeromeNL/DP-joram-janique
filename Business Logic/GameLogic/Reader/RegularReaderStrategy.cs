using Business_Model.Interfaces;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Reader
{
    class RegularReaderStrategy : IStrategy
    {
        public RegularReaderStrategy()
        {


        }

        public ISudoku CreateBasedOnFile(FileInfo fileInfo)
        {
            SudokuBuilder builder = new SudokuBuilder();
            return builder.CreateRegularSudokuFromFile(fileInfo);
        }

    }
}







