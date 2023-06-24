using Business_Logic.GameLogic.Reader;
using Business_Model.Interfaces;

namespace Business_Model.Models
{
    public class Leaf : IComponent
    {
        public Position position = new Position();
        public int currentValue = 0;
        public int initialValue = 0;
        public List<int> helperValues = new List<int>();

        public void AcceptLoad(ISudokuLoadVisitor visitor)
        {
            visitor.VisitLoadLeaf(this);
        }

        public void AddHelperValue(int value)
        {
            if (!helperValues.Contains(value))
            {
                helperValues.Add(value);
            }
        }

        public void RemoveHelperValue(int value)
        {
            if (helperValues.Contains(value))
            {
                helperValues.Remove(value);
            }
        }
    }


}

