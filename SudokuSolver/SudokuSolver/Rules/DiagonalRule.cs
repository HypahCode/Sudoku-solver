using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class DiagonalRule : ISudokuRule
    {
        public void SolveStep(SudokuGrid grid)
        {
            EditPossibilities(grid);
        }

        private void EditPossibilities(SudokuGrid grid)
        {
            NineCellSolver.Solve(GetAffectedCellsLeftToRight(grid));
            NineCellSolver.Solve(GetAffectedCellsRightToLeft(grid));
        }

        private List<GridCell> GetAffectedCellsLeftToRight(SudokuGrid grid)
        {
            var cells = new List<GridCell>();
            for (int x = 0; x < grid.Width; x++)
            {
                cells.Add(grid.Cell(x, x));
            }
            return cells;
        }
        private List<GridCell> GetAffectedCellsRightToLeft(SudokuGrid grid)
        {
            var cells = new List<GridCell>();
            for (int x = 0; x < grid.Width; x++)
            {
                cells.Add(grid.Cell(x, grid.Width - x));
            }
            return cells;
        }
    }
}
