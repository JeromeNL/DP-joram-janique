using Business_Logic.GameLogic.Printer;
using Business_Logic.GameLogic.Reader;

namespace Business_Model.Interfaces
{
    public interface ISudoku 
    {
        void AcceptLoad(ISudokuLoadVisitor visitor);
        void AcceptPrint(ISudokuPrintVisitor visitor);
    }
}

