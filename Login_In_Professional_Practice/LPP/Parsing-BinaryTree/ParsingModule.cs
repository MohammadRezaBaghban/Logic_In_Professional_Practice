﻿using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;
using LPP.Visitor_Pattern;
using Component = LPP.Composite_Pattern.Components.Component;

namespace LPP
{
    /// <summary>
    /// A static class for the sake of parsing user input
    /// </summary>
    public static class ParsingModule
    {
        private static Component lastVariableContainingNode = null;
        private static BinaryTree _binaryTree = new BinaryTree();
        private static readonly List<char> Elements = new List<char>();
        private enum CharacterType { PropositionalVariable, Connectives, Quantifier, Predicate, Unknown }
        private static readonly char[] Connectives = new char[] { '~', '>', '=', '&', '|', '%' };
        private static readonly char[] Quantifiers = new char[] { '!', '@', '.' };
        public static int NodeCounter;


        /// <summary>
        /// Parse the input formula and generate binary tree object out of it
        /// </summary>
        /// It will parse the given formula (Proposition or Predicate) recursively and extract formula elements
        /// abd Generate the binary tree out of the given formula
        ///  <param name="input">prefix proposition or predicate formula</param>
        /// <returns>The BinaryTree object </returns>
        public static BinaryTree Parse(string input)
        {
            EraseParsedList();
            var isPredicate = input.Contains("@") || input.Contains("!");
            ParseRecursively(ref input, isPredicate);
            
            if (isPredicate) GenerateBinaryTreePredicate();
            else GenerateBinaryTreeProposition();
            _binaryTree.MakeIt_Non_Modifiable();
            return _binaryTree;
        }

        /// <summary>
        /// Parse an input in the format of string and extract the list of formula elements
        /// </summary>
        /// <returns>return string excluding processed section</returns>
        private static string ParseRecursively(ref string expression, bool isPredicate) =>
            string.IsNullOrEmpty(expression) ? null :
            !isPredicate ? ParseProposition(ref expression, false) :
            ParsePredicate(ref expression, true);
        
        private static string ParseProposition(ref string expression, bool isPredicate)
        {
            var currentCharacterType = TypeOfCharacter(expression[0], isPredicate);
            if (expression[0] == ' ' || expression[0] == ',' || expression[0] == ')')
            {
                EatMethod(ref expression);
                return ParseRecursively(ref expression, isPredicate);
            }
            if (currentCharacterType == CharacterType.Unknown)
            {
                throw new Exception($"Unknown Character:{expression[0]}" +
                                       "\n\nThe Propositional formula contain Invalid Characters\n" +
                                       "Propositional Variables: English Capital Letter - 0,1\n" +
                                       "Connectives: ~,>,=,&,|\n" + "Separators: '(', ',' ,')'");
            }

            if (currentCharacterType == CharacterType.PropositionalVariable)
            {
                Elements.Add(expression[0]);
                EatMethod(ref expression);
                return ParseRecursively(ref expression, isPredicate);
            }
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
        private static string ParsePredicate(ref string expression, bool isPredicate)
        {
            var currentChar = expression[0];
            var currentCharacterType = TypeOfCharacter(currentChar, isPredicate);
            if (expression[0] == ' ' || expression[0] == ',' || expression[0] == ')')
            {
                EatMethod(ref expression);
                return ParseRecursively(ref expression, isPredicate);
            }

            if (currentCharacterType == CharacterType.Unknown)
            {
                throw new Exception($"Unknown Character:{expression[0]}" +
                                       "\n\nThe Predicate contain Invalid Characters\n" +
                                       "Propositional Variables: English Capital Letter - 0,1\n" +
                                       "Quantifier: @,!\n" + "Connectives: ~,>,=,&,|\n" +
                                       "Separators: '(', ',' ,')' ,'.'");
            }

            if (currentCharacterType == CharacterType.PropositionalVariable)
            {
                var lastElementType = TypeOfCharacter(Elements[Elements.Count - 1], true);
                if (lastElementType == CharacterType.Quantifier)
                {
                    expression.Substring(0, expression.IndexOf(".", StringComparison.Ordinal)).ToList().ForEach(x => Elements.Add(x));
                    EatMethod(ref expression, expression.IndexOf("(", StringComparison.Ordinal) + 1);
                    return ParseRecursively(ref expression, isPredicate);
                }

                if (lastElementType == CharacterType.Predicate)
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
            else if (currentCharacterType == CharacterType.Predicate)
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
                        ParseRecursively(ref a2, isPredicate);
                        expression = expression.Remove(0, a2.Length);
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
        /// Generate a binary tree based for a proposition formula
        /// </summary>
        /// <returns>The root of binary tree</returns>
        private static void GenerateBinaryTreeProposition()
        {
            Component root = _binaryTree.Root;
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var currentCharacter = Elements[i];
                var currentCharacterType = TypeOfCharacter(currentCharacter, false);
                if (currentCharacterType == CharacterType.PropositionalVariable)
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
                else if (currentCharacterType == CharacterType.Connectives)
                {
                    root = GenerateOperatorObject(currentCharacter, root);
                }
            }
            NodeCounter = 0;
        }

        /// <summary>
        /// Generate a binary tree based for a Predicate formula
        /// </summary>
        /// <returns>The root of binary tree</returns>
        private static void GenerateBinaryTreePredicate()
        {
            Component root = _binaryTree.Root;
            for (var i = 0; i <= Elements.Count - 1; i++)
            {
                var currentCharacter = Elements[i];
                var currentCharacterType = TypeOfCharacter(currentCharacter, true);

                if (currentCharacterType == CharacterType.PropositionalVariable)
                {
                    Variable propositionVariable = null;
                    if (lastVariableContainingNode is Universal || lastVariableContainingNode is Existential)
                        propositionVariable = new Variable(currentCharacter, true);
                    else if (lastVariableContainingNode is Predicate)
                    {
                        propositionVariable = new Variable(currentCharacter);
                        if (_binaryTree.PropositionalVariables.Get_BindVariables().Exists(x => x.Symbol == currentCharacter))
                            propositionVariable.BindVariable = true;
                    }
                    _binaryTree.InsertNode(lastVariableContainingNode, propositionVariable);
                }
                else if (currentCharacterType == CharacterType.Predicate)
                {
                    lastVariableContainingNode = new Predicate(currentCharacter);
                    root = _binaryTree.InsertNode(root, lastVariableContainingNode);
                }
                else if (currentCharacterType == CharacterType.Connectives || currentCharacterType == CharacterType.Quantifier)
                {
                    root = GenerateOperatorObject(currentCharacter, root);
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
            if (!string.IsNullOrEmpty(input)) input = input.Remove(0, count == 0 ? 1 : count);
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
            Tableaux.VarIndex = 0;
            _binaryTree.Root = null;
            lastVariableContainingNode = null;
        }

        private static CharacterType TypeOfCharacter(char character, bool predicate)
        {
            if (!predicate)
            {
                // If the character is Propositional Variables or true/false  
                if (char.IsUpper(character) || character == '0' || character == '1')
                {
                    return CharacterType.PropositionalVariable;
                }
                return Connectives.Contains(character) ? CharacterType.Connectives : CharacterType.Unknown;
            }

            // If the character is Propositional Variables or true/false  
            if (char.IsUpper(character))
                return CharacterType.Predicate;
            if (char.IsLower(character))
                return CharacterType.PropositionalVariable;
            if (Quantifiers.Contains(character))
                return CharacterType.Quantifier;
            if (Connectives.Contains(character))
                return CharacterType.Connectives;
            return CharacterType.Unknown;

        }

        private static Component GenerateOperatorObject(char operatorCharacter, Component root)
        {
            switch (operatorCharacter)
            {
                case '>':
                    return _binaryTree.InsertNode(root, new Implication());
                case '=':
                    return _binaryTree.InsertNode(root, new BiImplication());
                case '%':
                    return  _binaryTree.InsertNode(root, new Nand());
                case '&':
                    return _binaryTree.InsertNode(root, new Conjunction());
                case '|':
                    return _binaryTree.InsertNode(root, new Disjunction());
                case '~':
                    return _binaryTree.InsertNode(root, new Negation());
                case '@':
                    lastVariableContainingNode = new Universal();
                    return _binaryTree.InsertNode(root, lastVariableContainingNode);
                case '!':
                    lastVariableContainingNode = new Existential();
                    return _binaryTree.InsertNode(root, lastVariableContainingNode);
                default:
                    return null;
            }
        }
    }
}