using System;
using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class SingleIsolationRule : ISudokuRule
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
            SetSingleNumsToSolved(cells);
        }

        private static void SetSingleNumsToSolved(List<GridCell> cells)
        {
            var counts = new Dictionary<int, int>();
            foreach (var cell in cells)
            {
                foreach (var num in cell.Possibilities.Numbers)
                {
                    if (counts.ContainsKey(num))
                    {
                        counts[num]++;
                    }
                    else
                    {
                        counts[num] = 1;
                    }
                }
            }

            foreach (var key in counts.Keys)
            {
                if (counts[key] == 1)
                {
                    foreach (var cell in cells)
                    {
                        if (cell.Possibilities.Numbers.Contains(key))
                        {
                            cell.Possibilities.Numbers.Clear();
                            cell.Possibilities.Numbers.Add(key);
                        }
                    }
                }
            }
        }
    }
}
