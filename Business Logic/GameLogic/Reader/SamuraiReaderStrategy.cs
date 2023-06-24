using Business_Model.Interfaces;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Reader
{
    class SamuraiReaderStrategy : IStrategy
    {
        public SamuraiReaderStrategy()
        {

        }


        public ISudoku CreateBasedOnFile(FileInfo fileInfo)
        {
            SudokuBuilder builder = new SudokuBuilder();
            return builder.CreateSamuraiSudokuFromFile(fileInfo);
        }

    }
}







