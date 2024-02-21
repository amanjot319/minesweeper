namespace minesweeper
{
    public partial class Form1 : Form
    {
        public class cell
        {
            public Button button;          // Button displays cell onto form
            public short x, y, w, h;       // x, y are position of cell, w, h are width and height for button
            public bool isRevealed, hasFlag, hasBomb;
            public byte numAdjecentBombs;  // Number of bombs in vicinity
        }

        public class board
        {
            public cell[,] cells;
            public byte numBombs = 0;
        }
        
        int difficulty = 1; // 0 is Beginner, 1 is med, 2 is hard, change how difficulty is set later via a button or drop down menu

        private void InitializeBoard(board board)
        {
            int x, y;
            if (difficulty == 0)
            {
                x = 8;   // Beginner is 8x8
                y = 8;
            }
            else if (difficulty == 1)
            {
                x = 16;  // Intermediate is 16x16
                y = 16;
            }
            else
            {
                x = 30;  // Expert is 30x16
                y = 16;
            }

            board.cells = new cell[x, y];  // Create 2d array of cells

            for (short i = 0; i < x; i++)
            {
                for (short j = 0; j < y; j++)
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

            for (int i = 0; i < max; i++)
            {
                Random rnd = new Random();
                int xRnd = 0;
                int yRnd = 0;

                if (difficulty == 0)
                {
                    xRnd = rnd.Next(0, 7);
                    yRnd = rnd.Next(0, 7);
                }
                else if (difficulty == 1)
                {
                    xRnd = rnd.Next(0, 15);
                    yRnd = rnd.Next(0, 15);
                }
                else if (difficulty == 2)
                {
                    xRnd = rnd.Next(0, 29);
                    yRnd = rnd.Next(0, 15);
                }

                if (board.cells[xRnd, yRnd].hasBomb == false)
                {
                    // If cell does not currently contain bomb, add bomb
                    board.cells[xRnd, yRnd].hasBomb = true;
                    board.numBombs++;
                }
                else
                {
                    max++; // If cell did have bomb, increase the max amount of iterations by 1 and repeat for loop
                    continue;
                }
            }

        }

        private void GenerateNeighborNumbers()
        {

        }

        public Form1()
        {
            InitializeComponent();
            board board = new board();
            InitializeBoard(board);
            GenerateBombs(board);
        }

    }
}
