using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(int row, int col, bool isWhite) : base(row, col, 'b', isWhite, true, null)
        {
            MoveDir = new int[,]
            {
                {1,1},{-1,1},
                {1,-1},{-1,-1}
            };
        }

        public override List<(int, int)> GetMoves(Piece[,] board)
        {
            List<(int, int)> possMoves = new List<(int, int)>();
            for (int i = 0; i < MoveDir.GetLength(0); i++)
            {
                int newRow = Row + MoveDir[i, 0];
                int newCol = Col + MoveDir[i, 1];
                while (IsInsideBoard(newRow, newCol))
                {
                    if (board[newRow, newCol] == null)
                    {
                        possMoves.Add((newRow, newCol));

                    }
                    else
                    {
                        if (board[newRow, newCol].IsWhite != IsWhite)
                        {
                            possMoves.Add((newRow, newCol));
                        }
                        break;
                    }
                    newRow += MoveDir[i, 0];
                    newCol += MoveDir[i, 1];
                }
            }
            return possMoves;
        }
    }
}
