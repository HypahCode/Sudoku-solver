using System.Collections.Generic;

namespace SudokuSolver
{
    public class SudokuGrid
    {
        private List<GridCell> cells = new List<GridCell>();

        public int Width => 9;
        public int Height => 9;

        public SudokuGrid()
        {
            for (int i = 0; i < Width * Height; i++)
            {
                cells.Add(new GridCell());
            }
            FillAllPossibilities();
        }

        public void SetFixed(string[] rows)
        {
            int y = 0;
            foreach (var row in rows)
            {
                int x = 0;
                foreach (var num in row)
                {
                    if (int.TryParse(num.ToString(), out int number))
                    {
                        if (number != 0)
                        {
                            Cell(x, y).SetFixed(number);
                        }
                        x++;
                    }
                }
                y++;
            }
        }

        public GridCell Cell(int x, int y)
        {
            return cells[y * Width + x];
        }

        public void FillAllPossibilities()
        {
            foreach (var cell in cells)
            {
                cell.Possibilities.Fill();
            }
        }

        public bool HasError()
        {
            foreach (var cell in cells)
            {
                if (cell.State == CellState.Error)
                    return true;
            }
            return false;
        }

        public bool IsSolved()
        {
            foreach (var cell in cells)
            {
                if ((cell.State == CellState.Unsolved) || (cell.State == CellState.Error))
                    return false;
            }
            return true;
        }

        public void SetSolvedWhereConvinced()
        {
            foreach (var cell in cells)
            {
                if (cell.State == CellState.Unsolved)
                {
                    if (cell.Possibilities.Numbers.Count == 1)
                    {
                        cell.SetSolved(cell.Possibilities.Numbers[0]);
                    }
                }
            }
        }
    }
}
