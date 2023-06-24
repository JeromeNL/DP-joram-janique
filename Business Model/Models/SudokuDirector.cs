using Business_Model.Interfaces;

namespace Business_Model.Models
{
    public class SudokuDirector
    {
        private SudokuBuilder builder;
        public SudokuDirector(SudokuBuilder builder)
        {
            this.builder = builder;
        }

        public ISudoku CreateRegularSudoku()
        {
            return builder.CreateRegularSudoku().Build();
        }

        public ISudoku CreateSamuraiSudoku()
        {
            return builder.CreateSamuraiSudoku().Build();
        }

        public ISudoku CreateJigsawSudoku()
        {
            return builder.CreateJigsawSudoku().Build();
        }
    }


}

