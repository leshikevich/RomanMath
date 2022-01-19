using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanMath.Impl
{
    public static class Service
    {
        /// <summary>
        /// See TODO.txt file for task details.
        /// Do not change contracts: input and output arguments, method name and access modifiers
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ///
        private static Dictionary<int, string> ra = new Dictionary<int, string>
             { { 1000, "M" },  { 900, "CM" },  { 500, "D" },  { 400, "CD" },  { 100, "C" },
                               { 90 , "XC" },  { 50 , "L" },  { 40 , "XL" },  { 10 , "X" },
                               { 9  , "IX" },  { 5  , "V" },  { 4  , "IV" },  { 1  , "I" } };

        private static List<char> validСharacters = new List<char> { '+', '-', '*', 'I', 'X', 'C',
                                                                     'L', 'D', 'V', 'M' };



        public static int Evaluate(string expression)
        {
            if (ValidateRomanString(expression))
            {
                List<int> arabicNumbers = GetArabicNumbers(expression);

                List<char> operators = GetOperators(expression);

                while (operators.Contains('*'))
                {
                    arabicNumbers[operators.IndexOf('*')] =
                    arabicNumbers[operators.IndexOf('*')] *
                    arabicNumbers[operators.IndexOf('*') + 1];

                    arabicNumbers.RemoveAt(operators.IndexOf('*') + 1);

                    operators.Remove('*');
                }

                while (operators.Count != 0)
                {
                    if (operators.Count != 0 && operators[0] == '+')
                    {
                        arabicNumbers[0] = arabicNumbers[0] + arabicNumbers[1];
                        arabicNumbers.RemoveAt(1);
                        operators.RemoveAt(0);
                    }
                    if (operators.Count != 0 && operators[0] == '-')
                    {
                        arabicNumbers[0] = arabicNumbers[0] - arabicNumbers[1];
                        arabicNumbers.RemoveAt(1);
                        operators.RemoveAt(0);
                    }
                }

                return arabicNumbers[0];
            }
            return 0;
        }

        private static List<char> GetOperators(string expression)
        {
            var operators = expression.Where(o => o == '+' || o == '-' || o == '*').ToList();
            return operators;
        }

        private static List<int> GetArabicNumbers(string expression)
        {
            List<string> romanNumbers = expression.Split('+', '-', '*').ToList();

            List<int> arabicNumbers = new List<int>(romanNumbers.Count);

            for (int i = 0; i < romanNumbers.Count; i++)
            {
                arabicNumbers.Add(ToArabic(romanNumbers[i]));
            }
            return arabicNumbers;
        }

        private static bool ValidateRomanString(string expression)
        {
            if (expression == null || expression.Length == 0)
            {
                throw new NullReferenceException();
            }
            if (expression.Except(validСharacters).Count() == 0)
            {
                Console.WriteLine("true");
                return true;
            }
            throw new NotSupportedException();
        }

        private static int ToArabic(string number) => number.Length == 0 ? 0 : ra
            .Where(d => number.StartsWith(d.Value))
            .Select(d => d.Key + ToArabic(number.Substring(d.Value.Length)))
            .First();
    }
}
//TODO: Method ValidateRomanString must be better.
