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
        [InlineData("|(A,|(B,C))", "¬A⋀¬B⋀C⋁¬A⋀B⋀¬C⋁¬A⋀B⋀C⋁A⋀¬B⋀¬C⋁A⋀¬B⋀C⋁A⋀B⋀¬C⋁A⋀B⋀C", "C⋁B⋁A")]
        [InlineData("&(A,|(B,C))", "(A⋀(¬B))⋀C ⋁ (A⋀B)⋀(¬C) ⋁ (A⋀B)⋀C", "A⋀C⋁A⋀B")]
        [InlineData(">(A,>(B,A))", "¬A⋀¬B⋁¬A⋀B⋁A⋀¬B⋁A⋀B", "")]
        [InlineData("&(>(P,Q),|(Q,P))", "¬P⋀Q⋁P⋀Q", "Q")]
        [InlineData("|(P,Q)", "¬P⋀Q⋁P⋀¬Q⋁P⋀Q", "Q⋁P")]
        [InlineData("&(&(A,B),~(A))", "", "")]
        public void TruthTable_DNFProcessing_DNFFormulaAndHashCodeBeAsExpected(string prefixInput,string normalDNF, string simplifiedDNF)
        {
            //Arrange
            var binaryTree = ParsingModule.ParseInput(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);
            truthTable.ProcessDNF();

            normalDNF = DeleteCharacters(normalDNF, new string[] { ")", "(", " ", });
            simplifiedDNF = DeleteCharacters(simplifiedDNF, new string[] { ")", "(", " ", });
            var actualNormalDNF = DeleteCharacters(DNF.DNFFormula(truthTable.DNF_Normal_Components), new string[] { ")", "(", " ", });
            var actualSimplifiedDNF = DeleteCharacters(DNF.DNFFormula(truthTable.DNF_Simplified_Components), new string[] { ")", "(", " ", });

            //Assert
            Assert.Equal(actualNormalDNF, normalDNF);
            Assert.Equal( actualSimplifiedDNF, simplifiedDNF);
            if (normalDNF != "")
            {
                var truthTableDNF = new TruthTable(truthTable.DNF_Normal_BinaryTree);
                var n1 = truthTableDNF.GetHexadecimalHashCode();
                var n2 = truthTable.GetHexadecimalHashCode();
                Assert.Equal(n1,n2);
            }

            //Todo Refactor the binarytree creation such that it would be able to be parsed again with propositional variables
        }

        [Theory]
        [InlineData("|(A,B))")]
        [InlineData("|(~(A),~(B))")]
        [InlineData("~(|(|(A,~(B)),|(~(C),D))))")]
        [InlineData("|(|(A,B),|(C,|(F,G)))")]
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
