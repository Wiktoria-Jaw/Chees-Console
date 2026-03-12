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
            int dir = IsWhite ? -1 : 1;
            int newRow = Row + dir;

            if (newRow >= 0 && newRow < 8)
            {
                if (board[newRow, Col] == null)
                {
                    possMoves.Add((newRow, Col));
                    if (!HasMoved)
                    {
                        int newRow2 = Row + 2 * dir;
                        if (newRow2 >= 0 && newRow2 < 8 && board[newRow2, Col] == null)
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
            if (Program.lastPawnMove != null)
            {  
                var ((fromRow, fromCol),(toRow,toCol)) = Program.lastPawnMove.Value;

                if (board[toRow, toCol] is Pawn lastPawn &&  lastPawn.IsWhite != IsWhite &&
                    Math.Abs(toRow-fromRow)== 2 &&
                    Row == toRow &&
                    Math.Abs(Col-toCol)==1)
                {
                    int captureRow = IsWhite ? Row - 1 : Row + 1;
                    possMoves.Add((captureRow, toCol));
                }
            }
            return possMoves;
        }
    }
}


