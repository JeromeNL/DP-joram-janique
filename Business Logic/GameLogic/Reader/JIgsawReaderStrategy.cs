﻿using Business_Model.Interfaces;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Reader
{
    class JigsawReaderStrategy : IStrategy
    {
        public JigsawReaderStrategy()
        {
        }

        public ISudoku CreateBasedOnFile(FileInfo fileInfo)
        {
            SudokuBuilder builder = new SudokuBuilder();
            return builder.CreateJigsawSudokuFromFile(fileInfo);
        }
    }
}







