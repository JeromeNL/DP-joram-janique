
using Business_Logic.GameLogic.Printer;
using Business_Model.Abstractions;
using Business_Model.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace Business_Logic.GameLogic.Solver
{
    class RegularSolverStrategy : ISolveStrategy
    {
        public RegularSolverStrategy()
        {


        }


        public void SolveSudoku(Sudoku RegularSudoku)
        {
            RegularSudoku.timer = Stopwatch.StartNew(); // Start de timer
            BacktrackSolve(RegularSudoku);
            RegularSudoku.timer.Stop();
            SudokuPrintVisitor visitor = new SudokuPrintVisitor();
            RegularSudoku.AcceptPrint(visitor);
            Console.WriteLine("Solving time: " + RegularSudoku.timer.Elapsed);
        }


        public bool BacktrackSolve(Sudoku RegularSudoku)
        {
            Leaf cell = FindUnassignedCell(RegularSudoku);
            if (cell == null)
                return true;


            foreach (int value in GetLegalValues(cell, RegularSudoku))
            {
                cell.currentValue = value;
                Console.SetCursorPosition(cell.position.X * 2, cell.position.Y);
                Console.Write($" {cell.currentValue} ");

                if (BacktrackSolve(RegularSudoku))
                    return true;

                cell.currentValue = 0;
                Console.SetCursorPosition(cell.position.X * 2, cell.position.Y);
                Console.Write("  ");
            }

            return false;
        }

        public List<int> GetLegalValues(Leaf cell, Sudoku RegularSudoku)
        {
            List<int> legalValues = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (IsSafeToPlaceValue(cell, i, RegularSudoku))
                {
                    legalValues.Add(i);
                }
            }
            return legalValues;
        }

        public Leaf FindUnassignedCell(Sudoku RegularSudoku)
        {
            Leaf bestCell = null;
            int bestCount = int.MaxValue;

            foreach (IComponent component in RegularSudoku.components)
            {
                if (component is Composite group)
                {
                    foreach (Leaf cell in group.cells)
                    {
                        if (cell.currentValue == 0)
                        {
                            int count = GetLegalValues(cell, RegularSudoku).Count;
                            if (count < bestCount)
                            {
                                bestCell = cell;
                                bestCount = count;
                            }
                        }
                    }
                }
            }

            return bestCell;
        }

        public bool IsSafeToPlaceValue(Leaf cell, int value, Sudoku RegularSudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                if (GetCellValue(cell.position.X, i, RegularSudoku) == value || GetCellValue(i, cell.position.Y, RegularSudoku) == value)
                    return false;
            }

            Composite group = GetGroup(cell, RegularSudoku);
            foreach (Leaf groupCell in group.cells)
            {
                if (groupCell.currentValue == value)
                    return false;
            }

            return true;
        }

        public int GetCellValue(int x, int y, Sudoku RegularSudoku)
        {
            foreach (IComponent component in RegularSudoku.components)
            {
                if (component is Composite group)
                {
                    foreach (Leaf cell in group.cells)
                    {
                        if (cell.position.X == x && cell.position.Y == y)
                            return cell.currentValue;
                    }
                }
            }

            return 0;
        }

        public Composite GetGroup(Leaf cell, Sudoku RegularSudoku)
        {
            foreach (IComponent component in RegularSudoku.components)
            {
                if (component is Composite group)
                {
                    if (group.cells.Contains(cell))
                        return group;
                }
            }
            return null;
        }


    }
}







