using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class Cell3x3Rule : ISudokuRule
    {
        public void SolveStep(SudokuGrid grid)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    EditPossibilities(grid, x, y);
                }
            }
        }

        private void EditPossibilities(SudokuGrid grid, int xCellGroup, int yCellGroup)
        {
            var cells = NineCellSolver.Get3x3SubGrid(grid, xCellGroup, yCellGroup);
            NineCellSolver.Solve(cells);
        }
    }
}
