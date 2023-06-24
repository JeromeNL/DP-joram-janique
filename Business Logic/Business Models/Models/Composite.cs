using Business_Logic.GameLogic.Reader;
using Business_Model.Interfaces;

namespace Business_Model.Models
{
    public class Composite : IComponent
    {
        public List<IComponent> cells;

        public Composite()
        {
            cells = new List<IComponent>();
        }

        public void AcceptLoad(ISudokuLoadVisitor visitor)
        {
            visitor.VisitLoadComposite(this);
        }

        public void AddCell(IComponent cell)
        {
            cells.Add(cell);
        }

        public void RemoveCell(IComponent cell)
        {
            cells.Remove(cell);
        }
    }


}

