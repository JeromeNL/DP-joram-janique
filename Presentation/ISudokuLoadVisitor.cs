namespace Input_and_Output
{
    public interface ISudokuLoadVisitor
    {
        void VisitLoadRegularSudoku(RegularSudoku sudoku, string filePath);
        void VisitLoadSamuraiSudoku(SamuraiSudoku sudoku, string filePath);
        void VisitLoadJigsawSudoku(JigsawSudoku sudoku, string filePath);
        void VisitLoadComposite(Composite composite);
        void VisitLoadLeaf(Leaf leaf);
    }


}

