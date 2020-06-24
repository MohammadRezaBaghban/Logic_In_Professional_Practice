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
        private enum characterType { PropositionalVariable, Connectives, Quantifier, Predicate, Unknown }
        public static char[] Connectives = new char[] { '~', '>', '=', '&', '|', '%' };
        public static char[] Quantifiers = new char[] { '!', '@', '.' };
        public static int NodeCounter;

        public static BinaryTree Parse(string input)
        {
            BinaryTree root;
            if (input.Contains("@") || input.Contains("!"))
            {
                root = ParseInputPredicate(input);
            }
            else
            {
                root = ParseInputProposition(input);
            }
            return root;
        }

        /// <summary>
        /// Parse the input formula and generate binary tree object out of it
        /// </summary>
        /// It will parse the given formula recursively to extract formula elements
        /// Generate the binary tree out of the given formula
        ///  <param name="input">prefix abstract proposition formula</param>
        /// <returns>The binaryTree object </returns>
        private static BinaryTree ParseInputProposition(string input)
        {
            EraseParsedList();
            ParseRecursively(ref input, isPredicate: false);
            GenerateBinaryTreeProposition(Elements);
            _binaryTree.MakeIt_Non_Modifiable();
            return _binaryTree;
        }

        public static BinaryTree ParseInputPredicate(string input)
        {
            EraseParsedList();
            ParseRecursively(ref input, isPredicate: true);
            GenerateBinaryTreePredicate(Elements);
            _binaryTree.MakeIt_Non_Modifiable();
            return _binaryTree;
        }

        /// <summary>
        /// Parse an input in the format of string and extract the list of formula elements
        /// </summary>
        /// <returns>return string excluding processed section</returns>
        private static string ParseRecursively(ref string expression, bool isPredicate)
        {
            if (string.IsNullOrEmpty(expression))
                return null;
            return !isPredicate ? ParseProposition(ref expression, isPredicate) : ParsePredicate(ref expression, isPredicate);
        }
        private static string ParseProposition(ref string expression, bool isPredicate)
        {
            var currentCharacterType = CharacterType(expression[0], isPredicate);
            if (expression[0] == ' ' || expression[0] == ',' || expression[0] == ')')
            {
                EatMethod(ref expression);
                return ParseRecursively(ref expression, isPredicate);
            }
            if (currentCharacterType == characterType.Unknown)
            {
                throw new Exception($"Unknown Character:{expression[0]}" +
                                       "\n\nThe Propositional formula contain Invalid Characters\n" +
                                       "Propositional Variables: English Capital Letter - 0,1\n" +
                                       "Connectives: ~,>,=,&,|\n" + "Separators: '(', ',' ,')'");
            }
            else if (currentCharacterType == characterType.PropositionalVariable)
            {
                Elements.Add(expression[0]);
                EatMethod(ref expression);
                return ParseRecursively(ref expression, isPredicate);
            }
            else
            {
                switch (expression[0])
                {
                    case '>': case '=': case '%': case '&': case '|':
                        Elements.Add(expression[0]);
                        EatMethod(ref expression, 2);
                        string a1 = expression.Substring(0, expression.IndexOf(')') + 1);
                        expression = expression.Remove(0, a1.Length);
                        ParseRecursively(ref a1, isPredicate);
                        string a2 = expression.Substring(0, expression.IndexOf(')') + 1);
                        expression = expression.Remove(0, a2.Length);
                        ParseRecursively(ref a2, isPredicate);
                        return ParseRecursively(ref expression, isPredicate);
                    case '~':
                        Elements.Add(expression[0]);
                        EatMethod(ref expression, 2);
                        return ParseRecursively(ref expression, isPredicate);
                    default:
                        return null;
                }
            }
        }
        private static string ParsePredicate(ref string expression, bool isPredicate)
        {
            var currentCharacterType = CharacterType(expression[0], isPredicate);
            if (expression[0] == ' ' || expression[0] == ',' || expression[0] == ')')
            {
                EatMethod(ref expression);
                return ParseRecursively(ref expression, isPredicate);
            }

            if (currentCharacterType == characterType.Unknown)
            {
                throw new Exception($"Unknown Character:{expression[0]}" +
                                       "\n\nThe Predicate contain Invalid Characters\n" +
                                       "Propositional Variables: English Capital Letter - 0,1\n" +
                                       "Quantifier: @,!\n" + "Connectives: ~,>,=,&,|\n" +
                                       "Separators: '(', ',' ,')' ,'.'");
            }
            else if (currentCharacterType == characterType.PropositionalVariable)
            {
                var lastElementType = CharacterType(Elements[Elements.Count - 1], true);
                if (lastElementType == characterType.Quantifier)
                {
                    expression.Substring(0, expression.IndexOf(".", StringComparison.Ordinal)).ToList().ForEach(x => Elements.Add(x));
                    EatMethod(ref expression, expression.IndexOf("(", StringComparison.Ordinal) + 1);
                    return ParseRecursively(ref expression, isPredicate);
                }
                else if (lastElementType == characterType.Predicate)
                {
                    var str = expression.Substring(0, expression.IndexOf(")", StringComparison.Ordinal));
                    if (str.Length > 1)
                    {
                        str.Split(',').ToList().ForEach(x => Elements.Add(Convert.ToChar(x)));
                    }
                    else
                    {
                        Elements.Add(expression[0]);
                    }
                    EatMethod(ref expression, expression.IndexOf(")", StringComparison.Ordinal) + 1);
                    return ParseRecursively(ref expression, isPredicate);
                }
            }
            else if (currentCharacterType == characterType.Predicate)
            {
                Elements.Add(expression[0]);
                EatMethod(ref expression, 2);
                return ParseRecursively(ref expression, isPredicate);

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
                        ParseRecursively(ref a1, isPredicate);
                        string a2 = expression.Substring(0, expression.IndexOf(',') + 1);
                        expression = expression.Remove(0, a2.Length);
                        //ParseRecursively(ref a2, isPredicate);
                        return ParseRecursively(ref expression, isPredicate);
                    case '~':
                        Elements.Add(expression[0]);
                        EatMethod(ref expression, 2);
                        return ParseRecursively(ref expression, isPredicate);
                    case '!':
                    case '@':
                        Elements.Add(expression[0]);
                        EatMethod(ref expression, 1);
                        return ParseRecursively(ref expression, isPredicate);
                    default:
                        return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Interact with BinaryTree instance to generate a binary tree based on element
        /// </summary>
        /// <param name="input">list of elements in the binary tree</param>
        /// <returns>The root of binary tree</returns>
        private static void GenerateBinaryTreeProposition(List<char> input)
        {
            Component root = _binaryTree.Root;
            for (var i = 0; i <= input.Count - 1; i++)
            {
                var currentCharacter = input[i];
                var currentCharacterType = CharacterType(currentCharacter, false);
                if (currentCharacterType == characterType.PropositionalVariable)
                {
                    if (currentCharacter == '0')
                    {
                        _binaryTree.InsertNode(root, new TrueFalse(false));
                    }
                    else if (currentCharacter == '1')
                    {
                        _binaryTree.InsertNode(root, new TrueFalse(true));
                    }
                    else
                    {
                        var propositionVariable = new Variable(currentCharacter);
                        _binaryTree.InsertNode(root, propositionVariable);
                    }
                }
                else if (currentCharacterType == characterType.Connectives)
                {
                    if (currentCharacter == '>')
                        root = _binaryTree.InsertNode(root, new Implication());
                    else if (currentCharacter == '=')
                        root = _binaryTree.InsertNode(root, new BiImplication());
                    else if (currentCharacter == '%')
                        root = _binaryTree.InsertNode(root, new Nand());
                    else if (currentCharacter == '&')
                        root = _binaryTree.InsertNode(root, new Conjunction());
                    else if (currentCharacter == '|')
                        root = _binaryTree.InsertNode(root, new Disjunction());
                    else if (currentCharacter == '~') root = _binaryTree.InsertNode(root, new Negation());
                }
            }
            NodeCounter = 0;
        }
        private static void GenerateBinaryTreePredicate(List<char> input)
        {
            Component root = _binaryTree.Root;
            Component lastQuantifier = null;
            Component lastVariableContainingNode = null;
            for (var i = 0; i <= input.Count - 1; i++)
            {
                var currentCharacter = input[i];
                var currentCharacterType = CharacterType(currentCharacter, true);

                if (currentCharacterType == characterType.PropositionalVariable)
                {
                    Variable propositionVariable = null;
                    if (lastVariableContainingNode is Universal || lastVariableContainingNode is Existential)
                        propositionVariable = new Variable(currentCharacter,true);
                    else if (lastVariableContainingNode is Predicate)
                    {
                        propositionVariable = new Variable(currentCharacter, false);
                        if (_binaryTree.PropositionalVariables.Get_BindVariables().Exists(x => x.Symbol == currentCharacter))
                            propositionVariable.bindVariable = true;
                    }
                    _binaryTree.InsertNode(lastVariableContainingNode, propositionVariable);
                }
                else if (currentCharacterType == characterType.Predicate)
                {
                    lastVariableContainingNode = new Predicate(currentCharacter);
                    root = _binaryTree.InsertNode(root, lastVariableContainingNode);
                }
                else if (currentCharacterType == characterType.Connectives)
                {
                    if (currentCharacter == '>')
                        root = _binaryTree.InsertNode(root, new Implication());
                    else if (currentCharacter == '=')
                        root = _binaryTree.InsertNode(root, new BiImplication());
                    else if (currentCharacter == '%')
                        root = _binaryTree.InsertNode(root, new Nand());
                    else if (currentCharacter == '&')
                        root = _binaryTree.InsertNode(root, new Conjunction());
                    else if (currentCharacter == '|')
                        root = _binaryTree.InsertNode(root, new Disjunction());
                    else if (currentCharacter == '~') root = _binaryTree.InsertNode(root, new Negation());
                }
                else if (currentCharacterType == characterType.Quantifier)
                {
                    if (currentCharacter == '@')
                    {
                        lastVariableContainingNode = new Universal();
                        root = _binaryTree.InsertNode(root, lastVariableContainingNode);
                    }
                    else if (currentCharacter == '!')
                    {
                        lastVariableContainingNode = new Existential();
                        root = _binaryTree.InsertNode(root, lastVariableContainingNode);
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
            if (!String.IsNullOrEmpty(input)) input = input.Remove(0, count == 0 ? 1 : count);
        }

        /// <summary>
        /// Clear previous parsed formula and its associated binary tree
        /// </summary>
        /// Should be called before any external call of ParseRecursively
        private static void EraseParsedList()
        {
            _binaryTree = new BinaryTree();
            Elements.Clear();
            NodeCounter = 0;
            _binaryTree.Root = null;
        }

        private static characterType CharacterType(char character, bool predicate)
        {
            if (!predicate)
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
            else
            {
                // If the character is Propositional Variables or true/false  
                if (Char.IsUpper(character))
                {
                    return characterType.Predicate;
                }
                else if (Char.IsLower(character))
                {
                    return characterType.PropositionalVariable;
                }
                else if (Quantifiers.Contains(character))
                {
                    return characterType.Quantifier;
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

        }
    }
}