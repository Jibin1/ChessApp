using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoardModel;

namespace ChessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board chessBoard = new Board(8);
            Console.Write(chessBoard);
        }
    }
}
