using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessBoardModel;

namespace ChessApp
{
    public partial class ChessBoardForm : Form
    {

        public static Board chessboard = new Board(8);
        //public Button[,] btnGrid = new Button[chessboard.Size, chessboard.Size];
        public ChessButton[,] btnGrid = new ChessButton[chessboard.Size, chessboard.Size];

        public ChessBoardForm()
        {
            InitializeComponent();

            populateButton();
        }

        public void populateButton()
        {

            int buttonSize = panel1.Width / chessboard.Size;

            panel1.Height = panel1.Width;

            

            for(int i = 0; i < chessboard.Size; i++)
            {
                for(int j = 0; j < chessboard.Size; j++)
                {
                    btnGrid[i, j] = new ChessButton(i, j);
                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;
                    btnGrid[i, j].Click += (sender, eventArgs) => { Grid_Button_Click(sender, eventArgs); };
                    //btnGrid[i, j].Click += Grid_Button_Click;
                    if(i < 2 || i >5)
                    {
                        string pieceName = chessboard.Grid[i, j].chessPiece.Name;
                        btnGrid[i, j].Text = pieceName;
                    }
                    btnGrid[i, j].Location = new Point(buttonSize * j, buttonSize * i);
                    panel1.Controls.Add(btnGrid[i,j]);
                }
            }

            panel1.Update();
            
            
        }

        private void Grid_Button_Click(object sender, EventArgs e)
        {
            ChessButton cb = sender as ChessButton;

            Console.WriteLine(cb.row + ", " + cb.col );
        }



        public void getLegalMoves()
        {

        }

        //private void ChessBoardForm_Load(object sender, EventArgs e)
        //{

        //}
    }
}
