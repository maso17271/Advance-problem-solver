using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVANCE
{
    public class Square
    {
        public Board Board { get; }
        public int Row { get; }
        public int Col {  get; }
        
        public Square(Board board, int row, int col)
        {
            Board = board;
            Row = row;
            Col = col;
        }
        private Piece? occupant;

        public Piece? Occupant
        {
            get { return occupant; }
            internal set
            {
                if (value == null) throw new ArgumentNullException();
                occupant = value;
            }
        }

        public bool IsFree { 
            get {
                return Occupant == null;
            } 
        }

        public bool IsOccupied
        {
            get
            {
                return !IsFree;
            }
        }

        public void Place(Piece piece)
        {
            if (IsOccupied) throw new ArgumentException("Piece cannot be placed in occupied square.");
            
            Occupant = piece;
        }

       
        public void Remove()
        {
            occupant = null;
        }

        public IEnumerable<Square> AdjacentSquares
        {
            get
            {
                List<Square> adjacentSquares = new List<Square>();

                // Horizontal and Vertical Adjacent Squares
                Square? leftSquare = Neighbour(0, -1);
                Square? rightSquare = Neighbour(0, 1);
                Square? topSquare = Neighbour(-1, 0);
                Square? bottomSquare = Neighbour(1, 0);

                if (leftSquare != null) adjacentSquares.Add(leftSquare);
                if (rightSquare != null) adjacentSquares.Add(rightSquare);
                if (topSquare != null) adjacentSquares.Add(topSquare);
                if (bottomSquare != null) adjacentSquares.Add(bottomSquare);

                // Diagonal Adjacent Squares
                Square? topLeftSquare = Neighbour(-1, -1);
                Square? topRightSquare = Neighbour(-1, 1);
                Square? bottomLeftSquare = Neighbour(1, -1);
                Square? bottomRightSquare = Neighbour(1, 1);

                if (topLeftSquare != null) adjacentSquares.Add(topLeftSquare);
                if (topRightSquare != null) adjacentSquares.Add(topRightSquare);
                if (bottomLeftSquare != null) adjacentSquares.Add(bottomLeftSquare);
                if (bottomRightSquare != null) adjacentSquares.Add(bottomRightSquare);

                return adjacentSquares;
            }
        }

        public Square? Neighbour(int rowOffset, int colOffset)
        {
            int newRow = Row + rowOffset;
            int newCol = Col + colOffset;

            return Board.Get(newRow, newCol);
        }

        public Square? Diagonal(int rowOffset, int colOffset)
        {
            int newRow = Row + rowOffset;
            int newCol = Col + colOffset;

            return Board.Get(newRow, newCol);
        }

        public bool isThreatened(Player player)
        {
         
            
            return ThreateningPieces.Any(p=> p.Player == player);
            
        }

        public IEnumerable<Piece> ThreateningPieces
        {
            get
            {
                List<Piece> ThreatPieces = new List<Piece>();
                foreach (Square square in Board.Squares)
                {
                    Piece? p = square.Occupant;
                    if (p != null && p.CanAttack(this))
                    {
                        ThreatPieces.Add(p);
                    }
                }
                return ThreatPieces;
            }
        }

        public override string ToString()
        {
            if (IsFree)
            return $"Empty square at {Row}, {Col}";
            else
            return $"{Occupant}";
        }

        public static implicit operator Square(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
