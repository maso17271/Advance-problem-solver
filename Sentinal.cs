namespace ADVANCE
{
    internal class Sentinel : Piece
    {
        public Sentinel(Player player, Square initialSquare) : base(player, initialSquare)
        {
        }

        public override bool CanAttack(Square targetSquare)
        {
            if (targetSquare == null)
                return false;
            if (targetSquare?.Occupant is NeutralPiece neutralPiece) { return false; }
            int dy = targetSquare.Row - Square.Row;
            int dx = targetSquare.Col - Square.Col;

            if (dx < 0) dx = -dx;
            if (dy < 0) dy = -dy;

            return (dx != 0 && dy != 0 && (dx + dy == 3));
        }

        public override bool CanMoveTo(Square newSquare)
        {
            return CanAttack(newSquare);
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
                return Player.Colour == Colour.Black ? 's' : 'S';
            }
        }
    }
}
