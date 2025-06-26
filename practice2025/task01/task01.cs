using System;
using System.Linq;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            var lowInput = input.Where(symb => !char.IsPunctuation(symb) && !char.IsWhiteSpace(symb)).Select(char.ToLower).ToArray();

            return lowInput.SequenceEqual(lowInput.Reverse());
        }
    }
}
