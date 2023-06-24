using Business_Model.Models;

namespace Business_Logic.GameLogic.Reader
{
    public interface ISudokuLoadVisitor
    {
        void VisitLoadRegularSudoku(RegularSudoku sudoku, FileInfo  fileInfo);
        void VisitLoadSamuraiSudoku(SamuraiSudoku sudoku, FileInfo fileInfo);
        void VisitLoadJigsawSudoku(JigsawSudoku sudoku, FileInfo fileInfo);
        void VisitLoadComposite(Composite composite);
        void VisitLoadLeaf(Leaf leaf);
    }
}

