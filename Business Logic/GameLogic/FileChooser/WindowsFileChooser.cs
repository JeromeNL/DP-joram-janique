namespace Business_Logic.GameLogic
{
    public static partial class Game
    {
        public class WindowsFileChooser : IFileChooser
        {
            public FileInfo ChooseFile()
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string parentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;

                string relativePath = "Puzzles";
                string directoryPath = Path.Combine(parentDirectory, relativePath);


                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("Directory does not exist.");
                    throw new NotSupportedException("Directory does not exist.");
                }

                var files = Directory.GetFiles(directoryPath);
                if (files.Length == 0)
                {
                    Console.WriteLine("Directory is empty.");
                }

                while (true)
                {
                    Console.WriteLine("Please select a file:");
                    for (int i = 0; i < files.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                    }

                    int selectedFileNumber;
                    bool validInput = Int32.TryParse(Console.ReadLine(), out selectedFileNumber);
                    if (validInput && selectedFileNumber >= 1 && selectedFileNumber <= files.Length)
                    {
                        string selectedFilePath = files[selectedFileNumber - 1];
                        Console.WriteLine($"You've selected: {selectedFilePath}");
                        
                        FileInfo fileInfo = new FileInfo(selectedFilePath);

                        Console.Clear();
                        return fileInfo;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.\n");
                    }
                }
            }
        }
    }
}
