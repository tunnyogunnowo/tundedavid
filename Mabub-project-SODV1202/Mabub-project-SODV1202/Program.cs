using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mabub_project_SODV1202
{
   public class GameBoard 
    {
        private readonly char[,] board;
        private const int Rows = 6;
        private const int Columns = 7;

        public GameBoard()
        {
            board = new char[Rows, Columns];
            InitializeBoard();
        }

        private void InitializeBoard() 
        { 
            for (int row = 0; row < Rows; row++) 
            {
                for (int col = 0; col < Columns; col++)
                {
                    board[row, col] = ' ';
                }
    }
}

        public bool IsValidMove(int col)
        {
            return col >= 0 && col < Columns && board[0, col] == ' ';
        }

        public bool MakeMove(int col, char symbol)
        {
            if (!IsValidMove(col)) return false;

            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, col] == ' ')
                {
                    board[row, col] = symbol;
                    return true;
                }
            }
            return false;
        }

        public bool CheckWin(char symbol)
        {
            //Check the horizontal line
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    if (board[row, col] == symbol &&
                        board[row, col + 1] == symbol &&
                        board[row, col + 2] == symbol &&
                        board[row, col + 3] == symbol)
                    {
                        return true;
                    }
                }
            }

            //Check the vertical line
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    if (board[row, col] == symbol &&
                        board[row, col + 1] == symbol &&
                        board[row, col + 2] == symbol &&
                        board[row, col + 3] == symbol)
                    {
                        return true;
                    }
                }
            }


            //Check diagonal (top-left to bottom-right)
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    if (board[row, col] == symbol &&
                        board[row, col + 1] == symbol &&
                        board[row, col + 2] == symbol &&
                        board[row, col + 3] == symbol)
                    {
                        return true;
                    }
                }
            }

            // Check diagonal (bottom-left to top-right)
   
         for (int row = 3; row < Rows; row++)
            {
                for (int col = 0; col < Columns - 3; col++)
                {
                    if (board[row, col] == symbol &&
                        board[row - 1, col + 1] == symbol &&
                        board[row - 2, col + 2] == symbol &&
                        board[row - 3, col + 3] == symbol)
                    {
                        return true;
                    }
                }
               }

                return false;
        }
        public bool IsBoardFull()
        {
            for (int col = 0; col < Columns; col++)
            {
                if (board[0, col] == ' ')
                {
                    return false;
                }
            }
            return true;
        }

        public void DisplayBoard()
        {
            Console.WriteLine("\n  1   2   3   4   5   6   7");
            Console.WriteLine("+---+---+---+---+---+---+---+");

            for (int row = 0; row < Rows; row++)
            {
                Console.Write("| ");
                for (int col = 0; col < Columns; col++)
                {
                    Console.Write(board[row, col]);
                    Console.Write(" | ");
                }
                Console.WriteLine("\n+---+---+---+---+---+---+---+");
            }
        }
    }
    public class Player
    {
        public string Name { get; }
        public char Symbol { get; }

        public Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public virtual int GetMove(GameBoard board)
        {
            int col;
            do
            {
                Console.Write($"{Name}'s turn ({Symbol}). Enter column (1-7): ");
                if (!int.TryParse(Console.ReadLine(), out col))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                    col = -1;
                    continue;
                }
                col--; // Convert to 0-based index

                if (col < 0 || col >= 7)
                {
                    Console.WriteLine("Column must be between 1 and 7.");
                }
                else if (!board.IsValidMove(col))
                {
                    Console.WriteLine("That column is full. Choose another.");
                }
            } while (col < 0 || col >= 7 || !board.IsValidMove(col));

            return col;
        }
    }

    public class GameController
    {
        private readonly GameBoard board;
        private readonly Player player1;
        private readonly Player player2;
        private Player currentPlayer;

        public GameController(Player p1, Player p2)
        {
            board = new GameBoard();
            player1 = p1;
            player2 = p2;
            currentPlayer = player1;
        }
        public void StartGame()
        {
            Console.WriteLine("Connect Four Game Started!");
            Console.WriteLine($"{player1.Name} is {player1.Symbol}, {player2.Name} is {player2.Symbol}");

            while (true)
            {
                board.DisplayBoard();

                int col = currentPlayer.GetMove(board);
                board.MakeMove(col, currentPlayer.Symbol);

                if (board.CheckWin(currentPlayer.Symbol))
                {
                    board.DisplayBoard();
                    Console.WriteLine($"\n{currentPlayer.Name} ({currentPlayer.Symbol}) wins!");
                    break;
                }

                if (board.IsBoardFull())
                {
                    board.DisplayBoard();
                    Console.WriteLine("\nThe game is a draw!");
                    break;
                }

                // Switch players
                currentPlayer = (currentPlayer == player1) ? player2 : player1;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect Four (2-Player Version)");

            Console.Write("Enter Player 1 name: ");
            string p1Name = Console.ReadLine();

            Console.Write("Enter Player 2 name: ");
            string p2Name = Console.ReadLine();

            var player1 = new Player(p1Name, 'X');
            var player2 = new Player(p2Name, 'O');

            var game = new GameController(player1, player2);
            game.StartGame();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
