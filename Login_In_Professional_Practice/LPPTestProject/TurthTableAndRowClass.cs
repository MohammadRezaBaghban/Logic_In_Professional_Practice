using LPP.Modules;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class TruthTableAndRowClass
    {

        [Theory]
        [InlineData("|(~(>(A,B)),&(A,>(C,B)))","F0")]
        [InlineData("&(|(A,~(B)),C)", "A2")]
        [InlineData("|(&(A,B),C)", "EA")]
        [InlineData("&(A,|(B,C))","E0")]
        [InlineData("&(>(P,Q),>(Q,P))","9")]
        //[InlineData("~(>(~(A),|(A,B)))", "7")]
        [InlineData("~(|(~(A),|(>(A,~(A)),>(&(>(~(|(C,A)),C),C),C))))", "0")]
        [InlineData("|(|(A,B),C)","FE")]
        [InlineData("&(P,Q)", "8")]
        [InlineData("|(P,Q)", "E")]
        [InlineData(">(A,B)", "B")]
        [InlineData("=(A,B)", "9")]

        [InlineData("~(A)", "1")]

        public void TruthTable_CalculateTruthTable_HashCodeBeEqualAsExpected(string prefixInput, string hexaHashCode)
        {
            //Arrange
            var calculator = new Calculator();
            var rootOfComponent = ParsingModule.ParseInput(prefixInput);

            //Act
            calculator.Calculate(rootOfComponent);
            var truthTable = new TruthTable(rootOfComponent, calculator);

            //Assert
            Assert.Equal(truthTable.GetHexadecimalHashCode(), hexaHashCode);
        }

        [Theory]
        [InlineData("|(|(A,B),C)", " A  B  C   v\n 0  0  0   0\n *  *  1   1\n *  1  *   1\n 1  *  *   1")]
        public void TruthTable_SimplifyTruthTable_HashCodeBeEqualAsExpected(string prefixInput, string simplifiedTruthTable)
        {
            //Arrange
            var calculator = new Calculator();
            var rootOfComponent = ParsingModule.ParseInput(prefixInput);

            //Act
            calculator.Calculate(rootOfComponent);
            var truthTable = new TruthTable(rootOfComponent, calculator);
            truthTable.SimplifyRows();

            //Assert
            var actualSimplified = truthTable.SimplifiedToString().Replace("\n","").Replace(" ","");
            var expectedSimplified = simplifiedTruthTable.Replace("\n", "").Replace(" ", "");
            Assert.Equal(actualSimplified, expectedSimplified);

        }
    }
}
