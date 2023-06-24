namespace Business_Logic.GameLogic
{
    public static partial class Game
    {
        public static class FileChooserFactory
        {
            public static IFileChooser Create()
            {
                Console.WriteLine("Do you use Windows? Y/N");
                string os = Console.ReadLine().ToLower();

                if (os == "y")
                {
                    return new WindowsFileChooser();
                }
                else if (os == "n")
                {
                    return new MacFileChooser();
                }
                else { 
                Console.WriteLine("Reconsider your lifechoices carefully");
                throw new NotSupportedException("Operating system not supported");
                }
            }
        }











    }
}
