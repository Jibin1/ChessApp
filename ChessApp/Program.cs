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
            Console.WriteLine(chessBoard);
            bool exit = false;
            while(exit == false)
            {
                Console.WriteLine("Enter first cell");
                string fromCell = Console.ReadLine();
                Console.WriteLine("Enter secnd cell");
                string toCell = Console.ReadLine();
                if(fromCell == "end" || toCell == "end")
                {
                    exit = true;
                }else
                {
                    chessBoard.movePiece(fromCell, toCell);
                }
            }
            
           

        }
    }
}