namespace ADVANCE
{
    /// <summary>
    /// initialisation of army class
    /// </summary>
    public class Army
    {
        /// <summary>
        /// make enumerable of pieces
        /// </summary>
        public IEnumerable<Piece> Pieces
        {
            get
            {
                return pieces;
            }
        }
        /// <summary>
        /// list of pieces
        /// </summary>
        private List<Piece> pieces = new List<Piece>();
        public Army(Player player, Board board)
        {
            Player = player;
            Board = board;

            int baseRow = player.Colour == Colour.Black ? 0 : Board.Size - 1;
            int direction = player.Direction; 
/*
            for ( int col = 0; col < Board.Size; col++ )
            {
                Square initialSquare = board.Get(baseRow + direction, col);
                Piece newPiece = new Zombie(player, initialSquare);
               
            }*/
        }

        public Player Player { get; }
        public Board Board { get; }
        /// <summary>
        /// removes all pieces from the board
        /// </summary>
        public void RemoveAllPieces()
        {
           foreach( var currentPiece in Pieces )
            {
                if (currentPiece.OnBoard)
                {
                    currentPiece.LeaveBoard();
                }
            }
           pieces.Clear();
        }
        /// <summary>
        /// remove single piece from the board
        /// </summary>
        /// <param name="piece"> piece to be removed</param>
        public void remove( Piece piece )
        {
            pieces.Remove( piece );
        }
        /// <summary>
        /// add a piece to the board
        /// </summary>
        /// <param name="piece">piece to be added</param>
        public void add( Piece piece )
        {
            pieces.Add( piece );
        }
        /// <summary>
        /// recruit pieces to army
        /// </summary>
        /// <param name="icon">icon to represent a piece</param>
        /// <param name="initialSquare">where the piece starts</param>
        /// <exception cref="ArgumentException">square contains nothing</exception>
        internal void Recruit (char icon, Square? initialSquare)
        {
            if (initialSquare == null)
            {
                throw new ArgumentException("Initial square must not be null");
            }
            var symbol = char.ToLower( icon );
            Piece newPiece;

            switch ( symbol )
            {
                case 'z':
                    newPiece = new Zombie(Player, initialSquare);
                    break;

                case 'b':
                    newPiece = new Builder(Player, initialSquare);
                    break;

                case 'j':
                    newPiece = new Jester(Player, initialSquare);
                    break;

                case 'm':
                    newPiece = new Miner(Player, initialSquare);
                    break;

                case 's':
                    newPiece = new Sentinel(Player, initialSquare);
                    break;

                case 'c':
                    newPiece = new Catapult(Player, initialSquare);
                    break;

                case 'd':
                    newPiece = new Dragon(Player, initialSquare);
                    break;

                case 'g':
                    newPiece = new General(Player, initialSquare);
                    break;

                case '#':
                    newPiece = new NeutralPiece(Player, initialSquare);
                    break;

                default:
                    throw new ArgumentException("Unrecognised Icon");
            }

            pieces.Add ( newPiece );
        }
    }
}
