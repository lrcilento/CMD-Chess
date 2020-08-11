using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Classes
{
    class Chess
    {
        public static List<Piece> blackAlive = new List<Piece>();
        public static List<Piece> whiteAlive = new List<Piece>();
        public static List<string> possibleMoves = new List<string>();
        public static bool twoKings = true;
        public static char checkmate = 'n';
        public static string winner = "";
        public static char check = 'n';
        public static void Run()
        {
            Piece [,] board = new Piece[8, 8];
            int turn = 0;
            Piece chosenPiece = null;
            string target = "";
            ChessPrepare(board);
            while (twoKings && checkmate == 'n')
            {
                ChessPrint(board);
                if (turn % 2 == 0)
                {
                    Console.WriteLine("- White Player Turn -");
                    if (check == 'B')
                        Console.WriteLine("- You're in Check! Pay Attention! -");
                    Console.WriteLine("Choose a Piece to Move:");
                    foreach (Piece piece in whiteAlive)
                        Console.Write(piece.shortName + " ");
                    Console.WriteLine();
                    while (chosenPiece == null)
                    {
                        while (possibleMoves.Count == 0)
                        {
                            string chosenPieceName = Console.ReadLine().ToUpper();
                            chosenPiece = FindPiece(board, chosenPieceName);
                            if (chosenPiece == null || chosenPiece.color != 'W')
                            {
                                chosenPiece = null;
                                Console.WriteLine("Please, select a valid piece.");
                            }
                            else
                            {
                                possibleMoves = chosenPiece.GetPossibleMoves(board);
                                if (possibleMoves.Count == 0)
                                {
                                    Console.WriteLine("This piece doesn't have any moves available, please choose another one.");
                                }
                            }
                        }
                    }
                    Console.WriteLine(chosenPiece.shortName + " ("+chosenPiece.coords+") selected. Choose a target coordination:");
                    possibleMoves = chosenPiece.GetPossibleMoves(board);
                    foreach (string move in possibleMoves)
                    Console.Write(move + " ");
                    Console.WriteLine();
                    while (!possibleMoves.Contains(target))
                    {
                        target = Console.ReadLine().ToUpper();
                        if (!possibleMoves.Contains(target))
                            Console.WriteLine("Please, select a valid target coordination.");
                    }
                    possibleMoves.Clear();
                    chosenPiece.Move(target, board);
                    target = "";
                    chosenPiece = null;
                    turn++;
                }
                else
                {
                    Console.WriteLine("- Black Player Turn -");
                    if (check == 'W')
                        Console.WriteLine("- You're in Check! Pay Attention! -");
                    Console.WriteLine("Choose a Piece to Move:");
                    foreach (Piece piece in blackAlive)
                        Console.Write(piece.shortName + " ");
                    Console.WriteLine();
                    while (chosenPiece == null)
                    {
                        while (possibleMoves.Count == 0)
                        {
                            string chosenPieceName = Console.ReadLine().ToUpper();
                            chosenPiece = FindPiece(board, chosenPieceName);
                            if (chosenPiece == null || chosenPiece.color != 'B')
                            {
                                chosenPiece = null;
                                Console.WriteLine("Please, select a valid piece.");
                            }
                            else
                            {
                                possibleMoves = chosenPiece.GetPossibleMoves(board);
                                if (possibleMoves.Count == 0)
                                {
                                    Console.WriteLine("This piece doesn't have any moves available, please choose another one.");
                                }
                            }
                        }
                    }
                    Console.WriteLine(chosenPiece.shortName + " (" + chosenPiece.coords + ") selected. Choose a target coordination:");
                    possibleMoves = chosenPiece.GetPossibleMoves(board);
                    foreach (string move in possibleMoves)
                        Console.Write(move + " ");
                    Console.WriteLine();
                    while (!possibleMoves.Contains(target))
                    {
                        target = Console.ReadLine().ToUpper();
                        if (!possibleMoves.Contains(target))
                            Console.WriteLine("Please, select a valid target coordination.");
                    }
                    possibleMoves.Clear();
                    chosenPiece.Move(target, board);
                    target = "";
                    chosenPiece = null;
                    turn++;
                }
            }
            ChessPrint(board);
            if (checkmate == 'B')
                winner = "Black";
            else if (checkmate == 'W')
                winner = "White";
            if (checkmate != 'n')
                Console.WriteLine("- Checkmate! -");
            else
                Console.WriteLine("- Game Endend -");
            Console.WriteLine("- "+winner+" Wins! -");
        }
        public static void ChessPrepare(Piece[,] matrix)
        {
            matrix[0, 0] = new Piece('W', 'R', 1, 0, 0);
            matrix[1, 0] = new Piece('W', 'H', 1, 1, 0);
            matrix[2, 0] = new Piece('W', 'B', 1, 2, 0);
            matrix[3, 0] = new Piece('W', 'Q', 3, 0);
            matrix[4, 0] = new Piece('W', 'K', 4, 0);
            matrix[5, 0] = new Piece('W', 'B', 2, 5, 0);
            matrix[6, 0] = new Piece('W', 'H', 2, 6, 0);
            matrix[7, 0] = new Piece('W', 'R', 2, 7, 0);

            matrix[0, 1] = new Piece('W', 'P', 1, 0, 1);
            matrix[1, 1] = new Piece('W', 'P', 2, 1, 1);
            matrix[2, 1] = new Piece('W', 'P', 3, 2, 1);
            matrix[3, 1] = new Piece('W', 'P', 4, 3, 1);
            matrix[4, 1] = new Piece('W', 'P', 5, 4, 1);
            matrix[5, 1] = new Piece('W', 'P', 6, 5, 1);
            matrix[6, 1] = new Piece('W', 'P', 7, 6, 1);
            matrix[7, 1] = new Piece('W', 'P', 8, 7, 1);

            matrix[0, 6] = new Piece('B', 'P', 8, 0, 6);
            matrix[1, 6] = new Piece('B', 'P', 7, 1, 6);
            matrix[2, 6] = new Piece('B', 'P', 6, 2, 6);
            matrix[3, 6] = new Piece('B', 'P', 5, 3, 6);
            matrix[4, 6] = new Piece('B', 'P', 4, 4, 6);
            matrix[5, 6] = new Piece('B', 'P', 3, 5, 6);
            matrix[6, 6] = new Piece('B', 'P', 2, 6, 6);
            matrix[7, 6] = new Piece('B', 'P', 1, 7, 6);

            matrix[0, 7] = new Piece('B', 'R', 2, 0, 7);
            matrix[1, 7] = new Piece('B', 'H', 2, 1, 7);
            matrix[2, 7] = new Piece('B', 'B', 2, 2, 7);
            matrix[3, 7] = new Piece('B', 'K', 3, 7);
            matrix[4, 7] = new Piece('B', 'Q', 4, 7);
            matrix[5, 7] = new Piece('B', 'B', 1, 5, 7);
            matrix[6, 7] = new Piece('B', 'H', 1, 6, 7);
            matrix[7, 7] = new Piece('B', 'R', 1, 7, 7);
        }
        public static void ChessPrint(Piece[,] matrix)
        {
            Console.WriteLine("     A   B   C   D   E   F   G   H\n");
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int rowsNumber = 8;
            for (int j = 0; j < rows; j++)
            {
                Console.Write(rowsNumber+"   ");
                for (int i = 0; i < cols; i++)
                {
                    if (matrix[i, cols - (j + 1)] != null)
                    {
                        Console.Write(matrix[i, cols - (j + 1)].shortName + " ");
                    }
                    else
                    {
                        Console.Write("|-| ");
                    }
                }
                Console.Write("   " + rowsNumber);
                rowsNumber--;
                Console.WriteLine();
            }
            Console.WriteLine("\n     A   B   C   D   E   F   G   H");
            Console.WriteLine();
        }
        public static Piece FindPiece(Piece[,] matrix, string pieceName)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[] coords = new int[2];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    if (matrix[i, cols - (j + 1)] != null && matrix[i, cols - (j + 1)].shortName == pieceName)
                    {
                        return matrix[i, cols - (j + 1)];
                    }
                }
            }
            return null;
        }
        public static string CoordsConverter(int xCoord, int yCoord)
        {
            string newCoords = "";
            switch (xCoord)
            {
                case 0:
                    newCoords += "A";
                    break;
                case 1:
                    newCoords += "B";
                    break;
                case 2:
                    newCoords += "C";
                    break;
                case 3:
                    newCoords += "D";
                    break;
                case 4:
                    newCoords += "E";
                    break;
                case 5:
                    newCoords += "F";
                    break;
                case 6:
                    newCoords += "G";
                    break;
                case 7:
                    newCoords += "H";
                    break;
            }
            switch (yCoord)
            {
                case 0:
                    newCoords += "1";
                    break;
                case 1:
                    newCoords += "2";
                    break;
                case 2:
                    newCoords += "3";
                    break;
                case 3:
                    newCoords += "4";
                    break;
                case 4:
                    newCoords += "5";
                    break;
                case 5:
                    newCoords += "6";
                    break;
                case 6:
                    newCoords += "7";
                    break;
                case 7:
                    newCoords += "8";
                    break;
            }
            return newCoords;
        }
        public static int[] CoordsConverter(string coords)
        {
            int[] newCoords = new int[2];
            switch (coords.ElementAt(0))
            {
                case 'A':
                    newCoords[0] = 0;
                    break;
                case 'B':
                    newCoords[0] = 1;
                    break;
                case 'C':
                    newCoords[0] = 2;
                    break;
                case 'D':
                    newCoords[0] = 3;
                    break;
                case 'E':
                    newCoords[0] = 4;
                    break;
                case 'F':
                    newCoords[0] = 5;
                    break;
                case 'G':
                    newCoords[0] = 6;
                    break;
                case 'H':
                    newCoords[0] = 7;
                    break;
            }
            switch (coords.ElementAt(1))
            {
                case '1':
                    newCoords[1] = 0;
                    break;
                case '2':
                    newCoords[1] = 1;
                    break;
                case '3':
                    newCoords[1] = 2;
                    break;
                case '4':
                    newCoords[1] = 3;
                    break;
                case '5':
                    newCoords[1] = 4;
                    break;
                case '6':
                    newCoords[1] = 5;
                    break;
                case '7':
                    newCoords[1] = 6;
                    break;
                case '8':
                    newCoords[1] = 7;
                    break;
            }
            return newCoords;
        }
    }
}
