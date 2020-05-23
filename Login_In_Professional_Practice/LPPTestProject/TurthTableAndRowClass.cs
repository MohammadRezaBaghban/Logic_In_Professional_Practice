using LPP;
using LPP.Modules;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class TruthTableAndRowClass
    {

        [Theory]
        [InlineData("&(|(A,~(B)),C)", "10100010", "A2")]
        [InlineData("|(&(A,B),C)", "11101010", "EA")]
        [InlineData("&(A,|(B,C))", "11100000", "E0")]
        [InlineData("&(>(P,Q),>(Q,P))", "1001", "9")]
        [InlineData("&(P,Q)", "1000", "8")]
        [InlineData("|(P,Q)", "1110", "E")]
        public void Calculator_TruthTable_HashCodeBeEqualAsExpected(string prefixInput, string intHashCode, string hexaHashCode)
        {
            //Arrange
            var calculator = new Calculator();
            var rootOfComponent = ParsingModule.ParseInput(prefixInput);

            //Act
            calculator.Calculate(rootOfComponent);
            var truthTable = new TruthTable(rootOfComponent, calculator);

            //Assert
            Assert.Equal(truthTable.GetHashCode().ToString(), intHashCode);
            Assert.Equal(truthTable.GetHexadecimalHashCode(), hexaHashCode);
        }

    }
}
