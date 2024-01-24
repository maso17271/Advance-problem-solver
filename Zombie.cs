using System.ComponentModel;
using System.Drawing;

namespace ADVANCE
{
    /// <summary>
    /// represents a piece "Zombie"
    /// </summary>
    internal class Zombie : Piece // zombie inherits piece class 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Zombie"/> class.
        /// </summary>
        /// <param name="player">The player who owns the Zombie.</param>
        /// <param name="initialSquare">The initial square where the Zombie is placed.</param>
        public Zombie(Player player, Square initialSquare) : base(player, initialSquare) // constructor
        {   
        }

        /// <summary>
        /// Determines whether the zombie is able to attack a target square
        /// </summary>
        /// <param name="targetSquare"> target of the attack</param>
        /// <returns><c>true</c> Returns true if the zombie can attack with no one intervening, returns false if its a wall or bloacked</returns>
        /// <exception cref="Exception"></exception>

        public override bool CanAttack(Square targetSquare)
        {
            if (targetSquare == null) throw new Exception("zombie attack broken");
            int rowDiff = targetSquare.Row - Square.Row;
            int colDiff = targetSquare.Col - Square.Col;

            if (targetSquare?.Occupant is NeutralPiece neutralPiece) { return false; }

            if (CanMoveTo(targetSquare)) return true;

            int expectedRowDiff = Player.Colour == Colour.Black ? 1 : -1;


            if (rowDiff == expectedRowDiff * 2 && (colDiff == 0 || colDiff == -2 || colDiff == 2))
            {
                int dy = targetSquare.Row - Square.Row;
                int dx = targetSquare.Col - Square.Col;
                int adx = dx < 0 ? -dx : dx;
                int ady = dy < 0 ? -dy : dy;

                if (adx == 0 || ady == 0 || adx != ady)
                {
                    for (int i = 1; i < adx + ady; i++)
                    {
                        int row = Square.Row + i * dy / (adx + ady);
                        int col = Square.Col + i * dx / (adx + ady);
                        Square interveningSquare = Square.Board.Get(row, col);

                        if (interveningSquare == targetSquare)
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

            return false;

        }
        /// <summary>
        /// Determines if a Zombie is able to move to a new square
        /// </summary>
        /// <param name="newSquare"> The destination square for the zombie</param>
        /// <returns><c>true</c> if the zombie is able to move to the square, otherwise <c>false</c></returns>
        /// <exception cref="Exception"></exception>
        public override bool CanMoveTo(Square newSquare)
        {
            if (newSquare == null) throw new Exception("zombie move broken");
            int rowDiff = newSquare.Row - Square.Row;
            int colDiff = newSquare.Col - Square.Col;

            int expectedRowDiff = Player.Colour == Colour.Black ? 1 : -1;

            if (rowDiff == expectedRowDiff && Math.Abs(colDiff) == 0) return true;

            if (rowDiff == expectedRowDiff && Math.Abs(colDiff) == 1) return true;

            return false;
        }
        /// <summary>
        /// Shows whether a zombie is able to build or not
        /// </summary>
        /// <param name="targetSquare"> The square that the wall is build upon</param>
        /// <returns><c>false</c> as the zombie cannot build</returns>
        public override bool CanBuild(Square targetSquare)
        {
            return false;
        }
        /// <summary>
        /// Shows whether a Zombie is able to swap
        /// </summary>
        /// <param name="targetSquare"> the square that contains a piece that is able to be swapped with</param>
        /// <returns><c>false</c> as a zombie cannot swap</returns>
        public override bool CanSwap(Square targetSquare)
        {
            return false;
        }
        /// <summary>
        /// Shows whether a zombie is able to convert
        /// </summary>
        /// <param name="targetSquare"> the target piece's location for conversion</param>
        /// <returns><c>false</c> a zombie cannot convert a piece</returns>
        public override bool CanConvert(Square targetSquare)
        {
            return false;
        }
        /// <summary>
        /// The icon that represents a zombie depending on player colour
        /// </summary>
        public override char Icon
        {
            get
            {
                return Player.Colour == Colour.Black ? 'z' : 'Z';
            }
        }
    }
}
