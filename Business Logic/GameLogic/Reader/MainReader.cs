using Business_Model.Interfaces;

namespace Business_Logic.GameLogic.Reader
{
    public static class MainReader
    {
        public static ISudoku ReadGameData(FileInfo fileInfo)
        {
            Context context = new Context();
            List<string> regularSudokuExtensions = new List<string>
            {
                ".4x4",
                ".6x6",
                ".9x9"
            };

            if (regularSudokuExtensions.Contains(fileInfo.Extension))
            {
                context.SetStrategy(new RegularReaderStrategy());
            }
            else if (fileInfo.Extension.Equals(".samurai"))
            {
                context.SetStrategy(new SamuraiReaderStrategy());
            }
            else if (fileInfo.Extension.Equals(".jigsaw"))
            {
                context.SetStrategy(new JigsawReaderStrategy());
            }
            else { 
                throw new DirectoryNotFoundException();
            }

            return context.GetGame(fileInfo);
        }
    }
}