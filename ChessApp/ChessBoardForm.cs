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

        private ChessButton selectedButton = new ChessButton();
        private List<Tuple<int, int>> legalList;

        public ChessBoardForm()
        {
            InitializeComponent();
            selectedButton = null;
            //nextButton = null;
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
                    //btnGrid[i, j].cell = chessboard.Grid[i, j];
                    btnGrid[i, j].Click += (sender, eventArgs) => { Grid_Button_Click(sender, eventArgs); };

                    //btnGrid[i, j].Click += Grid_Button_Click;

                    if(i < 2 || i >5)
                    {
                        btnGrid[i, j].Text = chessboard.Grid[i, j].chessPiece.Name;
                    }
                    else
                    {
                        btnGrid[i, j].Enabled = false;
                    }
                    btnGrid[i, j].Location = new Point(buttonSize * j, buttonSize * i);
                    panel1.Controls.Add(btnGrid[i,j]);
                }

            }
            setColorTiles();
            panel1.Update();
            
            
        }

        private void setColorTiles()
        {
            string color = "White";
            for(int i = 0; i < chessboard.Size; i++)
            {
                for(int j = 0; j < chessboard.Size; j++)
                {
                    if (j == 0)
                    {

                    }
                    else
                    {
                        if (color == "White")
                        {
                            color = "Brown";
                        }
                        else
                        {
                            color = "White";
                        }
                    }
                    btnGrid[i, j].BackColor = Color.FromName(color);
                    btnGrid[i, j].tileColor = Color.FromName(color);
                }
            }
        }


        private void Grid_Button_Click(object sender, EventArgs e)
        {
            ChessButton cb = sender as ChessButton;
            ChessButton nextButton = btnGrid[cb.row, cb.col];
            

            if (selectedButton == null)
            {
                selectedButton = nextButton;
                legalList = chessboard.getlegalMoveList(chessboard.Grid[selectedButton.row,selectedButton.col]);
                setLegalColorTiles(true);
                Console.WriteLine("Selected Button is: " + selectedButton.row + ", " + selectedButton.col);
                
            }
            else
            {
                if(nextButton == selectedButton)
                {
                    selectedButton = null;
                    setLegalColorTiles(false);
                }
                else
                {
                    if (nextButton.BackColor == Color.FromName("Blue")) 
                    {
                        chessboard.movePiece(selectedButton.row, selectedButton.col, nextButton.row, nextButton.col);
                        nextButton.Text = chessboard.Grid[nextButton.row, nextButton.col].chessPiece.Name;
                        selectedButton.Text = "";
                        setLegalColorTiles(false);
                        nextButton.Enabled = true;
                        selectedButton.Enabled = false;
                        selectedButton = null;
                    }
                    else
                    {
                        Console.WriteLine("Not a Legal Move");
                    }
                }

            }

        }


        //private void Grid_Button_Click(object sender, EventArgs e)
        //{
        //    ChessButton cb = sender as ChessButton;
        //    ChessButton nextbutton = btnGrid[cb.row, cb.col];
        //    //Pick the first cell
        //    if (selectedButton == null)
        //    {
        //        if (nextbutton.cell == null)
        //        {
        //            //do nothing
        //        }
        //        else
        //        {
        //            selectedButton = nextbutton;
        //            chessboard.legalList.Clear();
        //            chessboard.legalMove(selectedButton.cell);
        //            if (chessboard.legalList.Count > 0)
        //            {
        //                setLegalColorTiles(true);
        //            }
        //        }
        //    }
        //    //pick the second cell
        //    else
        //    {
        //        if (nextbutton == selectedButton) 
        //        {
        //            selectedButton = null;
        //            setLegalColorTiles(false);
        //        }
        //        else
        //        {
        //            if (nextbutton.BackColor == Color.FromName("Blue"))
        //            {
        //                //move piece
        //                Console.Write("Legal Move");
        //                movePiece(selectedButton, nextbutton);
        //                selectedButton = null;
        //                setLegalColorTiles(false);
        //            }
        //            else
        //            {
        //                Console.Write("Not Legal Move");
        //            }
        //        }
        //    }

        //    Console.WriteLine(cb.row + ", " + cb.col );
        //}



        public void setLegalColorTiles(bool setBlueColor)
        {
            if (setBlueColor == true)
            {
                foreach (var item in legalList)
                {
                    btnGrid[item.Item1, item.Item2].BackColor = Color.FromName("Blue");
                    btnGrid[item.Item1, item.Item2].Enabled = true;
                }
            }
            else
            {
             foreach(var item in legalList)
                {
                    btnGrid[item.Item1, item.Item2].BackColor = btnGrid[item.Item1, item.Item2].tileColor;
                    btnGrid[item.Item1, item.Item2].Enabled = false;
                }
            }
            
        }

        //public void movePiece(ChessButton fromBtn, ChessButton toBtn)
        //{
        //    chessboard.movePiece(fromBtn.row, fromBtn.col, toBtn.row, toBtn.col);
        //    toBtn.cell.chessPiece = fromBtn.cell.chessPiece;
        //    toBtn.Text = toBtn.cell.chessPiece.Name;
        //    fromBtn.cell.chessPiece = null;
        //    fromBtn.Text = "";
            
        //}

        //private void ChessBoardForm_Load(object sender, EventArgs e)
        //{

        //}
    }
}
