using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LPP
{
    /// <summary>
    /// A static class for the sake of parsing user input
    ///
    /// </summary>
    public static class ParsingModule
    {

        public  static List<string> elements = new List<string>();
        private static int nodeCount;

        public static void ParseInput(string input)
        {
            EraseParsedList();
            ParseInputRecursively(ref input);
        }


        /// <summary>
        /// Parse an input in the format of string and extract the list of formula elements
        /// </summary>
        /// <param name="userInput">Abstract Proposition Formula</param>
        /// <returns>return string excluding processed section</returns>
        private static string ParseInputRecursively(ref string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }
            else
            {
                if (expression[0] == ' ' || expression[0] == ',' || expression[0] == ')')
                {
                    EatMethod(ref expression);
                    return ParseInputRecursively(ref expression);
                }

                if (CharacterType(expression[0]) == characterType.Unknown)
                {
                    throw new LPPException("The formula contain Invalid Characters\n" +
                                           "Propositional Variables: English Capital Letter - 0,1\n" +
                                           "Connectives: ~,>,=,&,|\n" +
                                           "Separators: '(', ',' ,')'");
                }
                else if (CharacterType(expression[0]) == characterType.PropositionalVariable)
                {
                    elements.Add(expression[0].ToString());
                    EatMethod(ref expression);
                    return ParseInputRecursively(ref expression);
                }
                else
                {
                    switch (expression[0])
                    {
                        case '>':
                        case '=':
                        case '&':
                        case '|':
                            elements.Add(expression[0].ToString());
                            EatMethod(ref expression,2);
                            string a1 = expression.Substring(0, expression.IndexOf(')') + 1);
                            expression = expression.Remove(0, a1.Length);
                            ParseInputRecursively(ref a1);
                            string a2 = expression.Substring(0, expression.IndexOf(')') + 1);
                            expression = expression.Remove(0, a2.Length);
                            ParseInputRecursively(ref a2);
                            return ParseInputRecursively(ref expression);
                        case '~':
                            elements.Add(expression[0].ToString());
                            EatMethod(ref expression, 2);
                            return ParseInputRecursively(ref expression);
                        default:
                            return null;
                    }
                }
            }
        }

        /// <summary>
        /// A 3 modes string removal method
        /// </summary>
        /// <param name="input">Source string which removal wants to be applied</param>
        /// <param name="Count">Remove till first occurence of a given character OR for specified number of characters</param>
        /// <returns></returns>
        private static void EatMethod(ref string input, int Count = 0)
        {
            if (!String.IsNullOrEmpty(input))
            {
                if (Count == 0)
                {
                    input = input.Remove(0, 1);
                }
                else
                {
                    input = input.Remove(0, Count);
                }
            }
        }


        /// <summary>
        /// Clear previous parsed formula and its associated binary tree
        /// </summary>
        /// Should be called before any external call of ParseInputRecursively
        private static void EraseParsedList()
        {
            elements.Clear();
            nodeCount = 0;
            //bt._root = null;
        }

        private static characterType CharacterType(char character)
        {
            var Connectives = new char[] {
                '~','>','=','&','|'
            };

            // If the character is Propositional Variables or true/false  
            if (Char.IsUpper(character) || character == '0' || character == '1')
            {
                return characterType.PropositionalVariable;
            }
            else if (Connectives.Contains(character))
            {
                return characterType.Connectives;
            }
            else
            {
                return characterType.Unknown;
            }
        }

        enum characterType
        {
            PropositionalVariable,
            Connectives,
            Unknown
        }
    }
}