using System;

namespace SudokuSolver
{
    public enum CellState { FixedNumber, Solved, Unsolved, Error }

    public class GridCell
    {
        public CellState State { get; set; } = CellState.Unsolved;

        public int Number { get; set; } = 0;

        public NumberRange Possibilities { get; set; } = new NumberRange();

        internal void SetFixed(int num)
        {
            State = ((num > 0) || (num < 10)) ? CellState.FixedNumber : CellState.Error;
            Number = num;
            Possibilities.Clear();
            Possibilities.Numbers.Add(num);
        }

        internal void SetSolved(int num)
        {
            State = ((num > 0) || (num < 10)) ? CellState.Solved : CellState.Error;
            Number = num;
            Possibilities.Clear();
            Possibilities.Numbers.Add(num);
        }

        internal void SetUnsolved()
        {
            State = CellState.Unsolved;
            Number = 0;
            Possibilities.Clear();
            Possibilities.Fill(); ;
        }
    }
}
