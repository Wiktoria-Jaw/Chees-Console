using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class Pawn : Piece
    {
        public Pawn(int row, int col, bool iswhite) : base(row, col, 'p', iswhite, false, null)
        {     
        }

        public override List<(int, int)> GetMoves(Piece[,] board)
        {
            List<(int, int)> possMoves = new List<(int, int)>();
            int dir = IsWhite ? 1 : -1;
            int newRow = Row + dir;

            if (newRow >= 0 && newRow < 8)
            {
                if (board[newRow, Col] == null)
                {
                    possMoves.Add((newRow, Col));
                    if (!HasMoved)
                    {
                        int newRow2 = Row + 2 * dir;
                        if (newRow2 >= 0 && newRow2 < 8)
                        {
                            possMoves.Add((newRow2, Col));
                        }
                    }
                }

                if (Col - 1 >= 0 && board[newRow, Col - 1] != null && board[newRow, Col - 1].IsWhite != IsWhite)
                {
                    possMoves.Add((newRow, Col - 1));
                }

                if (Col + 1 < 8 && board[newRow, Col + 1] != null && board[newRow, Col + 1].IsWhite != IsWhite)
                {
                    possMoves.Add((newRow, Col + 1));
                }
            }
            return possMoves;
        }
    }
}


