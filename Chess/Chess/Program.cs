using Chess.Pieces;
using System;

namespace Chess
{
    internal class Program
    {
        public void InitializeBoard()
        {
        }
        public void PrintBoard()
        {
            Console.WriteLine("         A   B   C   D   E   F   G   H\n");
            for (int y = 1; y < 9; y++)
            {
                Console.Write("  " + y);
                for (int x = 1; x < 9; x++)
                {
                    //char pion = Piece[x, y].GetSymbol();
                    //.Write(" | " + pion);
                }
                Console.Write(" |\n");

            }
        }
        public static void Main(string[] args)
        {
            Piece[,] Board;
            bool IsWhiteTurn = true;
        }
    }
}
