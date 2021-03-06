﻿using LPP;
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
            var binaryTree = ParsingModule.Parse(prefixInput);

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
            var binaryTree = ParsingModule.Parse(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);

            //Assert
            var actualSimplified = DeleteCharacters(truthTable.SimplifiedToString(),new[]{"\n"," ",});
            var expectedSimplified = DeleteCharacters(simplifiedTruthTable, new[] { "\n", " ", });
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
            var binaryTree = ParsingModule.Parse(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);
            truthTable.ProcessDnf();
            if (truthTable.DnfNormalComponents.Count == 0) return;
            var truthTableDnf = new TruthTable(truthTable.DnfNormalBinaryTree);

            //Assert
            Assert.Equal(truthTableDnf.GetHexadecimalHashCode(), truthTable.GetHexadecimalHashCode());
            Assert.Equal(truthTableDnf.GetHexadecimalSimplifiedHashCode(), truthTable.GetHexadecimalSimplifiedHashCode());
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
        [InlineData(">(&(|(P,Q),&(>(P,R),>(Q,R))),R)")]
        [InlineData(">(>(|(P,Q),R),|(>(P,R),>(Q,R)))")]
        public void TruthTable_Nandify_NandTruthTableHashCodeBeAsExpected(string prefixInput)
        {
            //Arrange
            var nandify = new Nandify();
            var binaryTree = ParsingModule.Parse(prefixInput);

            //Act
            var truthTable = new TruthTable(binaryTree);
            nandify.Calculate(binaryTree.Root);
            var truthTableNand = new TruthTable(Nandify.BinaryTree);

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
