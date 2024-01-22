namespace ADVANCE
{
    internal class Dragon : Piece
    {
        public Dragon(Player player, Square initialSquare) : base(player, initialSquare)
        {
        }

        public override bool CanAttack(Square targetSquare)
        {
            if (targetSquare == null)
            {
                return false;
            }

            if (targetSquare?.Occupant is NeutralPiece neutralPiece) { return false; }
            int rowDiff = targetSquare.Row - Square.Row;
            int colDiff = targetSquare.Col - Square.Col;

            if ((-1 <= rowDiff && rowDiff <= 1) && (-1 <= colDiff && colDiff <= 1)) return false;

            if (CanMoveTo(targetSquare)) { return true; }

            return false;

        }
    
    

        public override bool CanMoveTo(Square newSquare)
        {
            if (newSquare == null)
                return false;

            int dy = newSquare.Row - Square.Row;
            int dx = newSquare.Col - Square.Col;
            int adx = dx < 0 ? -dx : dx;
            int ady = dy < 0 ? -dy : dy;

            if (adx == 0 || ady == 0 || adx != ady)
            {
                for (int i = 1; i < adx + ady; i++)
                {
                    int row = Square.Row + i * dy / (adx + ady);
                    int col = Square.Col + i * dx / (adx + ady);
                    Square interveningSquare = Square.Board.Get(row, col);

                    if (interveningSquare == newSquare)
                    {
                        return true;
                    }

                    else if (interveningSquare.IsOccupied)
                    {
                        return false;
                    }
                }

                    return true;
            }

            if ((adx != 0 && ady != 0) || (adx == 0 && ady == 0))
            {
                for (int i = 1; i < adx; i++)
                {
                    int row = Square.Row + i * dy / ady;
                    int col = Square.Col + i * dx / adx;
                    Square interveningSquare = Square.Board.Get(row, col);

                    if (interveningSquare.IsOccupied) return false;
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
                return Player.Colour == Colour.Black ? 'd' : 'D';
            }
        }
    }
}
