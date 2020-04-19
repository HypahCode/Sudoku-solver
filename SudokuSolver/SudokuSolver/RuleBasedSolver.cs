using System.Collections.Generic;

namespace SudokuSolver
{
    public class RuleBasedSolver
    {
        public List<ISudokuRule> Rules { get; set; } = new List<ISudokuRule>();

        public RuleBasedSolver(IEnumerable<ISudokuRule> rules)
        {
            Rules.AddRange(rules);
        }

        public void SolveStep(SudokuGrid grid)
        {
            foreach (var rule in Rules)
            {
                rule.SolveStep(grid);                
            }
            grid.SetSolvedWhereConvinced();
        }
    }
}
