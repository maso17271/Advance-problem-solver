using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADVANCE
{
    /// <summary>
    /// initialisation of board
    /// </summary>
    public class Board
    {
        public const int Size = 9;

        private Square[] squares = new Square[Size *  Size];

        public IEnumerable<Square> Squares
        {
            get
            {
                return squares;
            }
        }
        /// <summary>
        /// states size of board fills with squares
        /// </summary>
        public Board()
        {
            for(int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Square newSquare = new Square(this, row, col);
                    Set(row, col, newSquare);
                }
            }
        }
        /// <summary>
        /// set into a square
        /// </summary>
        /// <param name="row">x axis of the board</param>
        /// <param name="col">y axis of the board</param>
        /// <param name="newSquare">square to be placed</param>
        /// <exception cref="ArgumentException">board to large</exception>
        private void Set(int row, int col, Square newSquare)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) throw new ArgumentException();
            squares[row * Size + col] = newSquare;
        }
        /// <summary>
        /// get squares
        /// </summary>
        /// <param name="row">x axis</param>
        /// <param name="col">y axis</param>
        /// <returns></returns>
        public Square? Get(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size) return null;
            return squares[row * Size + col];
        }
        public override string ToString()
        {
            return $"Board: \n{string.Join<Square>("\n",squares)}";
        }
    }
}
