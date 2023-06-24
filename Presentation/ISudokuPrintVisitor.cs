namespace Input_and_Output
{
    public interface ISudokuPrintVisitor
    {
        void VisitPrintSudoku(RegularSudoku sudoku);
        void VisitPrintSudoku(SamuraiSudoku sudoku);
        void VisitPrintSudoku(JigsawSudoku sudoku);
    }


}

