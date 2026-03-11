using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Queen : Piece
    {
        public Queen(int row, int col, bool isWhite) : base(row, col, 'q', isWhite, true, null)
        {
            MoveDir = new int[,]
            {
                {1,1},{1,0},{-1,1},
                {-1,0},{0,1},
                {1,-1},{0,-1},{-1,-1},
            };
        }

        public override List<(int, int)> GetMoves(Piece[,] board)
        {
            return GetSlidingMoves(board, MoveDir);
        }
    }
}
