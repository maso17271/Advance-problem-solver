using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ADVANCE;

namespace ADVANCE
{
    public class Game
    {
        public Player White { get; }
        public Player Black { get; }

        public Player neutral { get; }

        public Board Board { get; }

        public Game()
        {
            Board = new Board();
            White = new Player(Colour.White, this);
            Black = new Player(Colour.Black, this);

        }

        public override string ToString()
        {
            return $"Game:\n{White}\n{Black}\n{Board}";
        }

        public void Write(TextWriter writer)
        {
            for (int row = 0; row < Board.Size; row++)
            {
                for (int col = 0; col < Board.Size; col++)
                {
                    Square currentSquare = Board.Get(row, col);
                    Piece? currentPiece = currentSquare.Occupant;

                    if (currentPiece == null)
                    {
                        writer.Write('.');
                    }
                    else
                    {
                        writer.Write(currentPiece.Icon);
                    }
                }
                writer.WriteLine();
            }

        }

        public void Read(TextReader reader)
        {
            Black.Army.RemoveAllPieces();
            White.Army.RemoveAllPieces();

            for (int row = 0; row < Board.Size; row++)
            {
                string? currentRow = reader.ReadLine();

                if (currentRow == null)
                    throw new Exception("Ran out of data before filling board");
                if (currentRow.Length != Board.Size)
                {
                    throw new Exception("Row ${row} is not the right length");
                }

                for (int col = 0; col < Board.Size; col++)
                {
                    Square? currentSquare = Board.Get(row, col);
                    char icon = currentRow[col];
                    if (icon != '.')
                    {
                  
                        Player currentPlayer = Char.IsUpper(icon) ? White : Black;
                        currentPlayer.Army.Recruit(icon, currentSquare);
                    }
                }
            }
        }
    }
}
