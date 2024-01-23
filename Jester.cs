using System.ComponentModel;

namespace ADVANCE
{
    internal class Jester : Piece
    {
        public Jester(Player player, Square initialSquare) : base(player, initialSquare)
        {
        }

        public override bool CanAttack(Square targetSquare)
        {
            return false;
        }

        public override bool CanMoveTo(Square newSquare)
        {
            if (newSquare == null) throw new Exception("Cannot attack with piece that is off the board");

            int rowDiff = newSquare.Row - Square.Row;
            int colDiff = newSquare.Col - Square.Col;

            if (rowDiff == 0 && colDiff == 0) { return false; }

            if ((-1 <= colDiff && colDiff <= 1) && (-1 <= rowDiff && rowDiff <= 1)) return true;

            return false;
        }



        public override bool CanBuild(Square targetSquare)
        {
            return false;
        }

        public override bool CanSwap(Square targetSquare)
        {
            if (CanMoveTo(targetSquare)) { return true; }

            return false;
        }

        public override bool CanConvert(Square targetSquare)
        {
            if (CanMoveTo(targetSquare)) { return true; }

            return false;
        }

        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.Black ? 'j' : 'J';
            }
        }


    }
}
