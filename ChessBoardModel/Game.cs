using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Game
    {
        private bool whiteMove { get; }
        private bool blackMove { get; }
        private int moves = 0;
        public Board chessboard;


        public Game()
        {
            chessboard = new Board(8);
            whiteMove = true;
        }

        public void movePiece(int fromRow, int fromCol, int toRow, int toCol)
        {
            if(moves % 2 == 0)
            {
                if(chessboard.Grid[fromRow, fromCol].chessPiece.Color == "White")
                {

                }
                else
                {
                    Console.WriteLine("It is White's Turn");
                }
            }
            else
            {
                if (chessboard.Grid[fromRow, fromCol].chessPiece.Color == "Black")
                {

                }
                else
                {
                    Console.WriteLine("It is Black's Turn");
                }
            }
        }

    }
}
