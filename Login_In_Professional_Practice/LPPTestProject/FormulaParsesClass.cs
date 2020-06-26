using LPP;
using LPP.Modules;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class FormulaParserClass
    {
        
        [Theory]
        [InlineData(">(&(|(~(A),D),=(A,E)),>(&(C,~(D)),B))", "(((¬A)|D)&(A=E))>((C&(¬D))>B)")]
        [InlineData(">(&(>(Q,R),>(~(Q),P)),|(P,R))", "((Q>R)&((¬Q)>P))>(P|R)")]
        [InlineData(">(|(A,>(B,C)),=(D,&(A,E))", "(A|(B>C))>(D=(A&E))")]
        [InlineData("|(~(>(A,B)),&(A,>(C,B)))", "(¬(A>B))|(A&(C>B))")]
        [InlineData(">(>(P,Q),>(&(R,1),Q))", "(P>Q)>((R&True)>Q)")]
        [InlineData(">(=(P,Q),&(R,~(P)))", "(P=Q)>(R&(¬P))")]
        [InlineData(">(A,=(B,&(0,C))", "A>(B=(False&C))")]
        [InlineData("|(=(A,B),&(C,A))", "(A=B)|(C&A)")]
        [InlineData(">(&(A,B),~(C))", "(A&B)>(¬C)")]
        [InlineData("&(&(A,B),~(A))", "(A&B)&(¬A)")]
        [InlineData("&(|(A,~(B)),C)", "(A|(¬B))&C")]
        [InlineData("|(|(A,B),C)", "(A|B)|C")]
        public void InFixFormulaTesting_OnCorrectPropositionFormula_CorrectInfixFormula(string prefixInput,string infixOutput)
        {
            //Arrange
            var formulaGenerator = new InfixFormulaGenerator();
            var binaryTree = ParsingModule.Parse(prefixInput);

            //Act
            formulaGenerator.Calculate(binaryTree.Root);

            //Assert
            Assert.Equal(infixOutput, binaryTree.Root.InFixFormula);
        }

        [Theory]
        [InlineData(">(!x.(@y.(P(x,y))),@q.(!p.(P(p,q))))", "(∃x[∀y[P(x,y)]])>(∀q[∃p[P(p,q)]])")]
        public void InFixFormulaTesting_OnPredicateFormula_CorrectInfixFormula(string prefixInput, string infixOutput)
        {
            //Arrange
            var formulaGenerator = new InfixFormulaGenerator();
            var binaryTree = ParsingModule.Parse(prefixInput);

            //Act
            formulaGenerator.Calculate(binaryTree.Root);

            //Assert
            Assert.Equal(infixOutput, binaryTree.Root.InFixFormula);
        }
    }
}
