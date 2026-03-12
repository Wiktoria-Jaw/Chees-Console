using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class GameRules
    {
        public static bool ValidationSelectPiece(string pos, Piece[,] board, bool isWhiteTurn)
        {
            if (string.IsNullOrEmpty(pos) || pos.Length != 2)
            {
                return false;
            }

            char colChar = char.ToUpper(pos[0]);
            char rowChar = pos[1];

            if (colChar < 'A' || colChar > 'H' || rowChar < '1' || rowChar > '8')
            {
                return false;
            }

            int col = colChar - 'A';
            int row = 8 - (rowChar - '0');

            Piece currPiece = board[row, col];

            if (currPiece == null || currPiece.IsWhite != isWhiteTurn || currPiece.GetMoves(board).Count == 0)
            {
                return false;
            }
            return true;
        }
        public static Piece FindKing(bool isWhiteTurn, Piece[,] board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece p = board[r, c];
                    if (p is King && p.IsWhite == isWhiteTurn)
                    {
                        return p;
                    }
                }
            }
            return null;
        }
        public static bool IsCheck(bool isWhiteTurn, Piece[,] board)
        {
            Piece king = FindKing(isWhiteTurn, board);
            if (king == null) return false;

            int kingRow = king.Row;
            int kingCol = king.Col;
            foreach (var p in board)
            {
                if (p != null && p.IsWhite != isWhiteTurn)
                {
                    if (p.GetMoves(board).Contains((kingRow, kingCol)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool IsCheckmate(bool isWhiteTurn, Piece[,] board)
        {
            if (!IsCheck(isWhiteTurn, board)) return false;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece p = board[r, c];
                    if (p != null && p.IsWhite == isWhiteTurn)
                    {
                        foreach (var move in p.GetMoves(board))
                        {
                            Piece[,] copyBoard = (Piece[,])board.Clone();

                            copyBoard[move.Item1, move.Item2] = copyBoard[r, c];
                            copyBoard[r, c] = null;

                            if (!IsCheck(isWhiteTurn, copyBoard)) return false;
                        }
                    }
                }
            }
            return true;
        }
        public static bool IsStalemate(bool isWhiteTurn, Piece[,] board)
        {
            if (IsCheck(isWhiteTurn, board)) return false;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece p = board[r, c];
                    if (p != null && p.IsWhite == isWhiteTurn)
                    {
                        var legalMoves = MoveManager.GetLegalMoves(board, p, p.GetMoves(board));

                        if (legalMoves.Count > 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
