using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class MoveManager
    {
        public static List<(int, int)> SelectPiece(string pos, Piece[,] board)
        {
            (int row, int col) = BoardManager.ToNumbers(pos);
            return board[row, col].GetMoves(board);
        }
        public static void MovePiece(string pos, int rowNew, int colNew, Piece[,] board, bool isWhiteTurn)
        {
            (int rowCur, int colCur) = BoardManager.ToNumbers(pos);
            board[rowNew, colNew] = board[rowCur, colCur];
            board[rowCur, colCur] = null;
            Piece currPiece = board[rowNew, colNew];

            currPiece.Row = rowNew;
            currPiece.Col = colNew;
            currPiece.HasMoved = true;

            if(currPiece is Pawn)
            {
                HandlePawnMove(currPiece, rowCur, colCur, rowNew, colNew, board, isWhiteTurn);
            }

            if(currPiece is King)
            {
                HandleCastling(colCur, rowNew, colNew, currPiece, board);
            }
        }

        private static void HandlePawnMove(Piece currPiece, int rowCur, int colCur, int rowNew, int colNew, Piece[,] board, bool isWhiteTurn)
        {
            if ((currPiece.IsWhite && rowNew == 0) || (!currPiece.IsWhite && rowNew == 7))
            {
                Console.WriteLine("Pawn promotion! Choose: Q R B N");
                string choice = Console.ReadLine().ToUpper();
                while (!"QRBN".Contains(choice))
                {
                    Console.WriteLine("Invalid choice, choose: Q R B N");
                    choice = Console.ReadLine().ToUpper();
                }
                switch (choice)
                {
                    case "Q":
                        board[rowNew, colNew] = new Queen(rowNew, colNew, currPiece.IsWhite);
                        break;
                    case "R":
                        board[rowNew, colNew] = new Rook(rowNew, colNew, currPiece.IsWhite);
                        break;
                    case "B":
                        board[rowNew, colNew] = new Bishop(rowNew, colNew, currPiece.IsWhite);
                        break;
                    case "N":
                        board[rowNew, colNew] = new Knight(rowNew, colNew, currPiece.IsWhite);
                        break;
                }
            }

            if (Math.Abs(rowNew - rowCur) == 2)
            {
                Program.lastPawnMove = ((rowCur, colCur), (rowNew, colNew));
            }
            else
            {
                Program.lastPawnMove = null;
            }

            if (Program.lastPawnMove.HasValue)
            {
                var ((fromRow, fromCol), (toRow, toCol)) = Program.lastPawnMove.Value;
                if (rowNew == (isWhiteTurn ? toRow - 1 : toRow + 1) && colNew == toCol)
                {
                    board[toRow, toCol] = null;
                }
            }
        }

        private static void HandleCastling(int colCur, int rowNew, int colNew, Piece currPiece, Piece[,] board)
        {
            if (colNew - colCur == 2)
            {
                Piece rook = board[rowNew, 7];
                board[rowNew, 5] = rook;
                board[rowNew, 7] = null;

                rook.Col = 5;
                rook.HasMoved = true;
            }

            if (colNew - colCur == -2)
            {
                Piece rook = board[rowNew, 0];
                board[rowNew, 3] = rook;
                board[rowNew, 0] = null;

                rook.Col = 3;
                rook.HasMoved = true;
            }
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

                if (!GameRules.IsCheck(p.IsWhite, copyBoard))
                {
                    legalMoves.Add(move);
                }
            }
            return legalMoves;
        }
    }
}
