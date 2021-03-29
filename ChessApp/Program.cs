using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessBoardModel;
using System.Windows;
using System.Windows.Forms;

namespace ChessApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //Board chessBoard = new Board(8);
            //chessBoard.movePiece("d2", "d3");
            //chessBoard.movePiece("c1", "e3");
            //List<Tuple<int, int>> temp = chessBoard.legalList;
            //foreach(var s in temp)
            //{
            //    Console.WriteLine(s.Item1 + ", " + s.Item2);
            //}
            //Console.WriteLine(chessBoard);
            //bool exit = false;
            //while(exit == false)
            //{
            //    Console.WriteLine("Enter first cell");
            //    string fromCell = Console.ReadLine();
            //    Console.WriteLine("Enter secnd cell");
            //    string toCell = Console.ReadLine();
            //    if(fromCell == "end" || toCell == "end")
            //    {
            //        exit = true;
            //    }else
            //    {
            //        chessBoard.movePiece(fromCell, toCell);
            //    }
            //}

            //Application.Run(new ChessBoardForm());
            Console.Write(0 % 2);

        }
    }
}