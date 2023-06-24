
using Business_Model.Abstractions;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Player
{
    public class HelperNumberInputState : SudokuInputModeState
    {
        public HelperNumberInputState(Sudoku sudoku) : base(sudoku)
        {
        }


        public override void HandleInput(Leaf cell, int value)
        {
            HandleHelperNumberInput(cell, value, RegularSudoku);
        }


        private void HandleHelperNumberInput(Leaf cell, int value, Sudoku RegularSudoku)
        {
            Console.WriteLine("Current helper values for this cell are: " + string.Join(", ", cell.helperValues));
            Console.Write("Enter a helper value to add or remove, or 'b' to go back: ");
            string input = Console.ReadLine().ToLower();
            if (input == "b") return;
            if (!int.TryParse(input, out value))
            {
                Console.WriteLine("Invalid input. Please enter a valid number as a helper value.");
                return;
            }
            if (cell.currentValue != 0)
            {
                Console.WriteLine("Cannot add a helper value to a cell with a definitive number.");
                return;
            }
            if (!cell.helperValues.Contains(value))
            {
                cell.AddHelperValue(value);
                Console.WriteLine($"Added helper value: {value}");
            }
            else
            {
                cell.RemoveHelperValue(value);
                Console.WriteLine($"Removed helper value: {value}");
            }
        }
    }
}




