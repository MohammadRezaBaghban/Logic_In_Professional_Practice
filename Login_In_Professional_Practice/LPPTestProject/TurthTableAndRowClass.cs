using LPP;
using LPP.Composite_Pattern;
using LPP.Modules;
using LPP.Truth_Table;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class TruthTableAndRowClass
    {

        [Theory]
        [InlineData("~(|(~(A),|(>(A,~(A)),>(&(>(~(|(C,A)),C),C),C))))", "0")]
        [InlineData("|(~(>(A,B)),&(A,>(C,B)))","F0")]
        [InlineData("&(>(P,Q),>(Q,P))","9")]
        [InlineData("&(|(A,~(B)),C)", "A2")]
        [InlineData("&(&(A,B),~(A))", "0")]
        [InlineData("|(&(A,B),C)", "EA")]
        [InlineData("&(A,|(B,C))","E0")]
        [InlineData("|(|(A,B),C)","FE")]
        [InlineData(">(A,>(B,A))", "F")]
        [InlineData("&(P,Q)", "8")]
        [InlineData("|(P,Q)", "E")]
        [InlineData(">(A,B)", "B")]
        [InlineData("=(A,B)", "9")]
        [InlineData("~(A)", "1")]
        public void TruthTable_CalculateTruthTable_HashCodeBeEqualAsExpected(string prefixInput, string hexaHashCode)
        {
            //Arrange
            var binaryTree = ParsingModule.ParseInput(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);

            //Assert
            Assert.Equal(truthTable.GetHexadecimalHashCode(), hexaHashCode);
        }

        [Theory]
        [InlineData("|(|(A,B),C)", " A  B  C   v\n 0  0  0   0\n *  *  1   1\n *  1  *   1\n 1  *  *   1")]
        public void TruthTable_SimplifyTruthTable_HashCodeBeEqualAsExpected(string prefixInput, string simplifiedTruthTable)
        {
            //Arrange
            var binaryTree = ParsingModule.ParseInput(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);

            //Assert
            var actualSimplified = DeleteCharacters(truthTable.SimplifiedToString(),new string[]{"\n"," ",});
            var expectedSimplified = DeleteCharacters(simplifiedTruthTable, new string[] { "\n", " ", });
            Assert.Equal(actualSimplified, expectedSimplified);
        }

        [Theory]
        [InlineData("~(|(~(A),|(>(A,~(A)),>(&(>(~(|(C,A)),C),C),C))))")]
        [InlineData(">(>(|(P,Q),R),|(>(P,R),>(Q,R)))")]
        [InlineData("|(~(>(A,B)),&(A,>(C,B)))")]
        [InlineData("&(>(P,Q),|(Q,P))")]
        [InlineData("&(>(P,Q),>(Q,P))")]
        [InlineData("&(|(A,~(B)),C)")]
        [InlineData("&(&(A,B),~(A))")]
        [InlineData("|(&(A,B),C)")]
        [InlineData("&(A,|(B,C))")]
        [InlineData("|(|(A,B),C)")]
        [InlineData(">(A,>(B,A))")]
        [InlineData("|(A,|(B,C))")]
        [InlineData("&(P,Q)")]
        [InlineData("|(P,Q)")]
        [InlineData(">(A,B)")]
        [InlineData("=(A,B)")]
        public void TruthTable_DNFProcessing_DNFFormulaAndHashCodeBeAsExpected(string prefixInput)
        {
            //Arrange
            var binaryTree = ParsingModule.ParseInput(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);
            truthTable.ProcessDNF();
            if (truthTable.DNF_Normal_Components.Count != 0)
            {
                var truthTableDNF = new TruthTable(truthTable.DNF_Normal_BinaryTree);

                //Assert
                Assert.Equal(truthTableDNF.GetHexadecimalHashCode(), truthTable.GetHexadecimalHashCode());
                Assert.Equal(truthTableDNF.GetHexadecimalSimplifiedHashCode(), truthTable.GetHexadecimalSimplifiedHashCode());
            }
        }

        [Theory]
        [InlineData("|(A,B))")]
        [InlineData("&(A,B))")]
        [InlineData(">(A,B))")]
        [InlineData("=(A,B))")]
        [InlineData("|(~(A),~(B))")]
        [InlineData("|(|(A,B),|(C,|(F,G)))")]
        [InlineData(">(&(A,~(D)),|(B,|(Y,R)))")]
        [InlineData("~(|(|(A,~(B)),|(~(C),D))))")]
        [InlineData(">(>(|(P,Q),R),|(>(P,R),>(Q,R)))")]
        public void TruthTable_Nandify_NandTruthTableHashCodeBeAsExpected(string prefixInput)
        {
            //Arrange
            var nandify = new Nandify();
            var binaryTree = ParsingModule.ParseInput(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);
            nandify.Calculate(binaryTree.Root);
            var truthTableNand = new TruthTable(Nandify.binaryTree);

            //Assert
            Assert.Equal(truthTable.GetHexadecimalHashCode(),truthTableNand.GetHexadecimalHashCode());
        }

        private string DeleteCharacters(string src, string[] chars)
        {
            string result = src;
            foreach (var c in chars)
            {
                result = result.Replace(c, "");
            }

            return result;
        }
    }
}
