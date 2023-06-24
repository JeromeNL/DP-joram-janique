using Business_Logic.GameLogic.Printer;
using Business_Model.Abstractions;
using Business_Model.Interfaces;
using Business_Model.Models;
using System.Diagnostics;

namespace Business_Logic.GameLogic.Solver
{
    class JigsawSolverStrategy : ISolveStrategy
    {
        public JigsawSolverStrategy()
        {

        }

        public void SolveSudoku(Sudoku JigsawSudoku)
        {
            JigsawSudoku.timer = Stopwatch.StartNew();
      
            BacktrackSolve(JigsawSudoku);

            var x = JigsawSudoku.components.Count() * 2;
            var y = JigsawSudoku.components.Count();
            Console.SetCursorPosition(x * 2, y);
            Console.WriteLine(" ");

            JigsawSudoku.timer.Stop();
            SudokuPrintVisitor visitor = new SudokuPrintVisitor();
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Printing the solved Board");
            Console.WriteLine();
            JigsawSudoku.AcceptPrint(visitor);
            Console.WriteLine("Solving time: " + JigsawSudoku.timer.Elapsed);
        }


        public bool BacktrackSolve(Sudoku JigsawSudoku)
        {

            Leaf cell = FindUnassignedCell(JigsawSudoku);
            if (cell == null)
                return true;

            foreach (int value in GetLegalValues(cell, JigsawSudoku))
            {
                cell.currentValue = value;
                Console.SetCursorPosition(cell.position.X * 2, cell.position.Y);
                Console.Write($"{cell.currentValue} ");

                if (BacktrackSolve(JigsawSudoku))
                    return true;

                cell.currentValue = 0;
                Console.SetCursorPosition(cell.position.X * 2, cell.position.Y);
                
                Console.Write("  ");
            }

            return false;
        }

        public List<int> GetLegalValues(Leaf cell, Sudoku JigsawSudoku)
        {
            List<int> legalValues = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (JigsawSudoku.IsSafeToPlaceValue(cell, i, JigsawSudoku))
                {
                    legalValues.Add(i);
                }
            }
            return legalValues;
        }

        public Leaf FindUnassignedCell(Sudoku JigsawSudoku)
        {
            Leaf bestCell = null;
            int bestCount = int.MaxValue;

            foreach (IComponent component in JigsawSudoku.components)
            {
                if (component is Composite group)
                {
                    foreach (Leaf cell in group.cells)
                    {
                        if (cell.currentValue == 0)
                        {
                            int count = GetLegalValues(cell, JigsawSudoku).Count;
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
    }
}




