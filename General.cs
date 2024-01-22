namespace ADVANCE
{
    public class General : Piece
    {
        public General(Player player, Square initialSquare) : base(player, initialSquare)
        {

            Colour colour = player.Colour;
            
            
        }

        public override bool CanAttack(Square targetSquare)
        {
            if (targetSquare == null) throw new Exception("Cannot attack with piece that is off the board");
            if (targetSquare?.Occupant is NeutralPiece neutralPiece) { return false; }
            int rowDiff = targetSquare.Row - Square.Row;
            int colDiff = targetSquare.Col - Square.Col;

            if (colDiff == 0 && rowDiff == 0) return false;

            if (Math.Abs(colDiff) == 1 && (rowDiff == 0 || Math.Abs(rowDiff) == 1)) return true;
            if (colDiff == 0 && Math.Abs(rowDiff) == 1) return true;
            return false;


        }

        public override bool CanMoveTo(Square newSquare)
        {
            if (Square == null) throw new Exception("Cannot attack with piece that is off the board");

            int rowDiff = newSquare.Row - Square.Row;
            int colDiff = newSquare.Col - Square.Col;

            if (colDiff == 0 && rowDiff == 0) return false;

            if ((-1 <= colDiff && colDiff <= 1) && (-1 <= rowDiff && rowDiff <= 1))
            {
                return true;
            }
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
                return Player.Colour == Colour.Black ? 'g' : 'G';
            }
        }

        
    }

    
