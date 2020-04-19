using System;
using System.Collections.Generic;

namespace SudokuSolver.Rules
{
    public class PairIsolationRule : ISudokuRule
    {
        private class NumCounts
        {
            public int Number { get; }
            public List<GridCell> Cells { get; }
            public NumCounts(int num, List<GridCell> cells)
            {
                Number = num;
                Cells = cells;
            }
        }

        private class CellPair
        {
            public List<int> Numbers { get; }
            public List<GridCell> Cells { get; }
            public CellPair(int num, List<GridCell> cells)
            {
                Numbers = new List<int>(new[] { num });
                Cells = cells;
            }
        }


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

        private void EditPossibilities(SudokuGrid grid, int x, int y)
        {
            var cells = NineCellSolver.Get3x3SubGrid(grid, x, y);

            var numCounts = new Dictionary<int, List<NumCounts>>();
            for (int num = 1; num < 10; num++)
            {
                var cellsCount = GetAllCellsContainingNumber(cells, num);
                if (cellsCount.Count > 0)
                {
                    if (numCounts.ContainsKey(cellsCount.Count))
                    {
                        numCounts[cellsCount.Count].Add(new NumCounts(num, cellsCount));
                    }
                    else
                    {
                        numCounts[cellsCount.Count] = new List<NumCounts>(new[] { new NumCounts(num, cellsCount) });
                    }
                }
            }

            foreach (var kv in numCounts)
            {
                if (kv.Key < kv.Value.Count)
                {
                    var groups = GroupByNumbers(kv.Value);
                    foreach (var group in groups)
                    {
                        if (group.Numbers.Count == group.Cells.Count)
                        {
                            Isolate(grid, group);
                        }
                    }
                }
            }
        }

        private List<CellPair> GroupByNumbers(List<NumCounts> cellCounts)
        {
            var commonGroups = new List<CellPair>();
            foreach (var cellList in cellCounts)
            {
                var pair = commonGroups.Find((x) => CellListEquals(x.Cells, cellList.Cells));
                if (pair == null)
                {
                    commonGroups.Add(new CellPair(cellList.Number, cellList.Cells));
                }
                else
                {
                    pair.Numbers.Add(cellList.Number);
                }
            }
            return commonGroups;
        }

        private void Isolate(SudokuGrid grid, CellPair group)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    IsolateGroupOfNine(group, NineCellSolver.Get3x3SubGrid(grid, x, y));
                }
            }
            for (int i = 0; i < 9; i++)
            {
                IsolateGroupOfNine(group, NineCellSolver.GetColumn(grid, i));
                IsolateGroupOfNine(group, NineCellSolver.GetRow(grid, i));
            }
        }

        private void IsolateGroupOfNine(CellPair group, List<GridCell> cells)
        {
            // Count cells
            int count = 0;
            foreach (var groupCell in group.Cells)
            {
                if (cells.Contains(groupCell))
                {
                    count++;
                }
            }

            if (count == group.Cells.Count)
            {
                var range = new NumberRange();
                foreach (var num in group.Numbers)
                {
                    range.Add(num);
                }
                foreach (var cell in group.Cells)
                {
                    cell.Possibilities.RemoveAllBut(range);
                }
            }
        }

        private bool CellListEquals(List<GridCell> cellsA, List<GridCell> cellsB)
        {
            if (cellsA.Count == cellsB.Count)
            {
                foreach (var cell in cellsA)
                {
                    if (!cellsB.Contains(cell))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private List<GridCell> GetAllCellsContainingNumber(List<GridCell> cells, int num)
        {
            var result = new List<GridCell>();
            foreach (var cell in cells)
            {
                if (cell.Possibilities.Numbers.Contains(num))
                {
                    result.Add(cell);
                }
            }
            return result;
        }
    }
}
