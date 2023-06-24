using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Solver;
using Business_Model.Abstractions;
using Business_Model.Interfaces;
using Business_Model.Models;

namespace Business_Logic.GameLogic.Player
{
    class RegularPlayStrategy : IPlayStrategy
    {
        public RegularPlayStrategy()
        {
        }

        int min = 0;
        int max = 9;

        public void PlaySudoku(Sudoku RegularSudoku, FileInfo fileInfo)
        {

            RegularSudoku.CurrentState = new DefinitiveNumberInputState(RegularSudoku);

            while (true)
            {
                RegularSudoku.AcceptPrint(new SudokuPrintVisitor());
                Console.WriteLine();
                Console.WriteLine("Enter your move (row column value), 'c' to switch mode, or 'q' to quit: ");
                string input = Console.ReadLine().ToLower();
                if (input == "s") { MainSolver.SolveSudoku(RegularSudoku, fileInfo); break; }
                if (input == "q") { Console.WriteLine("Quitting the game..."); break; }
                if (input == "c") { ToggleSudokuMode(RegularSudoku); continue; }
                if (!TryParseMoveInput(input, out int row, out int column, out int value)) continue;

                Leaf cell = GetCell(row - 1, column - 1, RegularSudoku);
                if (cell == null || !ValidateMove(cell, value, RegularSudoku)) continue;

                PerformMove(cell, value, RegularSudoku);
                Console.Clear();

                if (!IsSudokuSolved(RegularSudoku)) continue;

                Console.WriteLine("Congratulations! You solved the Sudoku puzzle.");

                break;
            }
        }


        private void ToggleSudokuMode(Sudoku JigsawSudoku)
        {
            if (JigsawSudoku.CurrentState is DefinitiveNumberInputState)
            {
                JigsawSudoku.CurrentState = new HelperNumberInputState(JigsawSudoku);
                Console.WriteLine("Switched to Helper Number Input mode");
            }
            else
            {
                JigsawSudoku.CurrentState = new DefinitiveNumberInputState(JigsawSudoku);
                Console.WriteLine("Switched to Definitive Number Input mode");
            }
        }


        private bool TryParseMoveInput(string input, out int row, out int column, out int value)
        {
            row = column = value = min;
            string[] parts = input.Split(' ');
            if (parts.Length != 3 || !int.TryParse(parts[0], out row) || !int.TryParse(parts[1], out column) || !int.TryParse(parts[2], out value)
                || row < 1 || row > max || column < 1 || column > max || value < min || value > max)
            {
                Console.WriteLine("Invalid input. Please enter row, column, and value as valid numbers between 1 and 9.");
                return false;
            }
            return true;
        }


        private bool ValidateMove(Leaf cell, int value, Sudoku RegularSudoku)
        {
            if (cell.initialValue != min)
            {
                Console.WriteLine("Invalid move. Cannot modify the initial values.");
                return false;
            }
            else if (cell.initialValue == min && value == min)
            {
                return true;
            }
            if (RegularSudoku.IsSafeToPlaceValue(cell, value, RegularSudoku)) return true;
            Console.WriteLine("Invalid move. The value conflicts with an existing value in the same row, column, or box.");
            return false;
        }


        private void PerformMove(Leaf cell, int value, Sudoku JigsawSudoku)
        {
            JigsawSudoku.CurrentState.HandleInput(cell, value);
        }


        private Leaf GetCell(int row, int column, Sudoku RegularSudoku)
        {
            foreach (IComponent component in RegularSudoku.components)
            {
                if (component is Composite group)
                {
                    foreach (Leaf cell in group.cells)
                    {
                        if (cell.position.X == column && cell.position.Y == row)
                        {
                            return cell;
                        }
                    }
                }
            }
            return null;
        }


        private bool IsSudokuSolved(Sudoku RegularSudoku)
        {
            foreach (IComponent component in RegularSudoku.components)
            {
                if (component is Composite group)
                {
                    foreach (Leaf cell in group.cells)
                    {
                        if (cell.currentValue == min || !RegularSudoku.IsSafeToPlaceValue(cell, cell.currentValue, RegularSudoku))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}






