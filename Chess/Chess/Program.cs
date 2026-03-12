using Chess.Pieces;
using System;

namespace Chess
{
    internal class Program
    {
        public static void InitializeBoard(Piece[,] board)
        {
            board[7, 0] = new Rook(7, 0, true);
            board[7, 1] = new Knight(7,1, true);
            board[7, 2] = new Bishop(7,2, true);
            board[7, 3] = new Queen(7,3, true);
            board[7, 4] = new King(7,4, true);
            board[7, 5] = new Bishop(7,5, true);
            board[7, 6] = new Knight(7,6, true);
            board[7, 7] = new Rook(7,7, true);

            for (int col = 0; col < 8; col++)
            {
                board[6, col] = new Pawn(6, col, true);
            }

            board[0,0] = new Rook(0,0, false);
            board[0,1] = new Knight(0,1, false);
            board[0,2] = new Bishop(0,2, false);
            board[0,3] = new Queen(0,3, false);
            board[0,4] = new King(0,4, false);
            board[0,5] = new Bishop(0,5, false);
            board[0,6] = new Knight(0,6, false);
            board[0,7] = new Rook(0,7, false);

            for (int col = 0;col < 8; col++)
            {
                board[1, col] = new Pawn(1, col, false);
            }
        }
        public static void PrintBoard(Piece[,] board)
        {
            Console.WriteLine("      A   B   C   D   E   F   G   H");
            Console.WriteLine("     -------------------------------");
            for (int row = 0; row <8; row++)
            {
                Console.Write(8-row + "  ");
                for (int col = 0; col < 8; col++)
                {
                    char pion = board[row, col] != null ? board[row, col].GetSymbol() : ' ';
                    Console.Write(" | " + pion);
                }
                Console.Write(" |\n");
                Console.WriteLine("     -------------------------------");
            }
        }
        public static string ToChessNotation(int row, int col)
        {
            int rowNum = 8 - row;
            char colChar = (char)('A' + col);
            return $"{colChar}{rowNum}";

        }
        public static bool ValidationSelectPiece(string pos, Piece[,] board, bool isWhiteTurn)
        {
            if (string.IsNullOrEmpty(pos) || pos.Length != 2) {
                return false;
            }

            char colChar = char.ToUpper(pos[0]);
            char rowChar = pos[1];

            if (colChar <'A' || colChar>'H' || rowChar < '1' || rowChar > '8')
            {
                return false;
            }

            int col = colChar - 'A';
            int row = 8 - (rowChar - '0');

            Piece currPiece = board[row, col];

            if (currPiece == null)
            {
                return false;
            }

            if (currPiece.IsWhite != isWhiteTurn)
            {
                return false;
            }

            if (currPiece.GetMoves(board).Count == 0)
            {
                return false;
            }
            return true;
        }
        public static List<(int, int)> SelectPiece(string pos, Piece[,] board)
        {
            int row = 8 - (pos[1] - '0');
            int col = char.ToUpper(pos[0])-'A';
            Piece currPiece = board[row, col];
            return board[row,col].GetMoves(board);
        }
        public static void MovePiece(string pos, int rowNew, int colNew, Piece[,] board)
        {
            int rowCur = 8 - (pos[1] - '0');
            int colCur = char.ToUpper(pos[0]) - 'A';
            board[rowNew, colNew] = board[rowCur, colCur];
            board[rowCur, colCur] = null;
            Piece currPiece = board[rowNew, colNew];

            currPiece.Row = rowNew;
            currPiece.Col = colNew;
            currPiece.HasMoved = true;
        }
        public static Piece FindKing(bool isWhiteTurn, Piece[,] board)
        {
            Piece king = null;
            for (int r = 0; r < 8; r++)
            {   
                for (int c = 0; c < 8; c++)
                {
                    Piece p = board[r, c];
                    if (p != null && p is King && p.IsWhite == isWhiteTurn)
                    {
                        king = p;
                        return king;
                    }
                }
            }
            return king;
        }
        public static bool IsCheck(bool isWhiteTurn, Piece[,] board)
        {
            Piece king = FindKing(isWhiteTurn, board);
            if (king == null) return false;

            int kingRow = king.Row;
            int kingCol = king.Col;
            foreach(var p in board)
            {
                if (p != null && p.IsWhite != isWhiteTurn)
                {
                    List<(int, int)> possMoves = p.GetMoves(board);
                    if(possMoves.Contains((kingRow, kingCol))){
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool IsCheckmate(bool isWhiteTurn, Piece[,] board)
        {
            if(!IsCheck(isWhiteTurn, board)) return false;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece p = board[r,c];
                    if (p != null && p.IsWhite == isWhiteTurn)
                    {
                        List<(int, int)> possMoves = p.GetMoves(board);
                        foreach (var move in possMoves)
                        {
                            Piece[,] copyBoard = (Piece[,])board.Clone();

                            copyBoard[move.Item1, move.Item2] = copyBoard[r,c];
                            copyBoard[r, c] = null;

                            if (!IsCheck(isWhiteTurn, copyBoard)) return false;
                        }
                    }
                }
            }
            return true;
        }
        
        //public static bool IsStalemate(bool isWhiteTurn, Piece[,] board)
        //{

        //}
        public static void Main(string[] args)
        {
            Piece[,] Board = new Piece[8, 8];
            bool IsWhiteTurn = true;
            bool gameOver = false;
            InitializeBoard(Board);
            
            while (!gameOver)
            {
                PrintBoard(Board);
                Console.WriteLine("Turn: " + (IsWhiteTurn ? "White" : "Black") + "\nChoose your piece (ex. F5):");
                string pos = Console.ReadLine();
                while (!ValidationSelectPiece(pos, Board, IsWhiteTurn))
                {
                    Console.WriteLine("Invalid choice, try again.");
                    pos = Console.ReadLine();
                }

                List<(int, int)> moves = SelectPiece(pos, Board);
                Console.WriteLine("Possible moves: ");
                foreach (var move in moves)
                {
                    Console.Write(ToChessNotation(move.Item1, move.Item2) + " ");
                }

                Console.WriteLine("Select your move from possible moves: ");
                string chosenMove = Console.ReadLine();
                while (chosenMove.Length != 2)
                {
                    Console.WriteLine("Invalid choice, try again. You need to type exacly in a way its displayed (ex. A2)");
                }

                int choseRow = 8 - (chosenMove[1] - '0');
                int choseCol = char.ToUpper(chosenMove[0]) - 'A';
                while (!moves.Contains((choseRow, choseCol)))
                {
                    Console.WriteLine("Invalid choice, try again. You need to type exacly in a way its displayed (ex. A2)");
                    chosenMove = Console.ReadLine();

                    if (chosenMove.Length != 2)
                    {
                        continue;
                    }

                    choseRow = 8 - (chosenMove[1] - '0');
                    choseCol = chosenMove[0] - 'A';
                }
                MovePiece(pos, choseRow, choseCol, Board);

                if (IsCheckmate(IsWhiteTurn, Board))
                {
                    PrintBoard(Board);
                    Console.WriteLine("Checkmate!"+(IsWhiteTurn ? "White" : "Black")+ " wins!");
                    gameOver = true;
                    continue;
                }
                else if (IsCheck(IsWhiteTurn, Board))
                {
                    Console.WriteLine("Check!");
                }
                else if (IsStalemate(IsWhiteTurn, Board))
                {
                    PrintBoard(Board);
                    Console.WriteLine("Stalemate! It's a draw!");
                    gameOver = true;
                    continue;
                }

                IsWhiteTurn = !IsWhiteTurn;
            }
            

        }
    }
}
