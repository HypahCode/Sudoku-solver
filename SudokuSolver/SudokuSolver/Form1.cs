using SudokuSolver.Rules;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        private int gridSize = 64;

        public SudokuGrid grid = new SudokuGrid();
        public List<GridCell> selectedCells = new List<GridCell>();

        public RuleBasedSolver Solver;

        public Form1()
        {
            InitializeComponent();

            grid.SetFixed(new[] {
                "000 230 060",
                "300 005 001",
                "900 000 700",

                "250 000 080",
                "090 000 006",
                "406 500 002",

                "609 300 000",
                "000 071 000",
                "003 906 840",
            });

            pictureBox1.Image = DrawSudoku(grid);

            Solver = new RuleBasedSolver(new ISudokuRule[] {
                new Cell3x3Rule(),
                new RowRule(),
                new ColRule(),
                new SingleIsolationRule(),
                new IsolationGridRule(),
                new PairIsolationRule()
            });
        }

        private Bitmap DrawSudoku(SudokuGrid grid)
        {
            var bmp = new Bitmap(gridSize * grid.Width+2, gridSize * grid.Height+2);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                for (int x = 0; x < grid.Width; x++)
                {
                    for (int y = 0; y < grid.Height; y++)
                    {
                        DrawCell(g, grid.Cell(x, y), x, y);
                    }
                }

                DrawGridLines(g, grid);
            }
            return bmp;
        }

        private void DrawGridLines(Graphics g, SudokuGrid grid)
        {
            var pen = new Pen(Color.Black, 1);
            for (int x = 0; x < grid.Width + 1; x++)
            {
                pen.Width = (x % 3) == 0 ? 2 : 1;
                g.DrawLine(pen, new Point(x * gridSize, 0), new Point(x * gridSize, grid.Height * gridSize));
            }

            for (int y = 0; y < grid.Height + 1; y++)
            {
                pen.Width = (y % 3) == 0 ? 2 : 1;
                g.DrawLine(pen, new Point(0, y * gridSize), new Point(grid.Width * gridSize, y * gridSize));
            }
        }

        private void DrawCell(Graphics g, GridCell cell, int x, int y)
        {
            int xAbsolute = x * gridSize;
            int yAbsolute = y * gridSize;

            if (selectedCells.Contains(cell))
            {
                g.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(xAbsolute, yAbsolute, gridSize, gridSize));
            }
            else if ((selectedCells.Count == 1) && (selectedCells[0].Number != 0) && (cell.State == CellState.Unsolved) && (!cell.Possibilities.Numbers.Contains(selectedCells[0].Number)))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 153, 145)), new Rectangle(xAbsolute, yAbsolute, gridSize, gridSize));
            }
            else
            {
                switch (cell.State)
                {
                    case CellState.FixedNumber: g.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle(xAbsolute, yAbsolute, gridSize, gridSize)); break;
                    case CellState.Solved: g.FillRectangle(new SolidBrush(Color.LightGreen), new Rectangle(xAbsolute, yAbsolute, gridSize, gridSize)); break;
                    case CellState.Unsolved: break;
                    case CellState.Error: g.FillRectangle(new SolidBrush(Color.DarkRed), new Rectangle(xAbsolute, yAbsolute, gridSize, gridSize)); break;
                }
            }

            if (cell.State == CellState.Unsolved)
            {
                var font = new Font("Courier new", 8, FontStyle.Bold);
                g.DrawString(cell.Possibilities.ToString(), font, new SolidBrush(Color.DarkGray), new PointF(xAbsolute + 1, yAbsolute + (gridSize - font.Height)));
            }

            if (cell.Number != 0)
            {
                var font2 = new Font("Courier new", 32, FontStyle.Bold);
                g.DrawString(cell.Number.ToString(), font2, new SolidBrush(Color.Black), new PointF(xAbsolute + 12, yAbsolute + 12));
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Solver.SolveStep(grid);
            pictureBox1.Image = DrawSudoku(grid);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectSingleCell(e.X / gridSize, e.Y / gridSize);
            }
            else
            {
                SelectMultiCell(e.X / gridSize, e.Y / gridSize);
            }
            pictureBox1.Image = DrawSudoku(grid);
        }

        private void SelectSingleCell(int x, int y)
        {
            selectedCells.Clear();
            if ((x >= 0) && (x < 9) && (y >= 0) && (y < 9))
            {
                selectedCells.Add(grid.Cell(x, y));
            }
        }

        private void SelectMultiCell(int x, int y)
        {
            if ((x >= 0) && (x < 9) && (y >= 0) && (y < 9))
            {
                var cell = grid.Cell(x, y);
                if (selectedCells.Contains(cell))
                {
                    selectedCells.Remove(cell);
                }
                else
                {
                    selectedCells.Add(cell);
                }
            }
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            foreach (var cell in selectedCells)
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad0:
                    case Keys.D0: cell.SetUnsolved(); break;
                    case Keys.NumPad1:
                    case Keys.D1: cell.SetSolved(1); break;
                    case Keys.NumPad2:
                    case Keys.D2: cell.SetSolved(2); break;
                    case Keys.NumPad3:
                    case Keys.D3: cell.SetSolved(3); break;
                    case Keys.NumPad4:
                    case Keys.D4: cell.SetSolved(4); break;
                    case Keys.NumPad5:
                    case Keys.D5: cell.SetSolved(5); break;
                    case Keys.NumPad6:
                    case Keys.D6: cell.SetSolved(6); break;
                    case Keys.NumPad7:
                    case Keys.D7: cell.SetSolved(7); break;
                    case Keys.NumPad8:
                    case Keys.D8: cell.SetSolved(8); break;
                    case Keys.NumPad9:
                    case Keys.D9: cell.SetSolved(9); break;
                    case Keys.F: cell.SetFixed(cell.Number); break;
                }
            }
            pictureBox1.Image = DrawSudoku(grid);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            grid.SetSolvedWhereConvinced();
            pictureBox1.Image = DrawSudoku(grid);
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            grid.Clear();
            pictureBox1.Image = DrawSudoku(grid);
        }
    }
}
