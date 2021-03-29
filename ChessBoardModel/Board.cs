using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace ChessBoardModel
{
    public class Board
    {
        public int Size { get; set; }
        public Cell[,] Grid { get; set; }
        private static String[] ylabel = { "a", "b", "c", "d", "e", "f", "g", "h" };
        public List<Tuple<int, int>> legalList {get;set;}

        public Board(int size)
        {

            this.Size = size;
            legalList = new List<Tuple<int, int>>();
            
            Grid = new Cell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                    Grid[i, j].CellName = ylabel[j] + (8 - i);
                }
            }

            setBoard();
            
        }


        

        public void setBoard()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    
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
                            string name = PieceName(j);
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
            legalList.Clear();
        }

        public Cell getCellfromName(string cellName)
        {
            int col =  Array.IndexOf(ylabel,cellName.Substring(0, 1).ToLower());
            int row = 8 - Int32.Parse(cellName.Substring(1, 1));

            return Grid[row, col];
        }


        public void movePiece(int fromRow, int fromCol, int toRow, int toCol)
        {

            Cell currentCell = Grid[fromRow, fromCol];
            Cell nextCell = Grid[toRow, toCol];
            resetBoard();
            if (currentCell.CurrentlyOccupied == false)
            {
                Console.WriteLine("Current cell is empty. Pick another cell. \n\n");
            }
            else
            {

                legalMove(currentCell);
                if (nextCell.LegalNextMove == true)
                {
                    Piece piece = currentCell.chessPiece;
                    nextCell.deloadPiece();
                    nextCell.loadPiece(piece.Name, piece.Color);
                    currentCell.deloadPiece();
                }
                else
                {
                    Console.WriteLine("This is an illegal move \n\n");
                }
            }
            
            Console.Write(ToString() + "\n\n");
            
        }


        public void legalMove(Cell currentCell)
        {
            Piece piece = currentCell.chessPiece;
            int row = currentCell.RowNumber;
            int col = currentCell.ColumnNumber;
            
            if(piece.Name == "Pawn")
            {
                ///////////////////////////////////////////////////////////////////////////////
                //Black Pawns can move down one or diagonally one space to attack white pieces.
                ///////////////////////////////////////////////////////////////////////////////
                if (piece.Color == "Black")
                {
                    if(row < Size - 1)
                    {
                        //Black Pawn can only move down one if that empty. 
                        Cell nextCell = Grid[row + 1, col];
                        if (nextCell.CurrentlyOccupied == false)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(row+1, col));
                            
                        }
                        
                        //Black Pawn can move diagonally to lower left if the space exists and is occupied by a White Piece. 
                        if(col - 1 >= 0)
                        {
                            nextCell = Grid[row + 1, col - 1];
                            if(nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color == "White")
                            {
                                nextCell.LegalNextMove = true;
                                legalList.Add(new Tuple<int, int>(row + 1, col-1));
                            }
                        }

                        //Black Pawn can move diagonally to lower right if the space exists and is occupied by a White Piece.
                        if (col + 1 <  Size)
                        {
                            nextCell = Grid[row + 1, col + 1];
                            if(nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color == "White")
                            {
                                nextCell.LegalNextMove = true;
                                legalList.Add(new Tuple<int, int>(row + 1, col+1));
                            }
                        }
                    }
                }

                /////////////////////////////////////////////////////////////////////////////
                //White Pawns can move up one or diagonally one space to attack Black pieces.
                /////////////////////////////////////////////////////////////////////////////
                else
                {
                    if (row > 0)
                    {
                        //White Pawn can only move up one if that empty. 
                        Cell nextCell = Grid[row - 1, col];
                        if (nextCell.CurrentlyOccupied == false)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(row - 1, col));
                        }

                        //White Pawn can move diagonally to upper left if the space exists and is occupied by a Black Piece. 
                        if (col - 1 >= 0)
                        {
                            nextCell = Grid[row - 1, col - 1];
                            if (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color == "Black")
                            {
                                nextCell.LegalNextMove = true;
                                legalList.Add(new Tuple<int, int>(row - 1, col - 1));
                            }
                        }

                        //White Pawn can move diagonally to upper right if the space exists and is occupied by a Black Piece.
                        if (col + 1 < Size)
                        {
                            nextCell = Grid[row - 1, col + 1];
                            if (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color == "Black")
                            {
                                nextCell.LegalNextMove = true;
                                legalList.Add(new Tuple<int, int>(row - 1, col + 1));
                            }
                        }
                    }

                }
                
            }

            //////////////////////////////////////////////////////////////
            //Rook can move vertically or horizontally one or more spaces.
            //////////////////////////////////////////////////////////////
            else if (piece.Name == "Rook")
            {
                int xtemp = row - 1;
                Cell nextCell;
                bool exit = false;

                //Up: Find all the legal moves above the Rook piece
                while (exit == false && xtemp >= 0)
                {
                    nextCell = Grid[xtemp, col];

                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp,col));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, col));
                    }
                    xtemp--;

                }

                //Down: Find all the legal moves below the Rook piece
                xtemp = row + 1;
                exit = false;
                while (exit == false && xtemp < Size)
                {
                    nextCell = Grid[xtemp, col];
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp, col));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, col));
                    }
                    xtemp++;

                }

                //Left: Find all the legal moves left of the Rook piece
                int ytemp = col - 1;
                exit = false;
                while (exit == false && ytemp >= 0)
                {
                    nextCell = Grid[row, ytemp];
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(row,ytemp));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(row, ytemp));
                    }
                    ytemp--;

                }

                //Right: Find all the legal moves right of the Rook piece.
                ytemp = col + 1;
                exit = false;
                while (exit == false && ytemp < Size)
                {
                    nextCell = Grid[row, ytemp]; ;
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(row, ytemp));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(row, ytemp));
                    }
                    ytemp++;

                }



            }

            //////////////////////////////////////////////////////////////////////////////////////////
            //Knight can move two spaces tur 90 degrees and move one space in any direction. (L-Shape)
            //////////////////////////////////////////////////////////////////////////////////////////
            else if (piece.Name == "Knight")
            {
                int xtemp = row;
                int ytemp = col;
                Cell nextCell;

                //Upper Left: find all the legal moves upper left of the Knight piece.
                if (xtemp - 2 >= 0 && ytemp - 1 >= 0)
                {
                    nextCell = Grid[xtemp - 2, ytemp - 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 2, ytemp - 1));
                    }

                }
                if (xtemp - 1 >= 0 && ytemp - 2 >= 0)
                {
                    nextCell = Grid[xtemp - 1, ytemp - 2];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 1, ytemp - 2));
                    }

                }

                //Upper Right: Find all the legal move upper right of the Knight piece. 
                if (xtemp - 2 >= 0 && ytemp + 1 < Size)
                {
                    nextCell = Grid[xtemp - 2, ytemp + 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 2, ytemp + 1));
                    }

                }

                if (xtemp - 1 >= 0 && ytemp + 2 < Size)
                {
                    nextCell = Grid[xtemp - 1, ytemp + 2];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 1, ytemp + 2));
                    }

                }

                //Lower Left: Find all the legal moves lower left of the Knight piece.
                if (xtemp + 2 < Size && ytemp - 1 >= 0)
                {
                    nextCell = Grid[xtemp + 2, ytemp - 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp + 2, ytemp - 1));
                    }

                }
                if (xtemp + 1 < Size && ytemp - 2 >= 0)
                {
                    nextCell = Grid[xtemp + 1, ytemp - 2];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp + 1, ytemp - 2));
                    }

                }

                //Lower Right: Find all the legal moves lower right of the Knight piece. 
                if (xtemp + 2 < Size && ytemp + 1 < Size)
                {
                    nextCell = Grid[xtemp + 2, ytemp + 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp + 2, ytemp + 1));
                    }
                }
                if (xtemp + 1 < Size && ytemp + 2 < Size)
                {
                    nextCell = Grid[xtemp + 1, ytemp + 2];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp + 1, ytemp + 2));
                    }
                }
            }

            ///////////////////////////////////////////////////////////////
            //Bishop can move diagonally in any direction 1 or more spaces.
            ///////////////////////////////////////////////////////////////
            else if (piece.Name == "Bishop")
            {
                int xtemp = row - 1;
                int ytemp = col - 1;
                Cell nextCell;
                bool exit = false;

                //Upper Left: Find all the legal moves upper left of the Bishop piece.
                while(xtemp >= 0 && ytemp >= 0 && exit == false)
                {
                    nextCell = Grid[xtemp, ytemp];
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                        }

                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                    }

                    xtemp--;
                    ytemp--;
                }

                //Upper Right: Find all the legal moves upper right of the Bishop piece. 
                xtemp = row - 1;
                ytemp = col + 1;
                exit = false;
                while (xtemp >= 0 && ytemp < Size && exit == false)
                {
                    nextCell = Grid[xtemp, ytemp];
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                    }

                    xtemp--;
                    ytemp++;
                }

                //Lower Left: Find all the legal moves lower left of the Bishop piece.
                xtemp = row + 1;
                ytemp = col - 1;
                exit = false;
                while (xtemp < Size && ytemp >= 0 && exit == false)
                {
                    nextCell = Grid[xtemp, ytemp];
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                    }
                    xtemp++;
                    ytemp--;
                }

                //Lower Right: Find all the legal moves lower right of the Bishop piece.
                xtemp = row + 1;
                ytemp = col + 1;
                exit = false;
                while (xtemp < Size && ytemp < Size && exit == false)
                {
                    nextCell = Grid[xtemp, ytemp];
                    if (nextCell.CurrentlyOccupied == true)
                    {
                        exit = true;
                        if (nextCell.chessPiece.Color != piece.Color)
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                        }
                    }
                    else
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, ytemp));
                    }
                    xtemp++;
                    ytemp++;
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////
            //Queen can move vertically, horizontally or diagonally one or more spaces in any direction.
            ////////////////////////////////////////////////////////////////////////////////////////////
            else if(piece.Name == "Queen")
            {
                Cell temp = currentCell;
                temp.chessPiece.Name = "Rook";
                legalMove(temp);
                temp.chessPiece.Name = "Bishop";
                legalMove(temp);
            }

            ///////////////////////////////////////////////////////////////////////////////////////
            //King can move one space in any direction.////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////
            else if (piece.Name == "King")
            {
                int xtemp = row;
                int ytemp = col;
                Cell nextCell;

                //Up
                //Upper Left
                //Upper Right
                if(xtemp - 1 >= 0)
                {
                    nextCell = Grid[xtemp - 1, ytemp];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 1, ytemp));
                    }
                }

                if (ytemp - 1 >= 0)
                {
                    nextCell = Grid[xtemp - 1, ytemp - 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 1, ytemp - 1));
                    }

                }
                if (ytemp + 1 < Size)
                {
                    nextCell = Grid[xtemp - 1, ytemp + 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp - 1, ytemp + 1));
                    }

                }

                //Left
                if (ytemp - 1 >= 0)
                {
                    nextCell = Grid[xtemp, ytemp - 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, ytemp - 1));
                    }

                }


                //Right
                if (ytemp + 1 < Size)
                {
                    nextCell = Grid[xtemp, ytemp + 1];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp, ytemp + 1));
                    }

                }

                //Down
                //Lower Left
                //Lower Right
                if (xtemp + 1 < Size)
                {
                    nextCell = Grid[xtemp + 1, ytemp];
                    if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                    {
                        nextCell.LegalNextMove = true;
                        legalList.Add(new Tuple<int, int>(xtemp + 1, ytemp));
                    }

                    if (ytemp - 1 >= 0)
                    {
                        nextCell = Grid[xtemp + 1, ytemp - 1];
                        if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp + 1, ytemp - 1));
                        }
                    }

                    if (ytemp + 1 < Size)
                    {
                        nextCell = Grid[xtemp + 1, ytemp + 1];
                        if (nextCell.CurrentlyOccupied == false || (nextCell.CurrentlyOccupied == true && nextCell.chessPiece.Color != piece.Color))
                        {
                            nextCell.LegalNextMove = true;
                            legalList.Add(new Tuple<int, int>(xtemp + 1, ytemp + 1));
                        }
                    }
                }
            }
            else
            {
                //Cell is currently unoccupied
            }

        }

        public List<Tuple<int,int>> getlegalMoveList(Cell currentCell)
        {
            legalList.Clear();
            legalMove(currentCell);
            return legalList;
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
            sb.Append("|    |");
            foreach(string s in ylabel)
            {
                sb.Append("|  " + s + " |");
            }
            sb.Append("\n");

            for(int i = 0; i < Size; i++)
            {
                sb.Append("|  " + (8-i) + " |");
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
                sb.Append("|  " + (8 - i) + " |");
                sb.Append("\n");
            }
            sb.Append("|    |");
            foreach (string s in ylabel)
            {
                sb.Append("|  " + s + " |");
            }
            return sb.ToString();
        }
    }
}
