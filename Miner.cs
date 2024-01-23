namespace ADVANCE
{
    internal class Miner : Piece
    {
        public Miner(Player player, Square initialSquare) : base(player, initialSquare)
        {
        }

        public override bool CanAttack(Square targetSquare)
        {
            if (targetSquare == null)
            {
                return false;
            }

            int dy = targetSquare.Row - Square.Row;
            int dx = targetSquare.Col - Square.Col;
            int adx = dx < 0 ? -dx : dx;
            int ady = dy < 0 ? -dy : dy;

            if ((adx != 0 && ady != 0) || (adx == 0 && ady == 0))
            {
                return false;
            }

            for (int i = 1; i < adx + ady; i++)
            {
                int row = Square.Row + i * dy / (adx + ady);
                int col = Square.Col + i * dx / (adx + ady);
                Square interveningSquare = Square.Board.Get(row, col);

                if(interveningSquare == targetSquare) { return true; }
               
                if (interveningSquare.IsOccupied)
                {
                    return false;
                }

            }

            return true;
        }

        public override bool CanMoveTo(Square newSquare)
        {
            if (newSquare == null)
            {
                return false;
            }

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;
            int adx = dx < 0 ? -dx : dx;
            int ady = dy < 0 ? -dy : dy;

            if ((adx != 0 && ady != 0) || (adx == 0 && ady == 0))
            {
                return false;
            }

            for (int i = 1; i < adx + ady; i++)
            {
                int row = Square.Row + i * dy / (adx + ady);
                int col = Square.Col + i * dx / (adx + ady);
                Square interveningSquare = Square.Board.Get(row, col);

                if (interveningSquare.IsOccupied)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool CanBuild(Square targetSquare)
        {
            return false;
        }

        public override bool CanSwap(Square targetSquare)
        {
            return false;
        }

        public override bool CanConvert(Square targetSquare)
        {
            return false;
        }

        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.Black ? 'm' : 'M';
            }
        }
    }
}
