namespace minesweeper
{
    public partial class Form1 : Form
    {
        public class cell
        {
            public Button button;          // Button displays cell onto form
            public short x, y, w, h;       // x, y are position of cell, w, h are width and height for button
            public bool isRevealed, hasFlag, hasBomb;
            public byte numAdjacentBombs = 0;  // Number of bombs in vicinity
        }

        public class board
        {
            public cell[,] cells;
            public int numBombs = 0;
            public int cols = 0;
            public int rows = 0;
        }
        
        byte difficulty = 2; // 0 is Beginner, 1 is med, 2 is hard, change how difficulty is set later via a button or drop down menu

        private void InitializeBoard(board board)
        {
            if (difficulty == 0)
            {
                board.rows = 8;   // Beginner is 8x8
                board.cols = 8;
            }
            else if (difficulty == 1)
            {
                board.rows = 16;  // Intermediate is 16x16
                board.cols = 16;
            }
            else
            {
                board.rows = 30;  // Expert is 30x16
                board.cols = 16;
            }

            board.cells = new cell[board.rows, board.cols];  // Create 2d array of cells

            for (short i = 0; i < board.rows; i++)
            {
                for (short j = 0; j < board.cols; j++)
                {
                    board.cells[i, j] = new cell() // Create a cell for every cell in 
                    {
                        x = i,
                        y = j,
                        w = 20,
                        h = 20
                    };

                    board.cells[i, j].button = new Button()
                    {
                        Width = board.cells[i, j].w,
                        Height = board.cells[i, j].h,
                        Location = new System.Drawing.Point(i * board.cells[i, j].w, j * board.cells[i, j].h)
                    };
                    int adjBombs = board.cells[i, j].numAdjacentBombs;

                    // add the cell to the button's tag to later use for click behavior
                    board.cells[i, j].button.Tag = board.cells[i, j];
                    board.cells[i, j].button.Click += (sender, e) => btn_click(sender, e);

                    this.Controls.Add(board.cells[i, j].button);
                }
            }
        }

        private void GenerateBombs(board board)
        {
            int max = 0;

            if (difficulty == 0)
            {
                max = 10;   // Beginner has 10 bombs and is 8x8
            }
            else if (difficulty == 1)
            {
                max = 40;   // Intermediate has 40 bombs and is 16x16
            }
            else
            {
                max = 99;   // Expert has 99 bombs and is 30x16
            }

            while (board.numBombs < max)
            {
                Random rnd = new Random();
                int xRnd = 0;
                int yRnd = 0;

                if (difficulty == 0)
                {
                    xRnd = rnd.Next(0, 8);
                    yRnd = rnd.Next(0, 8);
                }
                else if (difficulty == 1)
                {
                    xRnd = rnd.Next(0, 16);
                    yRnd = rnd.Next(0, 16);
                }
                else
                {
                    xRnd = rnd.Next(0, 30);
                    yRnd = rnd.Next(0, 16);
                }

                if (board.cells[xRnd, yRnd].hasBomb == false)
                {
                    // If cell does not currently contain bomb, add bomb
                    board.cells[xRnd, yRnd].hasBomb = true;
                    board.cells[xRnd, yRnd].button.Text = "F";
                    board.numBombs++;
                }
            }

        }

        private void CountAdjacentBombs(board board)
        {
            for (int x = 0; x < board.rows; x++)        // First two for loops iterate through the 2d matrix of cells
            {
                for (int y = 0; y < board.cols; y++)
                {
                    for (int i = -1; i <= 1; i++)       // Second two for loops determine how many cells in vicinity have bombs
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int checkX = i + x; // Creates x and y index values to check for bombs
                            int checkY = j + y;

                            if (checkX > -1 && checkX < board.rows && checkY > -1 && checkY < board.cols)
                            {   // Checks if index values for cells are within bounds
                                if (board.cells[checkX, checkY].hasBomb == true)
                                {
                                    board.cells[x, y].numAdjacentBombs++;   // Adds to numAdjecentBombs if there is a bomb
                                }
                            }
                        }
                    }
                }
            }
        }


        // decides functionality when button is clicked
        private void btn_click(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;
            cell data = (cell)btn.Tag;
            if(data.hasBomb == true)
            {
                btn.Text = "X";
            }
            else
            {
                btn.Text = data.numAdjacentBombs.ToString();
            }
            
        }



        public Form1()
        {
            InitializeComponent();
            board board = new board();
            InitializeBoard(board);
            GenerateBombs(board);
            CountAdjacentBombs(board);
        }

    }
}
