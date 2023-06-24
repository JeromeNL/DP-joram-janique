using Business_Logic.GameLogic.Player;
using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Reader;
using Business_Model.Abstractions;
using Business_Model.Interfaces;
using System.Diagnostics;

namespace Business_Logic.GameLogic
{



    public static partial class Game
    {
        static FileInfo fileInfo;
        static ISudoku sudoku;

        public static void Run()
        {
            bool running = true;
            while (running)
            {
                fileInfo = FileChooserFactory.Create().ChooseFile();
                sudoku = MainReader.ReadGameData(fileInfo);
                MainPlay.PlaySudoku((Sudoku)sudoku, fileInfo);

                   Console.WriteLine("Do you want to Continue? Type y otherwise the program will stop");

            string KeepRunning = Console.ReadLine().ToLower();

            if (KeepRunning.Equals("y"))
            {
                Console.Clear();
                Console.WriteLine("Lets Proceed Then :)");
                Console.WriteLine();
                Console.WriteLine();
                ISudoku clonedSudoku = ((ICloneableSudoku)sudoku).Clone();
                MainPlay.PlaySudoku((Sudoku)clonedSudoku, fileInfo);
            }
            else
            {
                running = false;
            }

            }

         
        }
    }
}
