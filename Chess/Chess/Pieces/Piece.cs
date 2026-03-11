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
