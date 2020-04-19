using System;
using System.Collections.Generic;

namespace SudokuSolver
{
    public class NumberRange
    {
        public List<int> Numbers = new List<int>();
        
        public NumberRange()
        {

        }

        public void Fill()
        {
            for (int i = 1; i < 10; i++)
            {
                if (Numbers.IndexOf(i) == -1)
                {
                    Numbers.Add(i);
                }
            }
            Sort();
        }

        internal void Clear()
        {
            Numbers.Clear();
        }

        public void Sort()
        {
            Numbers.Sort();
        }
        
        public override string ToString()
        {
            return string.Join("", Numbers);
        }

        public bool Remove(int number)
        {
            return Numbers.Remove(number);
        }

        internal void Remove(NumberRange range)
        {
            foreach (var num in range.Numbers)
            {
                Remove(num);
            }
        }

        public bool Add(int number)
        {
            var result = Numbers.IndexOf(number) == -1;
            if (result)
            {
                Numbers.Add(number);
            }
            return result;
        }

        public bool ContainsAll (NumberRange numbers)
        {
            foreach (var num in numbers.Numbers)
            {
                if (Numbers.IndexOf(num) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveAllBut(NumberRange numbers)
        {
            Numbers.Clear();
            Numbers.AddRange(numbers.Numbers);
        }
    }
}
