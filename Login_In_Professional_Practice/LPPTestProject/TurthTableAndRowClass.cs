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
        [InlineData("|(&(A,B),C)", "EA")]
        [InlineData("&(A,|(B,C))","E0")]
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
            var truthTable = new TruthTable(rootOfComponent as CompositeComponent);

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
            var truthTable = new TruthTable(rootOfComponent as CompositeComponent);

            //Assert
            var actualSimplified = DeleteCharacters(truthTable.SimplifiedToString(),new string[]{"\n"," ",});
            var expectedSimplified = DeleteCharacters(simplifiedTruthTable, new string[] { "\n", " ", });
            Assert.Equal(actualSimplified, expectedSimplified);
        }

        [Theory]
        [InlineData(">(A,B)", "(¬A ⋀ ¬B) ⋁ (¬A ⋀ B) ⋁ (A ⋀ B)", "A⋁B")]

        public void TruthTable_DNFProcessing_DNFFormulaBeAsExpected(string prefixInput,string normalDNF, string simplifiedDNF)
        {
            //Arrange
            var calculator = new Calculator();
            var rootOfComponent = ParsingModule.ParseInput(prefixInput);

            //Act
            calculator.Calculate(rootOfComponent);
            var truthTable = new TruthTable(rootOfComponent as CompositeComponent);

            //Assert
            normalDNF = DeleteCharacters(normalDNF, new string[] { ")", "(", " ", });
            simplifiedDNF = DeleteCharacters(simplifiedDNF, new string[] {")", "(", " ",});
            var actualNormalDNF = DeleteCharacters(DNF.DNFFormula(truthTable.DNF_Normal_Components), new string[] { ")", "(", " ", });
            var actualSimplifiedDNF = DeleteCharacters(DNF.DNFFormula(truthTable.DNF_Simplified_Components), new string[] { ")", "(", " ", });

            Assert.Equal(normalDNF, actualNormalDNF);
            Assert.Equal(simplifiedDNF, actualSimplifiedDNF);

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
