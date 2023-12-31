﻿using Business_Model.Abstractions;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Player
{
    public class DefinitiveNumberInputState : SudokuInputModeState
    {
        private const int extraSpaceValue = 2;

        public DefinitiveNumberInputState(Sudoku sudoku) : base(sudoku)
        {
        }


        public override void HandleInput(Leaf cell, int value)
        {
            SetCellValue(cell, value, RegularSudoku);
        }


        private void SetCellValue(Leaf cell, int value, Sudoku RegularSudoku)
        {
            if (cell != null && cell.initialValue == 0)
            {
                cell.currentValue = value;
                Console.SetCursorPosition(cell.position.X * extraSpaceValue, cell.position.Y);
                Console.Write(value.ToString());
            }
        }
    }
}




