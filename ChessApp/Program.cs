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
            chessBoard.Grid[3, 3].loadPiece("Queen", "Black");
            //chessBoard.Grid[4, 4].loadPiece("Pawn", "White");
            Cell temp = chessBoard.Grid[3,3];
            chessBoard.legalMove(temp);
            Console.Write(chessBoard.printLegalMoves());
            
        }
    }
}
