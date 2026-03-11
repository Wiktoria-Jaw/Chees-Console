using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    class Knight : Piece
    {
        public Knight(int row, int col, bool iswhite) : base(row, col, 's', iswhite, false, null)
        {
            MoveDir = new int[,]
            {
                {2,-1},{2,1},
                {1,2},{1,-2},
                {-1,2},{-1,-2},
                {-2,1},{-2,-1}
            };
        }

        public override List<(int, int)> GetMoves(Piece[,] board)
        {
            List<(int, int)> possMoves = new List<(int, int)>();

            for (int i = 0; i < MoveDir.GetLength(0); i++)
            {
                int newRow = Row + MoveDir[i, 0];
                int newCol = Col + MoveDir[i, 1];
                if(IsInsideBoard(newRow, newCol))
                {
                    if (board[newRow,newCol] == null)
                    {
                        possMoves.Add((newRow, newCol));
                    }
                    else if(board[newRow,newCol].IsWhite != IsWhite)
                    {
                        possMoves.Add((newRow, newCol));
                    }
                }
            }
            return possMoves;
        }
    }
}
