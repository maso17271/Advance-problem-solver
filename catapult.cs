namespace ADVANCE
{
    internal class Catapult : Piece
    {
        public Catapult(Player player, Square initialSquare) : base(player, initialSquare)
        {
        }

        public override bool CanAttack(Square targetSquare)
        {
            if (Square == null) throw new Exception("Cannot attack with piece that is off the board");

            int rowDiff = targetSquare.Row - Square.Row;
            int colDiff = targetSquare.Col - Square.Col;
            if (targetSquare?.Occupant is NeutralPiece neutralPiece) { return false; }

            if (colDiff == 0 && (rowDiff == 3 || rowDiff == -3)) { return true; }

            if (rowDiff == 0 && (colDiff == 3 || colDiff == -3)) { return true; }

            if ((rowDiff == 2 || rowDiff == -2) && (colDiff == 2 || colDiff == -2)) { return true; }

            return false;
        }

        public override bool CanMoveTo(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot attack with piece that is off the board");

            int rowDiff = newSquare.Row - Square.Row;
            int colDiff = newSquare.Col - Square.Col;

            if ((colDiff == -1 || colDiff ==1) && rowDiff == 0) { return true; }

            if ((rowDiff == -1 || rowDiff == 1) && colDiff == 0) { return true; }


            return false;
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
                return Player.Colour == Colour.Black ? 'c' : 'C';
            }
        }
    }
}
