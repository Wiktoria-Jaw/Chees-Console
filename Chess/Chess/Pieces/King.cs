using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class King : Piece
    {
        public King(int row, int col, bool iswhite) : base(row, col, 'k', iswhite, false, null)
        {
            MoveDir = new int[,]
            {
                {-1,1},{0,1},{1,1},
                {-1,0}, {1,0},
                {-1,-1},{0,-1},{1,-1}
            };
        }

        public override List<(int, int)> GetMoves(Piece[,] board)
        {
            List<(int, int)> possMoves = new List<(int, int)>();
            for (int i = 0; i < MoveDir.GetLength(0); i++)
            {
                int newRow = Row + MoveDir[i, 0];
                int newCol = Col + MoveDir[i, 1];
                if (IsInsideBoard(newRow, newCol))
                {
                    if (board[newRow, newCol] == null)
                    {
                        possMoves.Add((newRow, newCol));
                    }
                    else if (board[newRow, newCol].IsWhite != IsWhite)
                    {
                        possMoves.Add((newRow, newCol));
                    }
                }
            }
            if (!HasMoved)
            {
                if (board[Row, Col + 1] == null && board[Row, Col + 2] == null && board[Row, Col + 3] is Rook rook1 && !rook1.HasMoved)
                {
                    possMoves.Add((Row, Col + 2));
                }

                if (board[Row, Col - 1] == null && board[Row, Col - 2] == null && board[Row, Col - 3] == null && board[Row, Col - 4] is Rook rook2 && !rook2.HasMoved)
                {
                    possMoves.Add((Row, Col - 2));
                }
            }
            return possMoves;
        }
    }
}
