namespace ADVANCE
{
    public abstract class Piece
    {

        public Player Player { get; set; }
        public Square? Square { get; private set; }

        public Game Game { get; private set; }
        public Piece(Player player, Square initialSquare)

        
        {
            Player = player;
            Game = player.Game;
            EnterBoard(initialSquare);
        }

        public override string ToString()
        {
            return $"{Player.Colour} {GetType().Name} at {Square.Row}, {Square.Col}";
        }

        public void EnterBoard(Square square)
        {
            if (OnBoard) throw new Exception($"Piece at {square} is already on the board");
            square.Place(this);
            Square = square;
        }

        public bool OnBoard { 
            get
            {
                return Square != null;
            }
        }

        private char _icon;
        public virtual char Icon
        {
            get
            {
                char firstLetter = GetType().Name[0];

                return Player.Colour == Colour.Black
                    ? Char.ToLower(firstLetter)
                    : Char.ToUpper(firstLetter);

            }
            set
            {
                _icon = value;
            }
        }

        public virtual void LeaveBoard()
        {
            if (Square == null) throw new ArgumentNullException("Piece cannot be removed if it is not on the board");
            Square.Remove();
            Square = null;
        }


        public virtual bool MoveTo(Square newSquare)
         {
            if (!CanMoveTo(newSquare)) return false;
            if (newSquare.IsOccupied) return false;

            LeaveBoard();
            EnterBoard(newSquare);
            return true;
         }

        public abstract bool CanMoveTo(Square newSquare);
     

        public virtual bool Attack(Square targetSquare)
        {
            if (!CanAttack(targetSquare)) return false;
            if (targetSquare.IsFree) return false;
            if (targetSquare == Square) return false;
            

            targetSquare?.Occupant?.LeaveBoard();
            LeaveBoard();
            EnterBoard(targetSquare);
            return true;
        }

      

        public abstract bool CanAttack(Square targetSquare);

        public void SetIcon(char icon)
        {
            Icon = icon;
        }

        internal void Defect()
        {
            if (Square.Occupant != null)
            {
                if (Square.Occupant.Player == this.Player)
                {

                    Square.Occupant.Player = Player.Opponent;
                   
                }
                else 
                {
                    Square.Occupant.Player = this.Player;
                }
            }
           
        }

        public virtual bool BuildWall(Square buildSquare)
        {
            if (buildSquare.IsOccupied) throw new ArgumentException("Wall cannot be built on an occupied square.");

            buildSquare.Occupant = new NeutralPiece(Player, buildSquare);
            return true;
            
        }
        public virtual bool Build(Square buildSquare)
        {

            if (!CanBuild(buildSquare)) return false;
            if (buildSquare.IsOccupied) return false;

            BuildWall(buildSquare);
            return true;

        }



        public abstract bool CanSwap(Square targetSquare);

        public virtual bool Swap(Square jesterSquare, Square swapSquare)
        {
            if (swapSquare.IsFree) throw new ArgumentException("Cannot swap to an empty square.");

            jesterSquare = this.Square;
            Piece swapPiece = swapSquare.Occupant as Piece;
            swapPiece.LeaveBoard();
            LeaveBoard();
            EnterBoard(swapSquare);
            swapPiece.EnterBoard(jesterSquare);
            return true;

           

        }
        public virtual bool SwapPlayer(Square jesterSquare, Square swapSquare)
        {
            if (swapSquare.Occupant is NeutralPiece) return false;
            if (!CanSwap(swapSquare)) return false;
            if (swapSquare.IsFree) return false;
            if (swapSquare.Occupant is Jester) return false;
            
           

            Swap(jesterSquare, swapSquare);
            return true;

        }

        public abstract bool CanConvert(Square targetSquare);

        public virtual bool Convert(Square conversionSquare)
        {
            if (conversionSquare.IsFree) throw new ArgumentException("Cannot swap to an empty square.");

            
            Piece enemyPiece = conversionSquare.Occupant as Piece;
            enemyPiece.Defect();
            return true;



        }
        public virtual bool ConvertEnemy(Square conversionSquare)
        {
            if (conversionSquare.Occupant is NeutralPiece) return false;
        
            if (!CanConvert(conversionSquare)) return false;
            if (conversionSquare.IsFree) return false;

            Convert(conversionSquare);
            return true;

        }

        public abstract bool CanBuild(Square targetSquare);
    }

}
