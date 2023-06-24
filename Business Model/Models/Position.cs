namespace Business_Model.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position()
        {
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Position p = (Position)obj;
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return X * 17 + Y;
        }
    }


}

