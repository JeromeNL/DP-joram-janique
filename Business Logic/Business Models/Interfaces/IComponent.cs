using Business_Logic.GameLogic.Reader;

namespace Business_Model.Interfaces
{
    public interface IComponent
    {
        void AcceptLoad(ISudokuLoadVisitor visitor);
    }
}

