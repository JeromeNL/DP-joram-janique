namespace Business_Model.Interfaces
{
    public interface IComponent
    {
        void AcceptLoad(ISudokuLoadVisitor visitor);
    }


}

