using Business_Model.Models;
using System.ComponentModel;

namespace Business_Logic.GameLogic.Reader
{
    public class SudokuLoadVisitor : ISudokuLoadVisitor
    {
        public void VisitLoadLeaf(Leaf leaf)
        {
            leaf.currentValue = 42;

        }

        public void VisitLoadComposite(Composite composite)
        {
            // Implement the visiting logic for a Composite
            foreach (IComponent cell in composite.cells)
            {
               // cell.AcceptLoad(this);
            }
            //TODO
        }

        public void VisitLoadRegularSudoku(RegularSudoku sudoku, FileInfo fileInfo)
        {
            string sudokuData = File.ReadAllText(fileInfo.FullName);
            char lastLetter = fileInfo.FullName[fileInfo.FullName.Length - 1];
            int gridSize = int.Parse(lastLetter.ToString());

            int[,] values = ParseRegularSudokuData(sudokuData, gridSize);

            int blockSize = (int)Math.Sqrt(gridSize);
            for (int blockRow = 0; blockRow < blockSize; blockRow++)
            {
                for (int blockCol = 0; blockCol < blockSize; blockCol++)
                {
                    Composite component = new Composite();
                    for (int cellRow = 0; cellRow < blockSize; cellRow++)
                    {
                        for (int cellCol = 0; cellCol < blockSize; cellCol++)
                        {
                            int rowIndex = blockRow * blockSize + cellRow;
                            int colIndex = blockCol * blockSize + cellCol;
                            Leaf leaf = new Leaf();
                            leaf.currentValue = values[rowIndex, colIndex];
                            leaf.initialValue = values[rowIndex, colIndex];
                            leaf.position = new Position(rowIndex, colIndex);
                            component.AddCell(leaf);
                        }
                    }
                    sudoku.AddComponent(component);
                }
            }
        }


        public int[,] ParseRegularSudokuData(string data, int size)
        {
            int[,] values = new int[size, size];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int.TryParse(data[index].ToString(), out values[i, j]);
                    index++;
                }
            }
            return values;
        }

        public void VisitLoadJigsawSudoku(JigsawSudoku sudoku, FileInfo fileInfo)
        {
            string jigsawData = File.ReadAllText(fileInfo.FullName).Substring(10);
            string[] components = jigsawData.Split('=');
            Dictionary<int, Composite> groups = new Dictionary<int, Composite>();

            int index = 0;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    int value = int.Parse(components[index].Substring(0, 1));
                    int group = int.Parse(components[index].Substring(2, 1));

                    Leaf leaf = new Leaf
                    {
                        currentValue = value,
                        initialValue = value,
                        position = new Position(x, y)
                    };

                    if (!groups.ContainsKey(group))
                    {
                        groups[group] = new Composite();
                    }

                    groups[group].AddCell(leaf);
                    index++;
                }
            }

            foreach (Composite group in groups.Values)
            {
                sudoku.AddComponent(group);
            }
        }

        public void VisitLoadSamuraiSudoku(SamuraiSudoku sudoku, FileInfo fileInfo)
        {
            string[] lines = File.ReadAllLines(fileInfo.FullName);

            if (lines.Length != 5)
            {
                throw new FormatException("Samurai Sudoku data is in the wrong format.");
            }

            int[][] boardOffsets = new int[][]
            {
            new int[] { 0, 0 },
            new int[] { 0, 12 },
            new int[] { 6, 6 },
            new int[] { 12, 0 },
            new int[] { 12, 12 }
            };

            for (int i = 0; i < 5; i++)
            {
                string line = lines[i];

                if (line.Length != 81)
                {
                    throw new FormatException("Samurai Sudoku data is in the wrong format.");
                }

                Composite board = new Composite();

                for (int boxRow = 0; boxRow < 3; boxRow++)
                {
                    for (int boxCol = 0; boxCol < 3; boxCol++)
                    {
                        Composite group = new Composite();

                        for (int cellRow = 0; cellRow < 3; cellRow++)
                        {
                            for (int cellCol = 0; cellCol < 3; cellCol++)
                            {
                                int index = (boxRow * 3 + cellRow) * 9 + boxCol * 3 + cellCol;

                                Position pos = new Position(boardOffsets[i][1] + boxCol * 3 + cellCol, boardOffsets[i][0] + boxRow * 3 + cellRow);

                                char cellValueChar = line[index];
                                int cellValue = 0;

                                if (cellValueChar != '0')
                                {
                                    if (!int.TryParse(cellValueChar.ToString(), out cellValue))
                                    {
                                        throw new FormatException("Samurai Sudoku data is in the wrong format.");
                                    }
                                }

                                Leaf cellLeaf;
                                if (sudoku.sharedLeaves.ContainsKey(pos))
                                {
                                    cellLeaf = sudoku.sharedLeaves[pos];
                                }
                                else
                                {
                                    cellLeaf = new Leaf();
                                    cellLeaf.position = pos;
                                    sudoku.sharedLeaves[pos] = cellLeaf;
                                }
                                cellLeaf.currentValue = cellValue;
                                cellLeaf.initialValue = cellValue;

                                group.AddCell(cellLeaf);
                            }
                        }
                        board.AddCell(group);
                    }
                }
                sudoku.AddComponent(board);
            }
        }
    }
}

