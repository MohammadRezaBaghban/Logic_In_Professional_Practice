using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Modules;

namespace LPP.Visitor_Pattern
{
    public class Calculator : IVisitor
    {
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

        public void Visit(BiImplication visitable) => visitable.Data =
            (visitable.LeftNode.Data && visitable.RightNode.Data) ||
            (!visitable.LeftNode.Data && !visitable.RightNode.Data);


        public void Visit(Implication visitable) =>
            visitable.Data = !(visitable.LeftNode.Data && !visitable.RightNode.Data);

        public void Visit(Disjunction visitable) =>
            visitable.Data = visitable.LeftNode.Data || visitable.RightNode.Data;

        public void Visit(Conjunction visitable) =>
            visitable.Data = visitable.LeftNode.Data && visitable.RightNode.Data;

        public void Visit(Nand visitable) =>
            visitable.Data = !(visitable.LeftNode.Data && visitable.RightNode.Data);

        public void Visit(Negation visitable) => visitable.Data = !visitable.LeftNode.Data;

        public void Visit(TruthTable truthTable)
        {
            //Traverse Each Row
            foreach (var currentRow in truthTable.NormalRows)
            {
                for (var j = 0; j < currentRow.PropositionValues.Length; j++)
                {
                    truthTable.SetValue_Of_Propositional_Variables(
                        variable:truthTable.DistinctPropositionalVariables[j],
                        value: (bool) currentRow.PropositionValues[j]
                        );
                }
                this.Calculate(truthTable.RootOfBinaryTree);
                currentRow.SetValue(truthTable.RootOfBinaryTree.Data);
            }
        }
    }
}
