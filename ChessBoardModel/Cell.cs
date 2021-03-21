using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class Cell
    {

        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public bool CurrentlyOccupied { get; set; }
        public bool LegalNextMove { get; set; }
        public Piece chessPiece { get; set; }


        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
            chessPiece = null;
            CurrentlyOccupied = false;
        }

        public void loadPiece(string Name, string Color)
        {
            chessPiece = new Piece();
            chessPiece.Color = Color;
            chessPiece.Name = Name;
            CurrentlyOccupied = true;
        }

        public void deloadPiece()
        {
            chessPiece = null;
            CurrentlyOccupied = false;
        }
    }
}
