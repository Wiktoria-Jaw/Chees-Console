using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class BoardManager
    {
        public static void InitializeBoard(Piece[,] board)
        {
            board[7, 0] = new Rook(7, 0, true);
            board[7, 1] = new Knight(7, 1, true);
            board[7, 2] = new Bishop(7, 2, true);
            board[7, 3] = new Queen(7, 3, true);
            board[7, 4] = new King(7, 4, true);
            board[7, 5] = new Bishop(7, 5, true);
            board[7, 6] = new Knight(7, 6, true);
            board[7, 7] = new Rook(7, 7, true);

            for (int col = 0; col < 8; col++)
            {
                board[6, col] = new Pawn(6, col, true);
            }

            board[0, 0] = new Rook(0, 0, false);
            board[0, 1] = new Knight(0, 1, false);
            board[0, 2] = new Bishop(0, 2, false);
            board[0, 3] = new Queen(0, 3, false);
            board[0, 4] = new King(0, 4, false);
            board[0, 5] = new Bishop(0, 5, false);
            board[0, 6] = new Knight(0, 6, false);
            board[0, 7] = new Rook(0, 7, false);

            for (int col = 0; col < 8; col++)
            {
                board[1, col] = new Pawn(1, col, false);
            }
        }
        public static void PrintBoard(Piece[,] board)
        {
            Console.WriteLine("      A   B   C   D   E   F   G   H");
            Console.WriteLine("     -------------------------------");
            for (int row = 0; row < 8; row++)
            {
                Console.Write(8 - row + "  ");
                for (int col = 0; col < 8; col++)
                {
                    char pion = board[row, col] != null ? board[row, col].GetSymbol() : ' ';
                    Console.Write(" | " + pion);
                }
                Console.Write(" |\n");
                Console.WriteLine("     -------------------------------");
            }
        }
        public static string ToChessNotation(int row, int col)
        {
            int rowNum = 8 - row;
            char colChar = (char)('A' + col);
            return $"{colChar}{rowNum}";

        }
    }
}
