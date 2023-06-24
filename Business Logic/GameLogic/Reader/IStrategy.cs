using Business_Model.Interfaces;

namespace Business_Logic.GameLogic.Reader
{
    public interface IStrategy
    {
        ISudoku CreateBasedOnFile(FileInfo fileInfo);
    }



}


