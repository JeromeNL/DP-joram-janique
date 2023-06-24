using Business_Model.Interfaces;

namespace Business_Logic.GameLogic
{
    public interface ICloneableSudoku : ISudoku
    {
        ISudoku Clone();
    }
}
