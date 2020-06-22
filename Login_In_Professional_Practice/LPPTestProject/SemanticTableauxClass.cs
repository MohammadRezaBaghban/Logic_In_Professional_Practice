using LPP;
using LPP.Composite_Pattern.Components;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class SemanticTableauxClass
    {

        [Theory]
        [InlineData(">(P,P)")]
        [InlineData("|(>(P,Q),P)")]
        [InlineData(">(~(>(P,Q)),P)")]
        [InlineData(">(>(~(P),P),P)")]
        [InlineData(">(&(A,B),|(A,B))")]
        [InlineData(">(~(Q),~(&(P,Q)))")]
        [InlineData(">(&(P,Q),>(~(P),R))")]
        [InlineData(">(>(P,Q),|(Q,>(P,R)))")]
        [InlineData(">(~(P),~(>(>(P,Q),P)))")]
        [InlineData(">(~(&(P,Q)),>(P,~(Q)))")]
        [InlineData(">(=(P,Q),=(~(P),~(Q)))")]
        [InlineData(">(|(P,Q),|(P,&(~(P),Q)))")]
        [InlineData(">(>(P,~(Q)),>(>(P,Q),~(P)))")]
        [InlineData(">(>(>(P,Q),~(P)),>(P,~(Q)))")]
        [InlineData(">(&(|(P,Q),&(>(P,R),>(Q,R))),R)")]
        [InlineData(">(>(|(P,Q),R),|(>(P,R),>(Q,R)))")]
        [InlineData("=(&(P,>(Q,R)),>(>(P,Q),&(P,R)))")]
        [InlineData(">(=(P,Q),|(&(P,Q),&(~(P),~(Q))))")]
        public void SemanticTableauxTesting_OnTautologiesFormula_RootBeingClosed(string prefixInput)
        {
            //Arrange
            string userInput = $"~({prefixInput.Trim()})";
            var binaryTreeNormal = ParsingModule.ParseInput(userInput);
            var rootOfNormalBinaryTree = binaryTreeNormal.Root as CompositeComponent;
            var tableauxRoot = new TableauxNode(rootOfNormalBinaryTree);

            //Act
            tableauxRoot.IsClosed();

            //Assert
            Assert.Equal(true, tableauxRoot.Closed);
        }

        [Theory]
        [InlineData("~(A)")]
        [InlineData("=(P,Q)")]
        [InlineData(">(A,B)")]
        [InlineData("|(A,B)")]
        [InlineData("&(A,B)")]
        [InlineData(">(A,>(A,B))")]
        [InlineData(">(A,&(A,B))")]
        [InlineData(">(|(A,B),A)")]
        [InlineData(">(>(A,B),A)")]
        [InlineData(">(&(P,~(Q)),~(P,Q))")]
        [InlineData(">(>(P,Q),|(P,>(Q,R)))")]
        public void SemanticTableauxTesting_OnNonTatologiesFormula_RootNotBeingClosed(string prefixInput)
        {
            //Arrange
            string userInput = $"~({prefixInput.Trim()})";
            var binaryTreeNormal = ParsingModule.ParseInput(userInput);
            var rootOfNormalBinaryTree = binaryTreeNormal.Root as CompositeComponent;
            var tableauxRoot = new TableauxNode(rootOfNormalBinaryTree);

            //Act
            tableauxRoot.IsClosed();

            //Assert
            Assert.Equal(false, tableauxRoot.Closed);
        }

    }
}
