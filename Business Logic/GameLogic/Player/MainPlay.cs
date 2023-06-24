using Business_Model.Abstractions;


namespace Business_Logic.GameLogic.Player
{
    public static class MainPlay
    {
        public static void PlaySudoku(Sudoku a, FileInfo fileInfo)
        {
            PlayContext context = new PlayContext();

            List<string> regularSudokuExtensions = new List<string>
            {
                ".4x4",
                ".6x6",
                ".9x9"
            };

            if (regularSudokuExtensions.Contains(fileInfo.Extension))
            {
                context.SetPlayStrategy(new RegularPlayStrategy());
            }
            else if (fileInfo.Extension.Equals(".samurai"))
            {
                context.SetPlayStrategy(new SamuraiPlayStrategy());
            }
            else if (fileInfo.Extension.Equals(".jigsaw"))
            {
                context.SetPlayStrategy(new JigsawPlayStrategy());
            }
            else
            {
                throw new DirectoryNotFoundException();
            }

            context.GetPlay(a, fileInfo);
        }
    }
}