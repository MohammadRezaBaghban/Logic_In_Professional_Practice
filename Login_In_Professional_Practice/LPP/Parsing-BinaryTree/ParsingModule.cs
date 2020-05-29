using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using Component = LPP.Composite_Pattern.Component;

namespace LPP.Modules
{
    /// <summary>
    /// A static class for the sake of parsing user input
    /// </summary>
    public static class ParsingModule
    {

        private static readonly BinaryTree Bt = new BinaryTree();
        public static List<char> Elements = new List<char>();
        public static char[] Connectives = new char[] { '~', '>', '=', '&', '|' };
        public static int NodeCounter;

        /// <summary>
        /// Parse the input formula and generate binary tree object out of it
        /// </summary>
        /// It will parse the given formula recursively to extract formula elements
        /// Generate the binary tree out of the given formula
        ///  <param name="input">prefix abstract proposition formula</param>
        /// <returns>The root of generate BinaryTree</returns>
        public static CompositeComponent ParseInput(string input)
        {
            EraseParsedList();
            ParseInputRecursively(ref input);
            GenerateBinaryTree(Elements);
            return Bt._root;
        }


        /// <summary>
        /// Parse an input in the format of string and extract the list of formula elements
        /// </summary>
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
                    throw new LPPException($"Unknown Character:{expression[0]}" +
                                           "\n\nThe formula contain Invalid Characters\n" +
                                           "Propositional Variables: English Capital Letter - 0,1\n" +
                                           "Connectives: ~,>,=,&,|\n" +
                                           "Separators: '(', ',' ,')'");
                }
                else if (CharacterType(expression[0]) == characterType.PropositionalVariable)
                {
                    Elements.Add(expression[0]);
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
                            Elements.Add(expression[0]);
                            EatMethod(ref expression, 2);
                            string a1 = expression.Substring(0, expression.IndexOf(')') + 1);
                            expression = expression.Remove(0, a1.Length);
                            ParseInputRecursively(ref a1);
                            string a2 = expression.Substring(0, expression.IndexOf(')') + 1);
                            expression = expression.Remove(0, a2.Length);
                            ParseInputRecursively(ref a2);
                            return ParseInputRecursively(ref expression);
                        case '~':
                            Elements.Add(expression[0]);
                            EatMethod(ref expression, 2);
                            return ParseInputRecursively(ref expression);
                        default:
                            return null;
                    }
                }
            }
        }


        /// <summary>
        /// Interact with BinaryTree instance to generate a binary tree based on element
        /// </summary>
        /// <param name="input">list of elements in the binary tree</param>
        /// <returns>The root of binary tree</returns>
        private static Component GenerateBinaryTree(List<char> input)
        {
            Component root = Bt._root;
            for (var i = 0; i <= input.Count - 1; i++)
            {
                var currentCharacter = input[i];
                if (CharacterType(currentCharacter) == characterType.PropositionalVariable)
                {
                    switch (currentCharacter)
                    {
                        // Values
                        case '0':
                            Bt.InsertNode(root, new TrueFalse(false));
                            break;
                        case '1':
                            Bt.InsertNode(root, new TrueFalse(true));
                            break;

                        //Variables                    
                        default:
                            var propositionVariable = new PropositionalVariable(currentCharacter);
                            Bt._root.PropositionalVariables.AddPropositionalVariable(propositionVariable);
                            Bt.InsertNode(root, propositionVariable);
                            break;
                    }
                }
                else
                {
                    switch (currentCharacter)
                    {
                        //Two Operands Connectives
                        case '>':
                            root = Bt.InsertNode(root, new ImplicationConnective());
                            break;
                        case '=':
                            root = Bt.InsertNode(root, new Bi_ImplicationConnective());
                            break;
                        case '&':
                            root = Bt.InsertNode(root, new ConjunctionConnective());
                            break;
                        case '|':
                            root = Bt.InsertNode(root, new DisjunctionConnective());
                            break;


                        //One Operand Connective
                        case '~':
                            root = Bt.InsertNode(root, new NegationConnective());
                            break;
                    }
                }
            }
            NodeCounter = 0;
            return Bt._root;
        }

        /// <summary>
        /// A 3 modes string removal method
        /// </summary>
        /// <param name="input">Source string which removal wants to be applied</param>
        /// <param name="count">Remove till first occurence of a given character OR for specified number of characters</param>
        /// <returns></returns>
        private static void EatMethod(ref string input, int count = 0)
        {
            if (!String.IsNullOrEmpty(input))
            {
                if (count == 0)
                {
                    input = input.Remove(0, 1);
                }
                else
                {
                    input = input.Remove(0, count);
                }
            }
        }

        /// <summary>
        /// Clear previous parsed formula and its associated binary tree
        /// </summary>
        /// Should be called before any external call of ParseInputRecursively
        private static void EraseParsedList()
        {
            Elements.Clear();
            NodeCounter = 0;
            Bt._root = null;
        }

        private static characterType CharacterType(char character)
        {
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