﻿using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Solver;
using Business_Model.Abstractions;
using Business_Model.Interfaces;
using Business_Model.Models;
using System.Diagnostics;
using System.Xml;

namespace Business_Logic.GameLogic.Player
{
    class SamuraiPlayStrategy : IPlayStrategy
    {
        private const int extraSpaceValue = 2;

        public SamuraiPlayStrategy()
        {

        }


        int min = 0;
        int max = 9;
        int baseStart = 1;
        int baseEnd = 5;

        public void PlaySudoku(Sudoku sudoku, FileInfo fileInfo)
        {
            while (true)
            {
                sudoku.AcceptPrint(new SudokuPrintVisitor());
                Console.WriteLine();
                Console.WriteLine("Enter your move (board row column value), 'c' to switch mode, or 'q' to quit: ");
                string input = Console.ReadLine().ToLower();
                if (input == "s") { MainSolver.SolveSudoku(sudoku, fileInfo); break; }
                if (input == "q") { Console.WriteLine("Quitting the game..."); break; }
                if (input == "c") { ToggleSudokuMode(sudoku); continue; }
                if (!TryParseMoveInput(input, out int board, out int row, out int column, out int value)) continue;

                Leaf cell = GetCell(board - 1, row - 1, column - 1, sudoku);
                if (cell == null || !ValidateMove(cell, value, sudoku)) continue;

                PerformMove(cell, value, sudoku);
                Console.Clear();

                if (!IsSamuraiSudokuSolved(sudoku)) continue;

                Console.WriteLine("Congratulations! You solved the Samurai Sudoku puzzle.");
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

        private bool TryParseMoveInput(string input, out int board, out int row, out int column, out int value)
        {
            board = row = column = value = min;
            string[] parts = input.Split(' ');
            if (parts.Length != 4 || !int.TryParse(parts[0], out board) || !int.TryParse(parts[1], out row) || !int.TryParse(parts[2], out column) || !int.TryParse(parts[3], out value)
                || board < baseStart || board > baseEnd || row < baseStart || row > max || column < baseStart || column > max || value < min || value > max)
            {
                Console.WriteLine("Invalid input. Please enter board (1-5), row, column, and value as valid numbers between 1 and 9.");
                return false;
            }
            return true;
        }

        private Leaf GetCell(int board, int row, int column, Sudoku sudoku)
        {
            Composite boardComposite = GetBoardComponent(board - 1, sudoku) as Composite;
            if (boardComposite != null)
            {
                foreach (IComponent component in boardComposite.cells)
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
            }
            return null;
        }


        private bool ValidateMove(Leaf cell, int value, Sudoku sudoku)
        {
            if (cell.initialValue != min)
            {
                Console.WriteLine("Invalid move. Cannot modify the initial values.");
                return false;
            }
            else if (cell.currentValue == value)
            {
                return true;
            }
            if (sudoku.IsSafeToPlaceValue(cell, value, sudoku)) return true;
            Console.WriteLine("Invalid move. The value conflicts with an existing value in the same row, column, or box.");
            return false;
        }


        private void PerformMove(Leaf cell, int value, Sudoku JigsawSudoku)
        {
            JigsawSudoku.CurrentState.HandleInput(cell, value);
        }


        private void HandleHelperNumberInput(Leaf cell, int value)
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
            if (cell.currentValue != min)
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

        private void SetCellValue(Leaf cell, int value)
        {
            if (cell != null && cell.initialValue == min)
            {
                cell.currentValue = value;
                Console.SetCursorPosition(cell.position.X * extraSpaceValue, cell.position.Y);
                Console.Write(value.ToString());
            }
        }


        private Composite GetBoardComponent(int index, Sudoku sudoku)
        {
            if (index >= min && index < sudoku.components.Count)
            {
                return sudoku.components[index] as Composite;
            }

            return null;
        }


        private bool IsSamuraiSudokuSolved(Sudoku sudoku)
        {
            foreach (IComponent component in sudoku.components)
            {
                if (component is Composite board)
                {
                    foreach (Leaf cell in board.cells)
                    {
                        if (cell.currentValue == min || !sudoku.IsSafeToPlaceValue(cell, cell.currentValue, sudoku))
                            return false;
                    }
                }
            }
            return true;
        }
    }
}







