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

            resetBoard();
            
        }


        public void resetBoard()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    Grid[i, j].deloadPiece();
                    if(i < 2)
                    {
                        string color = "White";
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
                        string color = "Black";
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

        public override string ToString()
        {
            return "Hello world";
        }
    }
}
