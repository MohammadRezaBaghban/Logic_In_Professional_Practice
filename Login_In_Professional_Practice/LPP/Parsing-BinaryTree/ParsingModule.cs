using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;
using Component = LPP.Composite_Pattern.Components.Component;

namespace LPP
{
    /// <summary>
    /// A static class for the sake of parsing user input
    /// </summary>
    public static class ParsingModule
    {

        private static BinaryTree _binaryTree = new BinaryTree();
        public static List<char> Elements = new List<char>();
        public static char[] Connectives = new char[] { '~', '>', '=', '&', '|','%' };
        public static int NodeCounter;

        /// <summary>
        /// Parse the input formula and generate binary tree object out of it
        /// </summary>
        /// It will parse the given formula recursively to extract formula elements
        /// Generate the binary tree out of the given formula
        ///  <param name="input">prefix abstract proposition formula</param>
        /// <returns>The binaryTree object </returns>
        public static BinaryTree ParseInput(string input)
        {
            EraseParsedList();
            ParseInputRecursively(ref input);
            GenerateBinaryTree(Elements);
            _binaryTree.MakeIt_Non_Modifiable();
            return _binaryTree;
        }

        public static BinaryTree ParseInputPredicate(string input)
        {
            EraseParsedList();
            var quantifierType = input.Substring(0, 1);
            var boundVariables = input.Substring(1, input.IndexOf(".") - 1).ToArray();
            Quantifier quantifier = quantifierType.Equals("@") == true ? (Quantifier) new Universal(boundVariables) : new Existential(boundVariables);
            
            var indexOfFirstParanthesis = input.IndexOf("("); var indexOfLastParanthesis = input.LastIndexOf(")");
            var formula = input.Substring(indexOfFirstParanthesis+1, indexOfLastParanthesis-indexOfFirstParanthesis-1);

            return BinaryTree.Object;
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
                    throw new Exception($"Unknown Character:{expression[0]}" +
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
                        case '%':
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
        private static void GenerateBinaryTree(List<char> input)
        {
            Component root = _binaryTree.Root;
            for (var i = 0; i <= input.Count - 1; i++)
            {
                var currentCharacter = input[i];
                if (CharacterType(currentCharacter) == characterType.PropositionalVariable)
                {
                    switch (currentCharacter)
                    {
                        // Values
                        case '0':
                            _binaryTree.InsertNode(root, new TrueFalse(false));
                            break;
                        case '1':
                            _binaryTree.InsertNode(root, new TrueFalse(true));
                            break;

                        //Variables                    
                        default:
                            var propositionVariable = new Variable(currentCharacter);
                            _binaryTree.InsertNode(root, propositionVariable);
                            break;
                    }
                }
                else
                {
                    switch (currentCharacter)
                    {
                        //Two Operands Connectives
                        case '>':
                            root = _binaryTree.InsertNode(root, new Implication());
                            break;
                        case '=':
                            root = _binaryTree.InsertNode(root, new BiImplication());
                            break;
                        case '%':
                            root = _binaryTree.InsertNode(root, new Nand());
                            break;
                        case '&':
                            root = _binaryTree.InsertNode(root, new Conjunction());
                            break;
                        case '|':
                            root = _binaryTree.InsertNode(root, new Disjunction());
                            break;


                        //One Operand Connective
                        case '~':
                            root = _binaryTree.InsertNode(root, new Negation());
                            break;
                    }
                }
            }
            NodeCounter = 0;
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
            _binaryTree = new BinaryTree();
            Elements.Clear();
            NodeCounter = 0;
            _binaryTree.Root = null;
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