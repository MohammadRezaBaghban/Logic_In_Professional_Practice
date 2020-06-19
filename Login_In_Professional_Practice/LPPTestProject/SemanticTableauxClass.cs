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
        [InlineData(">(&(A,B),|(A,B))")]
        [InlineData(">(>(|(P,Q),R),|(>(P,R),>(Q,R))) ")]
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
        [InlineData(">(A,B)")]
        [InlineData("|(A,B)")]
        [InlineData("&(A,B)")]
        [InlineData(">(A,>(A,B))")]
        [InlineData(">(A,&(A,B))")]
        [InlineData(">(|(A,B),A)")]
        [InlineData(">(>(A,B),A)")]

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
