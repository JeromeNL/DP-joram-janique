using Business_Logic.GameLogic.Printer;

using Business_Model.Abstractions;
using Business_Model.Interfaces;
using Business_Model.Models;
using System.Diagnostics;
using System.Xml;

namespace Business_Logic.GameLogic.Solver
{
    class SamuraiSolverStrategy : ISolveStrategy
    {
        public SamuraiSolverStrategy()
        {


        }
        public void SolveSudoku(Sudoku SamuraiSudoku)
        {

            SamuraiSudoku.timer = Stopwatch.StartNew();
            BacktrackSolve(SamuraiSudoku);
            
            var x = 21 * 2;
            var y = 21;
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(" ");


            SamuraiSudoku.timer.Stop();
            SudokuPrintVisitor visitor = new SudokuPrintVisitor();
            Console.WriteLine(" ");
            Console.WriteLine("Printing the solved Board");
            Console.WriteLine();
            SamuraiSudoku.AcceptPrint(visitor);
            Console.WriteLine("Solving time: " + SamuraiSudoku.timer.Elapsed);
        }


        public bool BacktrackSolve(Sudoku SamuraiSudoku)
        {
            Leaf cell = FindUnassignedCell(SamuraiSudoku);
            if (cell == null)
                return true;

            for (int value = 1; value <= 9; value++)
            {
                if (IsSafeToPlaceValue(cell, value, SamuraiSudoku))
                {
                    cell.currentValue = value;
                    Console.SetCursorPosition(cell.position.X * 2, cell.position.Y);
                    Console.Write($" {cell.currentValue} ");

                    if (BacktrackSolve(SamuraiSudoku))
                        return true;

                    cell.currentValue = 0;
                    Console.SetCursorPosition(cell.position.X * 2, cell.position.Y);
                    Console.Write("  ");
                }
            }

            return false;
        }

        private List<int> GetLegalValues(Leaf cell, Sudoku SamuraiSudoku)
        {
            List<int> legalValues = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (IsSafeToPlaceValue(cell, i, SamuraiSudoku))
                {
                    legalValues.Add(i);
                }
            }
            return legalValues;
        }

        private Leaf FindUnassignedCell(Sudoku SamuraiSudoku)
        {
            Leaf bestCell = null;
            int bestCount = int.MaxValue;

            foreach (IComponent component in SamuraiSudoku.components)
            {
                if (component is Composite board)
                {
                    foreach (Composite group in board.cells)
                    {
                        foreach (Leaf cell in group.cells)
                        {
                            if (cell.currentValue == 0)
                            {
                                int count = GetLegalValues(cell, SamuraiSudoku).Count;
                                if (count < bestCount)
                                {
                                    bestCell = cell;
                                    bestCount = count;
                                }
                            }
                        }
                    }
                }
            }

            return bestCell;
        }

        private bool IsSafeToPlaceValue(Leaf cell, int value, Sudoku SamuraiSudoku)
        {
            if (!IsSafeInOwnBoard(cell, value, SamuraiSudoku))
                return false;
            if (!IsSafeInOwnGroup(cell, value, SamuraiSudoku))
                return false;
            if (!IsSafeInOverlappingBoard(cell, value, SamuraiSudoku))
                return false;

            return true;
        }

        private bool IsSafeInOwnBoard(Leaf cell, int value, Sudoku SamuraiSudoku)
        {
            Composite board = GetBoard(cell, SamuraiSudoku);
            for (int i = 0; i < 9; i++)
            {
                if (GetCellValue(cell.position.X, i, board) == value || GetCellValue(i, cell.position.Y, board) == value)
                    return false;
            }

            return true;
        }

        private bool IsSafeInOwnGroup(Leaf cell, int value, Sudoku SamuraiSudoku)
        {
            Composite group = GetGroup(cell, SamuraiSudoku);
            foreach (Leaf groupCell in group.cells)
            {
                if (groupCell.currentValue == value)
                    return false;
            }

            return true;
        }

        private bool IsSafeInOverlappingBoard(Leaf cell, int value, Sudoku SamuraiSudoku)
        {
            foreach (var pair in SamuraiSudoku.sharedLeaves)
            {
                Leaf overlappingCell = pair.Value;
                if (overlappingCell.position.X == cell.position.X && overlappingCell.position.Y == cell.position.Y)
                {
                    Composite overlappingBoard = GetBoard(overlappingCell, SamuraiSudoku);
                    if (overlappingBoard != GetBoard(cell, SamuraiSudoku)) // Avoid checking the same board twice
                    {
                        // Limit the overlapping check to the central square of the Samurai Sudoku
                        if (overlappingCell.position.X >= 6 && overlappingCell.position.X <= 14 && overlappingCell.position.Y >= 6 && overlappingCell.position.Y <= 14)
                        {
                            for (int i = 6; i <= 14; i++)
                            {
                                if (GetCellValue(overlappingCell.position.X, i, overlappingBoard) == value || GetCellValue(i, overlappingCell.position.Y, overlappingBoard) == value)
                                    return false;
                            }

                            Composite overlappingGroup = GetGroup(overlappingCell, SamuraiSudoku);
                            foreach (Leaf overlappingGroupCell in overlappingGroup.cells)
                            {
                                if (overlappingGroupCell.currentValue == value)
                                    return false;
                            }
                        }
                    }
                }
            }

            return true;
        }


        private int GetCellValue(int x, int y, Composite board)
        {
            foreach (Composite group in board.cells)
            {
                foreach (Leaf cell in group.cells)
                {
                    if (cell.position.X == x && cell.position.Y == y)
                        return cell.currentValue;
                }
            }

            return 0;
        }

        private Composite GetBoard(Leaf cell, Sudoku SamuraiSudoku)
        {
            foreach (IComponent component in SamuraiSudoku.components)
            {
                if (component is Composite board)
                {
                    foreach (Composite group in board.cells)
                    {
                        if (group.cells.Contains(cell))
                            return board;
                    }
                }
            }

            return null;
        }

        private Composite GetGroup(Leaf cell, Sudoku SamuraiSudoku)
        {
            Composite board = GetBoard(cell, SamuraiSudoku);

            foreach (Composite group in board.cells)
            {
                if (group.cells.Contains(cell))
                    return group;
            }

            return null;
        }



    }
}







