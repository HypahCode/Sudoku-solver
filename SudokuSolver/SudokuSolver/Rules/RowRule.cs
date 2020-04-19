using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class RowRule : ISudokuRule
    {
        public void SolveStep(SudokuGrid grid)
        {
            for (int row = 0; row < grid.Height; row++)
            {
                EditPossibilities(grid, row);
            }
        }

        private void EditPossibilities(SudokuGrid grid, int row)
        {
            var cells = NineCellSolver.GetRow(grid, row);
            NineCellSolver.Solve(cells);
        }
    }
}
