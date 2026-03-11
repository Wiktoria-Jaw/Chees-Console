using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    abstract class Piece
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public char Symbol { get; set; }
        public bool HasMoved { get; set; }
        public bool IsWhite { get; set; }
        public bool IsSlidingPiece { get; set; }
        public int[,] MoveDir { get; set; }
        protected Piece(int row, int col, char sym, bool iswhite, bool issliding, int[,] movedir)
        {
            Row = row;
            Col = col;
            Symbol = sym;
            HasMoved = false;
            IsWhite = iswhite;
            IsSlidingPiece = issliding;
            MoveDir = movedir;
        }
        public abstract List<(int, int)> GetMoves(Piece[,] board);
      
        protected List<(int, int)> GetSlidingMoves(Piece[,] board, int[,] moveDirs)
        {
            List<(int, int)> possMoves = new List<(int, int)>();
            for (int i = 0; i < moveDirs.GetLength(0); i++)
            {
                int newRow = Row + moveDirs[i, 0];
                int newCol = Col + moveDirs[i, 1];
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
                    newRow += moveDirs[i, 0];
                    newCol += moveDirs[i, 1];
                }
            }
            return possMoves;
        }

        protected char GetSymbol()
        {
            if (IsWhite)
            {
                return  char.ToUpper(Symbol);
            }
            else
            {
                return char.ToLower(Symbol);
            }
        }

        protected bool IsInsideBoard(int row, int col)
        {
            return row < 8 && row >= 0 && col < 8 && col >= 0;
        }
    }
}
