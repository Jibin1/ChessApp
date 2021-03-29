using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ChessBoardModel;

namespace ChessApp
{
    public class ChessButton : Button
    {
        public int row { get; set; }
        public int col { get; set; }
        //public Cell cell { get; set; }
        
        public Color tileColor { get; set; } 

        public ChessButton(int row, int col)
        {
            this.row = row;
            this.col = col;
            //cell = null;
        }
        
        public ChessButton()
        {

        }

        
        
    }
}
