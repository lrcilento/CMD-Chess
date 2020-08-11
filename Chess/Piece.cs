using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp.Classes
{
    public class Piece
    {
        public char color;
        public char type;
        public char enemy;
        public string shortName;
        public int number;
        public int[] position = new int[2];
        public string coords;

        public Piece()
        {
        }
        public Piece(char color, char type, int number, int xPos, int yPos)
        {
            this.color = color;
            if (color == 'W')
                enemy = 'B';
            else
                enemy = 'W';
            this.type = type;
            this.number = number;
            position[0] = xPos;
            position[1] = yPos;
            coords = Chess.CoordsConverter(xPos, yPos);
            shortName = color.ToString() + type.ToString() + number.ToString();
            if (color == 'B')
                Chess.blackAlive.Add(this);
            else
                Chess.whiteAlive.Add(this);

        }
        public Piece(char color, char type, int xPos, int yPos)
        {
            this.color = color;
            if (color == 'W')
                enemy = 'B';
            else
                enemy = 'W';
            this.type = type;
            position[0] = xPos;
            position[1] = yPos;
            coords = Chess.CoordsConverter(xPos, yPos);
            shortName = color.ToString() + type.ToString() + "*";
            if (color == 'B')
                Chess.blackAlive.Add(this);
            else
                Chess.whiteAlive.Add(this);
        }
        public List<string> GetPossibleMoves(Piece[,] board)
        {
            List<string> possibleMoves = new List<string>();
            if (type == 'P')
            {
                if (color == 'W')
                {
                    if (position[0] > 0 && position[1] < 7 && board[position[0] - 1, position[1] + 1] != null && board[position[0] - 1, position[1] + 1].color == 'B')
                        possibleMoves.Add(Chess.CoordsConverter(position[0] - 1, position[1] + 1));
                    if (position[0] < 7 && position[1] < 7 && board[position[0] + 1, position[1] + 1] != null && board[position[0] + 1, position[1] + 1].color == 'B')
                        possibleMoves.Add(Chess.CoordsConverter(position[0] + 1, position[1] + 1));
                    if (position[1] < 7 && (board[position[0], position[1] + 1] == null || board[position[0], position[1] + 1].color == 'B'))
                        possibleMoves.Add(Chess.CoordsConverter(position[0], position[1] + 1));
                    if (position[1] < 6 && board[position[0], position[1] + 1] == null && (board[position[0], position[1] + 2] == null || board[position[0], position[1] + 2].color == 'B'))
                        possibleMoves.Add(Chess.CoordsConverter(position[0], position[1] + 2));
                }
                else
                {
                    if (position[0] > 0 && position[1] > 0 && board[position[0] - 1, position[1] - 1] != null && board[position[0] - 1, position[1] - 1].color == 'W')
                        possibleMoves.Add(Chess.CoordsConverter(position[0] - 1, position[1] - 1));
                    if (position[0] < 7 && position[1] > 0 && board[position[0] + 1, position[1] - 1] != null && board[position[0] + 1, position[1] - 1].color == 'W')
                        possibleMoves.Add(Chess.CoordsConverter(position[0] + 1, position[1] - 1));
                    if (position[1] > 0 && (board[position[0], position[1] - 1] == null || board[position[0], position[1] - 1].color == 'W'))
                        possibleMoves.Add(Chess.CoordsConverter(position[0], position[1] - 1));
                    if (position[1] > 1 && board[position[0], position[1] - 1] == null && (board[position[0], position[1] - 2] == null || board[position[0], position[1] - 2].color == 'W'))
                        possibleMoves.Add(Chess.CoordsConverter(position[0], position[1] - 2));
                }
            }
            if (type == 'R' || type == 'Q')
            {
                int auxX = position[0] + 1;
                int auxY = position[1] + 1;
                while (auxY < 8 && board[position[0], auxY] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0], auxY));
                    auxY++;
                }
                if (auxY < 8 && board[position[0], auxY].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0], auxY));
                }
                auxY = position[1] - 1;
                while (auxY >= 0 && board[position[0], auxY] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0], auxY));
                    auxY--;
                }
                if (auxY >= 0 && board[position[0], auxY].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0], auxY));
                }
                while (auxX < 8 && board[auxX, position[1]] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, position[1]));
                    auxX++;
                }
                if (auxX < 8 && board[auxX, position[1]].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, position[1]));
                }
                auxX = position[0] - 1;
                while (auxX >= 0 && board[auxX, position[1]] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, position[1]));
                    auxX--;
                }
                if (auxX >= 0 && board[auxX, position[1]].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, position[1]));
                }
            }
            if (type == 'H')
            {
                if (position[0] < 6 && position[1] < 7 && (board[position[0] + 2, position[1] + 1] == null || board[position[0] + 2, position[1] + 1].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] + 2, position[1] + 1));
                }
                if (position[0] < 7 && position[1] < 6 && (board[position[0] + 1, position[1] + 2] == null || board[position[0] + 1, position[1] + 2].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] + 1, position[1] + 2));
                }
                if (position[0] < 6 && position[1] > 0 && (board[position[0] + 2, position[1] - 1] == null || board[position[0] + 2, position[1] - 1].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] + 2, position[1] - 1));
                }
                if (position[0] < 7 && position[1] > 1 && (board[position[0] + 1, position[1] - 2] == null || board[position[0] + 1, position[1] - 2].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] + 1, position[1] - 2));
                }
                if (position[0] > 2 && position[1] < 7 && (board[position[0] - 2, position[1] + 1] == null || board[position[0] - 2, position[1] + 1].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] - 2, position[1] + 1));
                }
                if (position[0] > 1 && position[1] < 6 && (board[position[0] - 1, position[1] + 2] == null || board[position[0] - 1, position[1] + 2].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] - 1, position[1] + 2));
                }
                if (position[0] > 2 && position[1] > 1 && (board[position[0] - 2, position[1] - 1] == null || board[position[0] - 2, position[1] - 1].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] - 2, position[1] - 1));
                }
                if (position[0] > 1 && position[1] > 2 && (board[position[0] - 1, position[1] - 2] == null || board[position[0] - 1, position[1] - 2].color == enemy))
                {
                    possibleMoves.Add(Chess.CoordsConverter(position[0] - 1, position[1] - 2));
                }
            }
            if (type == 'B' || type == 'Q')
            {
                int auxX = position[0] + 1;
                int auxY = position[1] + 1;
                while (auxX < 8 && auxY < 8 && board[auxX, auxY] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                    auxY++;
                    auxX++;
                }
                if (auxX < 8 && auxY < 8 && board[auxX, auxY].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                }
                auxX = position[0] + 1;
                auxY = position[1] - 1;
                while (auxX < 8 && auxY >= 0 && board[auxX, auxY] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                    auxY--;
                    auxX++;
                }
                if (auxX < 8 && auxY >= 0 && board[auxX, auxY].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                }
                auxX = position[0] - 1;
                auxY = position[1] + 1;
                while (auxY < 8 && auxX >= 0 && board[auxX, auxY] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                    auxX--;
                    auxY++;
                }
                if (auxY < 8 && auxX >= 0 && board[auxX, auxY].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                }
                auxX = position[0] - 1;
                auxY = position[1] - 1;
                while (auxY >= 0 && auxX >= 0 && board[auxX, auxY] == null)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                    auxX--;
                    auxY--;
                }
                if (auxY >= 0 && auxX >= 0 && board[auxX, auxY].color == enemy)
                {
                    possibleMoves.Add(Chess.CoordsConverter(auxX, auxY));
                }
            }
            if (type == 'K')
            {
                if (position[0] < 7 && (board[position[0] + 1, position[1]] == null || board[position[0] + 1, position[1]].color == enemy))
                    possibleMoves.Add(Chess.CoordsConverter(position[0] + 1, position[1]));
                if (position[1] < 7 && (board[position[0], position[1] + 1] == null || board[position[0], position[1] + 1].color == enemy))
                    possibleMoves.Add(Chess.CoordsConverter(position[0], position[1] + 1));
                if (position[0] > 0 && (board[position[0] - 1, position[1]] == null || board[position[0] - 1, position[1]].color == enemy))
                    possibleMoves.Add(Chess.CoordsConverter(position[0] - 1, position[1]));
                if (position[1] > 0 && (board[position[0], position[1] - 1] == null || board[position[0], position[1] - 1].color == enemy))
                    possibleMoves.Add(Chess.CoordsConverter(position[0], position[1] - 1));
            }
            return possibleMoves;
        }
        public bool CheckChecker(Piece[,] board)
        {
            string enemyKing = enemy + "K*";
            if (GetPossibleMoves(board).Contains(Chess.FindPiece(board, enemyKing).coords))
                return true;
            else
                return false;
        }
        public bool CheckmateChecker()
        {
            return false;
        }
        public void Move(string coords, Piece[,] board)
        {
            int[] target = Chess.CoordsConverter(coords);
            if (board[target[0], target[1]] != null)
            {
                if (board[target[0], target[1]].type == 'K')
                {
                    Chess.twoKings = false;
                    if (board[target[0], target[1]].color == 'B')
                        Chess.winner = "White";
                    else
                        Chess.winner = "Black";
                }
                if (board[target[0], target[1]].color == 'B')
                    Chess.blackAlive.Remove(board[target[0], target[1]]);
                else
                    Chess.whiteAlive.Remove(board[target[0], target[1]]);
            }
            board[position[0], position[1]] = null;
            board[target[0], target[1]] = this;
            position[0] = target[0];
            position[1] = target[1];
            this.coords = Chess.CoordsConverter(position[0], position[1]);
            if (Chess.twoKings == true)
            {
                if (CheckChecker(board))
                {
                    if (CheckmateChecker())
                        Chess.checkmate = color;
                    else
                        Chess.check = color;
                }
            }
        }
    }
}