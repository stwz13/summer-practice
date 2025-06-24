using System;
using System.Linq;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            var lowStr = str.Where(symb => !char.IsPunctuation(symb) && !char.IsWhiteSpace(symb)).Select(char.ToLower).ToArray();

            return lowStr.SequenceEqual(lowStr.Reverse());
        }
    }
}
