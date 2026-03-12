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

                int row = 8 - (pos[1] - '0');
                int col = char.ToUpper(pos[0]) - 'A';
                Piece currPiece = Board[row, col];
                List<(int, int)> filteredMoves = MoveManager.GetLegalMoves(Board, currPiece, MoveManager.SelectPiece(pos, Board));
                Console.WriteLine("Possible moves: ");
                foreach (var move in filteredMoves)
                {
                    Console.Write(BoardManager.ToChessNotation(move.Item1, move.Item2) + " ");
                }

                Console.WriteLine("Select your move from possible moves: ");
                string chosenMove = Console.ReadLine();
                while (chosenMove.Length != 2)
                {
                    Console.WriteLine("Invalid choice, try again. You need to type exacly in a way its displayed (ex. A2)");
                    chosenMove = Console.ReadLine();
                }

                int choseRow = 8 - (chosenMove[1] - '0');
                int choseCol = char.ToUpper(chosenMove[0]) - 'A';
                while (!filteredMoves.Contains((choseRow, choseCol)))
                {
                    Console.WriteLine("Invalid choice, try again. You need to type exacly in a way its displayed (ex. A2)");
                    chosenMove = Console.ReadLine();

                    choseRow = 8 - (chosenMove[1] - '0');
                    choseCol = char.ToUpper(chosenMove[0]) - 'A';
                }
                MoveManager.MovePiece(pos, choseRow, choseCol, Board);

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
