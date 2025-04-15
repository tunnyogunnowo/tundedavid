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
    }
}
