using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Rook : Piece
    {
        public Rook(int row, int col,   bool isWhite ):base(row, col, 'r', isWhite, true, null)
        {
            MoveDir = new int[,]
            {
                {1,0},{-1,0},
                {0,1},{0,-1}
            };
        }
           public override List<(int, int)> GetMoves(Piece[,] board)
        {
            return GetSlidingMoves(board, MoveDir);
        }
    }
}
