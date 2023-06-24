using Business_Model.Interfaces;

namespace Business_Logic.GameLogic.Reader
{
    class Context
    {
        private IStrategy _Strategy;

        public Context()
        {
        }

        public Context(IStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public void SetStrategy(IStrategy Strategy)
        {
            _Strategy = Strategy;
        }

        public ISudoku GetGame(FileInfo fileInfo)
        {
            return _Strategy.CreateBasedOnFile(fileInfo);
        }
    }
}
