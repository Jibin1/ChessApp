using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessApp
{
    public class ChessButton : Button
    {
        public int row { get; set; }
        public int col { get; set; }
        //private Color textColor;
        //private Color backColor; 

        public ChessButton(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        
        
    }
}
