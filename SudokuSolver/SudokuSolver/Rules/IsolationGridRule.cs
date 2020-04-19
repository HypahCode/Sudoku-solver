using System;
using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class IsolationGridRule : ISudokuRule
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
            var cellsGrid = NineCellSolver.Get3x3SubGrid(grid, xCellGroup, yCellGroup);

            for (int i = 0; i < 3; i++)
            {
                var cellsRow = NineCellSolver.GetRow(grid, (yCellGroup * 3) + i);
                IsolatePossibilities(cellsGrid, cellsRow);
            }
            for (int i = 0; i < 3; i++)
            {
                var cellsCol = NineCellSolver.GetColumn(grid, (xCellGroup * 3) + i);
                IsolatePossibilities(cellsGrid, cellsCol);
            }
        }

        private void IsolatePossibilities(List<GridCell> cells, List<GridCell> otherCells)
        {
            foreach (var cell in cells)
            {
                if (otherCells.Contains(cell))
                {
                    foreach (var num in cell.Possibilities.Numbers)
                    {
                        var count1 = CountNumber(cells, num);
                        var count2 = CountNumberWithin(cells, otherCells, num);
                        if (count1 == count2)
                        {
                            Isolate(cells, otherCells, num);
                        }
                    }
                }
            }
        }

        private int CountNumber(List<GridCell> cells, int number)
        {
            int count = 0;
            foreach (var cell in cells)
            {
                if (cell.Possibilities.Numbers.Contains(number))
                {
                    count++;
                }
            }
            return count;
        }

        private int CountNumberWithin(List<GridCell> cells, List<GridCell> otherCells, int number)
        {
            int count = 0;
            foreach (var cell in cells)
            {
                if (otherCells.Contains(cell) && cell.Possibilities.Numbers.Contains(number))
                {
                    count++;
                }
            }
            return count;
        }

        private void Isolate(List<GridCell> cells, List<GridCell> otherCells, int num)
        {
            foreach (var cell in otherCells)
            {
                if ((!cells.Contains(cell)) && (cell.Possibilities.Numbers.Contains(num)))
                {
                    cell.Possibilities.Remove(num);
                }
            }
        }
    }
}
