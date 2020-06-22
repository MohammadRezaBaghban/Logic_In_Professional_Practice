using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using IVisitor = LPP.Composite_Pattern.IVisitor;

namespace LPP.Visitor_Pattern
{
    /// <summary>
    /// A concrete Visitor class for generating infix formula from the root of main connective of the abstract proposition
    /// </summary>
    public class InfixFormulaGenerator:IVisitor
    {
        public static InfixFormulaGenerator Calculator { get; } = new InfixFormulaGenerator();

        /// <summary>
        /// Recursive Traversing Method for calculating the InFix formula of abstract Proposition. 
        /// </summary>
        /// <param name="visitable">The root of abstract proposition - Main Connective</param>
        public void Calculate(Component visitable)
        {
            if (!(visitable is SingleComponent))
            {
                CompositeComponent compositeNode = visitable as CompositeComponent;

                if (compositeNode is Negation)
                {
                    Calculate(compositeNode.LeftNode);
                    compositeNode.Evaluate(this);
                }
                else if (compositeNode != null)
                {
                    Calculate(compositeNode.RightNode);
                    Calculate(compositeNode.LeftNode);
                    compositeNode.Evaluate(this);
                }
            }
        }

        public void Visit(Negation visitable)
        {
            if(visitable.LeftNode is CompositeComponent)
            {
                visitable.InFixFormula = $"¬({visitable.LeftNode.InFixFormula})";
            }
            if(visitable.LeftNode is SingleComponent)
            {
                visitable.InFixFormula = $"¬{visitable.LeftNode.InFixFormula}";
            }
        }
        public void Visit(BiImplication visitable) => GenerateInfixGenerator(visitable, '=');
        public void Visit(Implication visitable) => GenerateInfixGenerator(visitable, '>');
        public void Visit(Disjunction visitable) => GenerateInfixGenerator(visitable, '|');
        public void Visit(Conjunction visitable) => GenerateInfixGenerator(visitable, '&');
        public void Visit(Nand visitable) => GenerateInfixGenerator(visitable, '%');

        public void Visit(Universal visitable) =>
            visitable.InFixFormula =
                $"∀{visitable.BoundVariables.SelectMany(x => x).ToString()}" +
                $"[{visitable.LeftNode.InFixFormula}]";

        public void Visit(Existential visitable) =>
            visitable.InFixFormula =
                $"∃{visitable.boundVariables.SelectMany(x => x).ToString()}" +
                $"[{visitable.LeftNode.InFixFormula}]";

        /// <summary>
        /// Method for create more intuitive infix formula based on the type of left and right node.
        /// </summary>
        /// <param name="visitable">The upper root of component</param>
        /// <param name="connective">The character of connective</param>
        private void GenerateInfixGenerator(CompositeComponent visitable, char connective)
        {
            if (visitable.LeftNode is CompositeComponent && visitable.RightNode is CompositeComponent)
            {
                visitable.InFixFormula = $"({visitable.LeftNode.InFixFormula}){connective}({visitable.RightNode.InFixFormula})";
            }
            else if (visitable.LeftNode is CompositeComponent)
            {
                visitable.InFixFormula = $"({visitable.LeftNode.InFixFormula}){connective}{visitable.RightNode.InFixFormula}";
            }
            else if (visitable.RightNode is CompositeComponent)
            {
                visitable.InFixFormula = $"{visitable.LeftNode.InFixFormula}{connective}({visitable.RightNode.InFixFormula})";
            }
            else
            {
                visitable.InFixFormula = $"{visitable.LeftNode.InFixFormula}{connective}{visitable.RightNode.InFixFormula}";
            }
        }
    }
}
