namespace Business_Model.Interfaces
{
    public interface ISudoku
    {
        void AcceptLoad(ISudokuLoadVisitor visitor);
        void AcceptPrint(ISudokuPrintVisitor visitor);

    }


}

