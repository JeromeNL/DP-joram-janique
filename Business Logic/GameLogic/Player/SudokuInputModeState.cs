using Business_Model.Abstractions;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Player
{
    public abstract class SudokuInputModeState
    {
        protected Sudoku RegularSudoku;


        public SudokuInputModeState(Sudoku sudoku)
        {
            RegularSudoku = sudoku;
        }


        public abstract void HandleInput(Leaf cell, int value);
    }
}




