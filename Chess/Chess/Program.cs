using Chess.Pieces;
using System;

namespace Chess
{
    internal class Program
    {
        public static ((int,int),(int,int))? lastPawnMove = null;

        public static void Main(string[] args)
        {
            Piece[,] Board = new Piece[8, 8];
            bool IsWhiteTurn = true;
            bool gameOver = false;
            BoardManager.InitializeBoard(Board);
            
            while (!gameOver)
            {
                BoardManager.PrintBoard(Board);

                Console.WriteLine("Turn: " + (IsWhiteTurn ? "White" : "Black") + "\nChoose your piece (ex. F5):");
                string pos = Console.ReadLine();

                while (!GameRules.ValidationSelectPiece(pos, Board, IsWhiteTurn))
                {
                    Console.WriteLine("Invalid choice, try again.");
                    pos = Console.ReadLine();
                }

                var (row, col) = BoardManager.ToNumbers(pos);
                Piece currPiece = Board[row, col];

                List<(int, int)> filteredMoves = MoveManager.GetLegalMoves(Board, currPiece, MoveManager.SelectPiece(pos, Board));
                Console.WriteLine("Possible moves: ");

                foreach (var move in filteredMoves)
                {
                    Console.Write(BoardManager.ToChessNotation(move.Item1, move.Item2) + " ");
                }

                Console.WriteLine("\nSelect your move from possible moves: ");
                string chosenMove = Console.ReadLine();

                while (chosenMove.Length != 2)
                {
                    Console.WriteLine("Invalid choice, try again. You need to type exacly in a way its displayed (ex. A2)");
                    chosenMove = Console.ReadLine();
                }

                var (choseRow, choseCol) = BoardManager.ToNumbers(chosenMove);

                while (!filteredMoves.Contains((choseRow, choseCol)))
                {
                    Console.WriteLine("Invalid choice, try again. You need to type exacly in a way its displayed (ex. A2)");
                    chosenMove = Console.ReadLine();

                    (choseRow, choseCol) = BoardManager.ToNumbers(chosenMove);
                }

                MoveManager.MovePiece(pos, choseRow, choseCol, Board, IsWhiteTurn);

                IsWhiteTurn = !IsWhiteTurn;

                if (GameRules.IsCheckmate(IsWhiteTurn, Board))
                {
                    BoardManager.PrintBoard(Board);
                    Console.WriteLine("Checkmate!"+(IsWhiteTurn ? "White" : "Black")+ " wins!");
                    gameOver = true;
                    continue;
                }
                else if (GameRules.IsCheck(IsWhiteTurn, Board))
                {
                    Console.WriteLine("Check!");
                }
                else if (GameRules.IsStalemate(IsWhiteTurn, Board))
                {
                    BoardManager.PrintBoard(Board);
                    Console.WriteLine("Stalemate! It's a draw!");
                    gameOver = true;
                    continue;
                }
            }
        }
    }
}
