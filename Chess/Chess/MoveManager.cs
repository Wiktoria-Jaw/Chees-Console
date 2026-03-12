using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class MoveManager
    {
        public static List<(int, int)> SelectPiece(string pos, Piece[,] board)
        {
            int row = 8 - (pos[1] - '0');
            int col = char.ToUpper(pos[0]) - 'A';
            Piece currPiece = board[row, col];
            return currPiece.GetMoves(board);
        }
        public static void MovePiece(string pos, int rowNew, int colNew, Piece[,] board)
        {
            int rowCur = 8 - (pos[1] - '0');
            int colCur = char.ToUpper(pos[0]) - 'A';
            board[rowNew, colNew] = board[rowCur, colCur];
            board[rowCur, colCur] = null;
            Piece currPiece = board[rowNew, colNew];

            currPiece.Row = rowNew;
            currPiece.Col = colNew;
            currPiece.HasMoved = true;
        }
        public static List<(int, int)> GetLegalMoves(Piece[,] board, Piece p, List<(int, int)> moves)
        {
            List<(int, int)> legalMoves = new List<(int, int)>();

            foreach (var move in moves)
            {
                int newRow = move.Item1;
                int newCol = move.Item2;

                Piece[,] copyBoard = (Piece[,])board.Clone();

                copyBoard[newRow, newCol] = copyBoard[p.Row, p.Col];
                copyBoard[p.Row, p.Col] = null;

                copyBoard[newRow, newCol].Row = newRow;
                copyBoard[newRow, newCol].Col = newCol;

                if (!IsCheck(p.IsWhite, copyBoard))
                {
                    legalMoves.Add(move);
                }
            }
            return legalMoves;
        }
    }
}
