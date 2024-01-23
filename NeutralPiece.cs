namespace ADVANCE
{
    public class NeutralPiece : Piece
    {
        private Square? initialSquare;

        public NeutralPiece(Player player, Square? initialSquare) : base (player, initialSquare)
        {
            initialSquare.Occupant = this;
            
        }

        public override bool CanAttack(Square targetSquare)
        {
            return false;
        }

        public override bool CanMoveTo(Square newSquare)
        {
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
                return Player.Colour == Colour.Black ? '#' : '#';
            }
        }
    }
}
