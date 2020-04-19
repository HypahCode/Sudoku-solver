using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class ColRule : ISudokuRule
    {
        public void SolveStep(SudokuGrid grid)
        {
            for (int col = 0; col < grid.Width; col++)
            {
                EditPossibilities(grid, col);
            }
        }

        private void EditPossibilities(SudokuGrid grid, int col)
        {
            var cells = NineCellSolver.GetColumn(grid, col);
            NineCellSolver.Solve(cells);
        }
    }
}
