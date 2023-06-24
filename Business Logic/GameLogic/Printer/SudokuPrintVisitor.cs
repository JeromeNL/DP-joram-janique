using Business_Model.Models;

namespace Business_Logic.GameLogic.Printer
{
    public class SudokuPrintVisitor : ISudokuPrintVisitor
    {
        ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Blue,
        ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.DarkRed, ConsoleColor.DarkGreen,
        ConsoleColor.DarkYellow, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.DarkCyan,
        ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Black };
        private const int ExtraSpaceValue = 2;
        private const int BoardSize = 21;

        public void VisitPrintSudoku(RegularSudoku sudoku)
        {
            if (sudoku.components.Count == 0)
            {
                Console.WriteLine("No components found in the Sudoku grid");
                return;
            }

            int blockSize = (int)Math.Sqrt(sudoku.components.Count);

            for (int blockRow = 0; blockRow < blockSize; blockRow++)
            {
                for (int cellRow = 0; cellRow < blockSize; cellRow++)
                {
                    for (int blockColumn = 0; blockColumn < blockSize; blockColumn++)
                    {
                        Composite composite = (Composite)sudoku.components[blockRow * blockSize + blockColumn];

                        if (composite.cells.Count == 0)
                        {
                            Console.WriteLine($"No cells found in component {blockRow * blockSize + blockColumn + 1}");
                            continue;
                        }

                        Console.ForegroundColor = colors[blockRow * blockSize + blockColumn % colors.Length];

                        for (int cellColumn = 0; cellColumn < blockSize; cellColumn++)
                        {
                            Leaf leaf = (Leaf)composite.cells[cellRow * blockSize + cellColumn];
                            Console.Write(leaf.currentValue.ToString() + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }

            Console.ResetColor();

        }


        public void VisitPrintSudoku(JigsawSudoku sudoku)
        {
            for (int i = 0; i < sudoku.components.Count; i++)
            {
                Composite group = (Composite)sudoku.components[i];
                Console.ForegroundColor = colors[i % colors.Length];
                foreach (Leaf cell in group.cells)
                {
                    Console.SetCursorPosition(cell.position.X * ExtraSpaceValue, cell.position.Y); 
                    Console.Write($"{cell.currentValue} ");
                }
            }
            Console.ResetColor();
            Console.WriteLine();

        }


        public void VisitPrintSudoku(SamuraiSudoku sudoku)
        {
            ConsoleColor overlapColor = ConsoleColor.Magenta;
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    Position pos = new Position(x, y);
                    if (sudoku.sharedLeaves.ContainsKey(pos))
                    {
                        if (y < 9 && x < 9 || y > 11 && x > 11)
                        {
                            Console.ForegroundColor = colors[0];
                            PrintCellValue(pos, sudoku);
                        }
                        else if (y < 9 && x > 11 || y > 11 && x < 9) // Top right or bottom left Sudoku
                        {
                            Console.ForegroundColor = colors[1];
                            PrintCellValue(pos, sudoku);
                        }
                        else if (y >= 6 && y <= 14 && x >= 6 && x <= 14 && !(y >= 9 && y <= 11 && x >= 9 && x <= 11)) // Middle Sudoku
                        {
                            Console.ForegroundColor = colors[2];
                            PrintCellValue(pos, sudoku);
                        }
                        else if (y >= 12 && y <= 14 && x >= 12 && x <= 14 || y >= 6 && y <= 8 && x >= 6 && x <= 8) // Bottom right and top left overlapping area
                        {
                            Console.ForegroundColor = colors[3];
                            PrintCellValue(pos, sudoku);
                        }
                        else if (y >= 6 && y <= 8 && x >= 12 && x <= 14 || y >= 12 && y <= 14 && x >= 6 && x <= 8) // Top right and bottom left overlapping area
                        {
                            Console.ForegroundColor = colors[4];
                            PrintCellValue(pos, sudoku);
                        }
                        else // Overlapping area in the middle
                        {
                            Console.ForegroundColor = overlapColor;
                            PrintCellValue(pos, sudoku);
                        }
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
            }

        }


        private void PrintCellValue(Position pos, SamuraiSudoku sudoku)
        {
            int cellValue = sudoku.sharedLeaves[pos].currentValue;

            Console.Write(cellValue == 0 ? " 0" : " " + cellValue.ToString());
        }
    }
}

