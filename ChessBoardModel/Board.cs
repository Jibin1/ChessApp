using System;
using System.Text;

namespace ChessBoardModel
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }

        public Board(int size)
        {
            this.Size = size;
            Grid = new Cell[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }

            //setBoard();
            
        }


        public void setBoard()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    Grid[i, j].deloadPiece();
                    if(i < 2)
                    {
                        string color = "Black";
                        if(i == 0)
                        {
                            string name = PieceName(j);
                            Grid[i, j].loadPiece(name, color);
                        }
                        if(i == 1)
                        {
                            string name = "Pawn";
                            Grid[i, j].loadPiece(name, color);
                        }

                    }
                    if(i > 5)
                    {
                        string color = "White";
                        if(i == 6)
                        {
                            string name = "Pawn";
                            Grid[i, j].loadPiece(name, color);
                        }
                        if(i == 7)
                        {
                            string name = PieceName(7 - j);
                            Grid[i, j].loadPiece(name, color);
                        }
                    }

                }
            }
            

        }

        public string PieceName(int num)
        {
            if (num == 0 || num == 7)
            {
                return "Rook";
            } else if (num == 1 || num == 6)
            {
                return "Knight";
            } else if (num == 2 || num == 5)
            {
                return "Bishop";
            } else if (num == 3)
            {
                return "Queen";
            }
            else if (num == 4)
            {
                return "King";
            }
            else
            {
                return "Pawn";
            }
        }

        public void resetBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j].LegalNextMove = false;
                }
            }
        }


        //public void movePiece(int xLoc1, int yLoc1, int xLoc2, int yLoc2)
        //{ 
             
        //    Cell currentCell = Grid[xLoc1, yLoc1];
        //    Cell nextCell = Grid[xLoc2, yLoc2];
        //    if (legalMove(currentCell, nextCell) == true)
        //    {

        //        Grid[xLoc2, yLoc2].loadPiece(currentCell.chessPiece.Name, currentCell.chessPiece.Color);
        //        Grid[xLoc1, yLoc1].deloadPiece();
        //    }
        //    else
        //    {

        //        Console.WriteLine("This is an illegal move");
        //    }



        //}

        public void legalMove(Cell currentCell)
        {
            
            Piece piece = currentCell.chessPiece;
            int row = currentCell.RowNumber;
            int col = currentCell.ColumnNumber;
            switch (piece.Name)
            {
                case "Pawn":
                    if (piece.Color == "Black")
                    {
                        Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].LegalNextMove = true;
                    }
                    else
                    {
                        Grid[currentCell.RowNumber + 1, currentCell.ColumnNumber].LegalNextMove = true;
                    }
                    break;
                case "Rook":
                    for (int i = 0; i < Size; i++)
                    {
                        if (i == col)
                        {

                        }
                        else
                        {
                            Grid[row, i].LegalNextMove = true;
                        }

                        if (i == row)
                        {

                        }
                        else
                        {
                            Grid[i, col].LegalNextMove = true;
                        }

                    }
                    break;
                case "Bishop":
                    //upper left
                    int xtemp = row - 1;
                    int ytemp = col - 1;
                    while (xtemp >= 0 && ytemp >= 0)
                    {
                        Grid[xtemp, ytemp].LegalNextMove = true;
                        xtemp--;
                        ytemp--;
                    }

                    //upper right
                    xtemp = row - 1;
                    ytemp = col + 1;
                    while (xtemp >= 0 && ytemp < Size)
                    {
                        Grid[xtemp, ytemp].LegalNextMove = true;
                        xtemp--;
                        ytemp++;
                    }

                    //lower left
                    xtemp = row + 1;
                    ytemp = col - 1;
                    while (xtemp < Size && ytemp >= 0)
                    {
                        Grid[xtemp, ytemp].LegalNextMove = true;
                        xtemp++;
                        ytemp--;
                    }

                    //lower right
                    xtemp = row + 1;
                    ytemp = col + 1;
                    while (xtemp < Size && ytemp < Size)
                    {
                        Grid[xtemp, ytemp].LegalNextMove = true;
                        xtemp++;
                        ytemp++;
                    }
                    break;
                case "Knight":
                    xtemp = row;
                    ytemp = col;

                    //upper left
                    if (xtemp - 2 >= 0 && ytemp - 1 >= 0)
                    {
                        Grid[xtemp - 2, ytemp - 1].LegalNextMove = true;
                    }
                    if (xtemp - 1 >= 0 && ytemp - 2 >= 0)
                    {
                        Grid[xtemp - 1, ytemp - 2].LegalNextMove = true;
                    }


                    //upper right
                    if (xtemp - 2 >= 0 && ytemp + 1 < Size)
                    {
                        Grid[xtemp - 2, ytemp + 1].LegalNextMove = true;
                    }

                    if (xtemp - 1 >= 0 && ytemp + 2 < Size)
                    {
                        Grid[xtemp - 1, ytemp + 2].LegalNextMove = true;
                    }

                    //lower left
                    if (xtemp +  2 < Size && ytemp - 1 >= 0)
                    {
                        Grid[xtemp + 2, ytemp - 1].LegalNextMove = true;
                    }
                    if (xtemp + 1 < Size && ytemp - 2 >= 0)
                    {
                        Grid[xtemp + 1, ytemp - 2].LegalNextMove = true;
                    }

                    //lower right
                    if (xtemp + 2 < Size && ytemp + 1 < Size)
                    {
                        Grid[xtemp + 2, ytemp + 1].LegalNextMove = true;
                    }
                    if (xtemp + 1 < Size && ytemp + 2 < Size)
                    {
                        Grid[xtemp + 1, ytemp + 2].LegalNextMove = true;
                    }

                    break;
                case "Queen":
                    Cell temp = currentCell;
                    temp.chessPiece.Name = "Rook";
                    legalMove(temp);
                    temp.chessPiece.Name = "Bishop";
                    legalMove(temp);

                    break;
                case "King":
                    xtemp = row;
                    ytemp = col;

                    //up
                    //upper left
                    //upper right
                    if(xtemp - 1 >= 0)
                    {
                        Grid[xtemp - 1, ytemp].LegalNextMove = true;
                        if(ytemp - 1 >= 0)
                        {
                            Grid[xtemp - 1, ytemp - 1].LegalNextMove = true;
                        }
                        if(ytemp + 1 < Size)
                        {
                            Grid[xtemp - 1, ytemp + 1].LegalNextMove = true;
                        }
                    }

                    //left
                    if(ytemp - 1 >= 0)
                    {
                        Grid[xtemp, ytemp - 1].LegalNextMove = true;
                    }

                    //right
                    if(ytemp + 1 < Size)
                    {
                        Grid[xtemp, ytemp + 1].LegalNextMove = true;
                    }

                    //down
                    //lower left
                    //lower right
                    if(xtemp + 1 < Size)
                    {
                        Grid[xtemp + 1, ytemp].LegalNextMove = true;
                        if(ytemp - 1 >= 0)
                        {
                            Grid[xtemp + 1, ytemp - 1].LegalNextMove = true;
                        }
                        if(ytemp + 1 < Size)
                        {
                            Grid[xtemp + 1, ytemp + 1].LegalNextMove = true;
                        }
                    }

                    
                    break;
            }
        }


        public string printLegalMoves()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < Size; i ++)
            {
                for(int j = 0; j < Size; j++)
                {
                   if(Grid[i,j].LegalNextMove == true)
                    {
                        sb.Append("| Legal |");
                    }
                    else
                    {
                        sb.Append("|       |");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < Size; i++)
            {
                sb.Append("| ");
                for(int j = 0; j < Size; j++)
                {
                    if (Grid[i, j].CurrentlyOccupied == true)
                    {
                        sb.Append(Grid[i, j].chessPiece.Name + " | ");
                    }else
                    {
                        sb.Append("- |");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
