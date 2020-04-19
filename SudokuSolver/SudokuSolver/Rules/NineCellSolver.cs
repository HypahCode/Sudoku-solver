using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    internal static class NineCellSolver
    {
        public static void Solve(IEnumerable<GridCell> cells)
        {
            var nums = new NumberRange();
            foreach (var cell in cells)
            {
                if ((cell.State == CellState.Solved) || (cell.State == CellState.FixedNumber))
                {
                    if (!nums.Add(cell.Number))
                    {
                        cell.State = CellState.Error;
                    }
                }
            }

            foreach (var cell in cells)
            {
                cell.Possibilities.Remove(nums);
            }
        }

        public static List<GridCell> GetColumn(SudokuGrid grid, int columnIndex)
        {
            var cells = new List<GridCell>();
            for (int y = 0; y < grid.Height; y++)
            {
                cells.Add(grid.Cell(columnIndex, y));
            }
            return cells;
        }

        public static List<GridCell> GetRow(SudokuGrid grid, int row)
        {
            var cells = new List<GridCell>();
            for (int x = 0; x < grid.Width; x++)
            {
                cells.Add(grid.Cell(x, row));
            }
            return cells;
        }

        public static List<GridCell> Get3x3SubGrid(SudokuGrid grid, int xCellGroup, int yCellGroup)
        {
            var cells = new List<GridCell>();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    cells.Add(grid.Cell(x + (xCellGroup * 3), y + (yCellGroup * 3)));
                }
            }
            return cells;
        }
    }
}
