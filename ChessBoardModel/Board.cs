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
            int xtemp = row;
            int ytemp = col;

            switch (piece.Name)
            {
                case "Pawn":
                    ///////////////////////////////////////////////////////////////////////////////
                    //Black Pawns can move down one or diagonally one space to attack white pieces.
                    ///////////////////////////////////////////////////////////////////////////////
                    if (piece.Color == "Black")
                    {
                        if(row < Size-1)
                        {
                            if(Grid[row+1, col].CurrentlyOccupied == false)
                            {
                                Grid[row + 1, col].LegalNextMove = true;
                            }

                            if(col - 1 >= 0 && Grid[row+1, col-1].CurrentlyOccupied == true && Grid[row+1,col-1].chessPiece.Color == "White")
                            {
                                Grid[row + 1, col - 1].LegalNextMove = true;
                            }
                            
                            if(col + 1 < Size && Grid[row+1,col+1].CurrentlyOccupied == true &&Grid[row + 1, col + 1].chessPiece.Color == "White")
                            {
                                Grid[row + 1, col + 1].LegalNextMove = true;
                            }
                            

                        }                        
                    }
                    /////////////////////////////////////////////////////////////////////////////
                    //White Pawns can move up one or diagonally one space to attack white pieces.
                    /////////////////////////////////////////////////////////////////////////////
                    else
                    {
                        if (row > 0)
                        {
                            if (Grid[row- 1, col].CurrentlyOccupied == false)
                            {
                                Grid[row - 1, col].LegalNextMove = true;
                            }

                            if (col - 1 >= 0 && Grid[row - 1, col - 1].CurrentlyOccupied == true && Grid[row - 1, col - 1].chessPiece.Color == "Black")
                            {
                                Grid[row - 1, col - 1].LegalNextMove = true;
                            }

                            if (col + 1 < Size && Grid[row - 1, col + 1].CurrentlyOccupied == true && Grid[row - 1, col + 1].chessPiece.Color == "Black")
                            {
                                Grid[row - 1, col + 1].LegalNextMove = true;
                            }


                        }
                    }
                    break;

                //////////////////////////////////////////////////////////////
                //Rook can move vertically or horizontally one or more spaces.
                //////////////////////////////////////////////////////////////
                case "Rook":

                    //up
                    xtemp = row - 1;
                    bool exit = false;

                    while(exit == false && xtemp >= 0)
                    {
                        if(Grid[xtemp, col].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if(Grid[xtemp, col].chessPiece.Color != piece.Color)
                            {
                                Grid[xtemp, col].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[xtemp, col].LegalNextMove = true;
                        }
                        xtemp--;
                        
                    }

                    //down
                    xtemp = row + 1;
                    exit = false;

                    while (exit == false && xtemp < Size)
                    {
                        if (Grid[xtemp, col].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[xtemp, col].chessPiece.Color != piece.Color)
                            {
                                Grid[xtemp, col].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[xtemp, col].LegalNextMove = true;
                        }
                        xtemp--;

                    }

                    //left
                    ytemp = col - 1;
                    exit = false;

                    while (exit == false && ytemp >= 0)
                    {
                        if (Grid[row, ytemp].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[row, ytemp].chessPiece.Color != piece.Color)
                            {
                                Grid[row, ytemp].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[row, ytemp].LegalNextMove = true;
                        }
                        ytemp--;

                    }

                    //right
                    ytemp = col + 1;
                    exit = false;

                    while (exit == false && ytemp < Size)
                    {
                        if (Grid[row, ytemp].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[row, ytemp].chessPiece.Color != piece.Color)
                            {
                                Grid[row, ytemp].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[row, ytemp].LegalNextMove = true;
                        }
                        ytemp++;

                    }
                    break;

                ///////////////////////////////////////////////////////////////
                //Bishop can move diagonally in any direction 1 or more spaces.
                ///////////////////////////////////////////////////////////////
                case "Bishop":

                    //upper left
                    xtemp = row - 1;
                    ytemp = col - 1;
                    exit = false;
                    while (xtemp >= 0 && ytemp >= 0 && exit == false)
                    {
                        if(Grid[xtemp, ytemp].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[xtemp, ytemp].chessPiece.Color != piece.Color)
                            {
                                Grid[xtemp, ytemp].LegalNextMove = true;
                            }

                        }
                        else
                        {
                            Grid[xtemp, ytemp].LegalNextMove = true;
                        }
                        
                        xtemp--;
                        ytemp--;
                    }

                    //upper right
                    xtemp = row - 1;
                    ytemp = col + 1;
                    exit = false;
                    while (xtemp >= 0 && ytemp < Size && exit == false)
                    {
                        if(Grid[xtemp,ytemp].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[xtemp, ytemp].chessPiece.Color != piece.Color)
                            {
                                Grid[xtemp, ytemp].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[xtemp, ytemp].LegalNextMove = true;
                        }
                        
                        xtemp--;
                        ytemp++;
                    }

                    //lower left
                    xtemp = row + 1;
                    ytemp = col - 1;
                    exit = false;
                    while (xtemp < Size && ytemp >= 0 && exit == false)
                    {
                        if (Grid[xtemp, ytemp].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[xtemp, ytemp].chessPiece.Color != piece.Color)
                            {
                                Grid[xtemp, ytemp].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[xtemp, ytemp].LegalNextMove = true;
                        }
                        xtemp++;
                        ytemp--;
                    }

                    //lower right
                    xtemp = row + 1;
                    ytemp = col + 1;
                    exit = false;
                    while (xtemp < Size && ytemp < Size && exit == false)
                    {
                        if (Grid[xtemp, ytemp].CurrentlyOccupied == true)
                        {
                            exit = true;
                            if (Grid[xtemp, ytemp].chessPiece.Color != piece.Color)
                            {
                                Grid[xtemp, ytemp].LegalNextMove = true;
                            }
                        }
                        else
                        {
                            Grid[xtemp, ytemp].LegalNextMove = true;
                        }
                        xtemp++;
                        ytemp++;
                    }
                    break;

                //////////////////////////////////////////////////////////////////////////////////////////
                //Knight can move two spaces tur 90 degrees and move one space in any direction. (L-Shape)
                //////////////////////////////////////////////////////////////////////////////////////////
                case "Knight":
                    xtemp = row;
                    ytemp = col;

                    //upper left
                    if (xtemp - 2 >= 0 && ytemp - 1 >= 0)
                    {

                        if(Grid[xtemp - 2, ytemp - 1].CurrentlyOccupied == false || (Grid[xtemp - 2, ytemp -1].CurrentlyOccupied == true && Grid[xtemp - 2, ytemp - 1].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp - 2, ytemp - 1].LegalNextMove = true;
                        }
                        
                    }
                    if (xtemp - 1 >= 0 && ytemp - 2 >= 0)
                    {
                        if (Grid[xtemp - 1, ytemp - 2].CurrentlyOccupied == false || (Grid[xtemp - 1, ytemp - 2].CurrentlyOccupied == true && Grid[xtemp - 1, ytemp - 2].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp - 1, ytemp - 2].LegalNextMove = true;
                        }
                        
                    }


                    //upper right
                    if (xtemp - 2 >= 0 && ytemp + 1 < Size)
                    {
                        if (Grid[xtemp - 2, ytemp + 1].CurrentlyOccupied == false || (Grid[xtemp - 2, ytemp + 1].CurrentlyOccupied == true && Grid[xtemp - 2, ytemp + 1].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp - 2, ytemp + 1].LegalNextMove = true;
                        }
                        
                    }

                    if (xtemp - 1 >= 0 && ytemp + 2 < Size)
                    {
                        if (Grid[xtemp - 1, ytemp + 2].CurrentlyOccupied == false || (Grid[xtemp - 1, ytemp + 2].CurrentlyOccupied == true && Grid[xtemp - 1, ytemp + 2].chessPiece.Color != piece.Color))
                        {
                           Grid[xtemp - 1, ytemp + 2].LegalNextMove = true;
                        }

                        
                    }

                    //lower left
                    if (xtemp +  2 < Size && ytemp - 1 >= 0)
                    {
                        if (Grid[xtemp + 2, ytemp - 1].CurrentlyOccupied == false || (Grid[xtemp + 2, ytemp - 1].CurrentlyOccupied == true && Grid[xtemp + 2, ytemp - 1].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp + 2, ytemp - 1].LegalNextMove = true;
                        }
                        
                    }
                    if (xtemp + 1 < Size && ytemp - 2 >= 0)
                    {
                        if (Grid[xtemp + 1, ytemp - 2].CurrentlyOccupied == false || (Grid[xtemp + 1, ytemp - 2].CurrentlyOccupied == true && Grid[xtemp + 1, ytemp - 2].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp + 1, ytemp - 2].LegalNextMove = true;
                        }
                        
                    }

                    //lower right
                    if (xtemp + 2 < Size && ytemp + 1 < Size)
                    {
                        if (Grid[xtemp + 2, ytemp + 1].CurrentlyOccupied == false || (Grid[xtemp + 2, ytemp + 1].CurrentlyOccupied == true && Grid[xtemp + 2, ytemp + 1].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp + 2, ytemp + 1].LegalNextMove = true;
                        }
                    }
                    if (xtemp + 1 < Size && ytemp + 2 < Size)
                    {
                        if (Grid[xtemp + 1, ytemp + 2].CurrentlyOccupied == false || (Grid[xtemp + 1, ytemp + 2].CurrentlyOccupied == true && Grid[xtemp + 1, ytemp + 2].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp + 1, ytemp + 2].LegalNextMove = true;
                        }
                    }

                    break;

                ////////////////////////////////////////////////////////////////////////////////////////

                ///////////////////////////////////////////////////////////////////////////////////////
                case "Queen":
                    Cell temp = currentCell;
                    temp.chessPiece.Name = "Rook";
                    legalMove(temp);
                    temp.chessPiece.Name = "Bishop";
                    legalMove(temp);

                    break;
                
                ///////////////////////////////////////////////////////////////////////////////////////
                //King can move one space in any direction.////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////
                case "King":
                    xtemp = row;
                    ytemp = col;

                    //up
                    //upper left
                    //upper right
                    if(xtemp - 1 >= 0)
                    {
                        if(Grid[xtemp - 1, ytemp].CurrentlyOccupied == false ||(Grid[xtemp-1,ytemp].CurrentlyOccupied == true && Grid[xtemp-1,ytemp].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp - 1, ytemp].LegalNextMove = true;
                        }
                        
                        if(ytemp - 1 >= 0)
                        {
                            if (Grid[xtemp - 1, ytemp-1].CurrentlyOccupied == false || (Grid[xtemp - 1, ytemp-1].CurrentlyOccupied == true && Grid[xtemp - 1, ytemp-1].chessPiece.Color != piece.Color))
                            {
                                Grid[xtemp - 1, ytemp - 1].LegalNextMove = true;
                            }
                            
                        }
                        if(ytemp + 1 < Size)
                        {
                            if (Grid[xtemp - 1, ytemp + 1].CurrentlyOccupied == false || (Grid[xtemp - 1, ytemp + 1].CurrentlyOccupied == true && Grid[xtemp - 1, ytemp + 1].chessPiece.Color != piece.Color))
                            {
                                Grid[xtemp - 1, ytemp + 1].LegalNextMove = true;
                            }
                            
                        }
                    }

                    //left
                    if(ytemp - 1 >= 0)
                    {
                        if (Grid[xtemp, ytemp-1].CurrentlyOccupied == false || (Grid[xtemp, ytemp-1].CurrentlyOccupied == true && Grid[xtemp, ytemp-1].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp, ytemp - 1].LegalNextMove = true;
                        }
                       
                    }

                    //right
                    if(ytemp + 1 < Size)
                    {
                        if (Grid[xtemp, ytemp + 1].CurrentlyOccupied == false || (Grid[xtemp, ytemp + 1].CurrentlyOccupied == true && Grid[xtemp, ytemp + 1].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp, ytemp + 1].LegalNextMove = true;
                        }
                    }

                    //down
                    //lower left
                    //lower right
                    if(xtemp + 1 < Size)
                    {
                        if (Grid[xtemp + 1, ytemp].CurrentlyOccupied == false || (Grid[xtemp + 1, ytemp].CurrentlyOccupied == true && Grid[xtemp + 1, ytemp].chessPiece.Color != piece.Color))
                        {
                            Grid[xtemp + 1, ytemp].LegalNextMove = true;
                        }
                        if(ytemp - 1 >= 0)
                        {
                            if (Grid[xtemp + 1, ytemp - 1].CurrentlyOccupied == false || (Grid[xtemp + 1, ytemp - 1].CurrentlyOccupied == true && Grid[xtemp + 1, ytemp - 1].chessPiece.Color != piece.Color))
                            {
                                Grid[xtemp + 1, ytemp - 1].LegalNextMove = true;
                            }
                        }
                        if(ytemp + 1 < Size)
                        {
                            if (Grid[xtemp + 1, ytemp + 1].CurrentlyOccupied == false || (Grid[xtemp + 1, ytemp + 1].CurrentlyOccupied == true && Grid[xtemp + 1, ytemp + 1].chessPiece.Color != piece.Color))
                            {
                                Grid[xtemp + 1, ytemp + 1].LegalNextMove = true;
                            }
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
                for(int j = 0; j < Size; j++)
                {
                    if (Grid[i, j].CurrentlyOccupied == true)
                    {
                        string name = Grid[i, j].chessPiece.Name.Substring(0,4);
                        
                        sb.Append("|" + name + "|");
                    }else
                    {
                        sb.Append("|    |");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
