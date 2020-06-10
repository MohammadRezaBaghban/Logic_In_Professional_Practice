using LPP;
using LPP.Modules;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class CalculatorClass
    {

        [Theory]
        [InlineData(">(1,0)", false)]
        [InlineData(">(0,0)", true)]
        [InlineData(">(0,1)", true)]
        [InlineData(">(1,1)", true)]
        public void Calculator_Implication_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;
            
            //Act
            calculator.Calculate(rootOfComponent);
`
            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }

        [Theory]
        [InlineData("=(1,0)", false)]
        [InlineData("=(0,0)", true)]
        [InlineData("=(0,1)", false)]
        [InlineData("=(1,1)", true)]
        public void Calculator_BiImplication_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;

            //Act
            calculator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }

        [Theory]
        [InlineData("&(1,0)", false)]
        [InlineData("&(0,0)", false)]
        [InlineData("&(0,1)", false)]
        [InlineData("&(1,1)", true)]
        public void Calculator_Conjunction_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;

            //Act
            calculator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }


        [Theory]
        [InlineData("%(1,0)", true)]
        [InlineData("%(0,0)", true)]
        [InlineData("%(0,1)", true)]
        [InlineData("%(1,1)", false)]
        public void Calculator_NAND_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;

            //Act
            calculator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }

        [Theory]
        [InlineData("|(1,0)", true)]
        [InlineData("|(0,0)", false)]
        [InlineData("|(0,1)", true)]
        [InlineData("|(1,1)", true)]
        public void Calculator_Disjunction_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;

            //Act
            calculator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }

        [Theory]
        [InlineData("~(|(1,0))", false)]
        [InlineData("~(|(0,0))", true)]
        public void Calculator_Negation_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;

            //Act
            calculator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }


        [Theory]
        [InlineData("~(=(|(&(1,0),>(1,0)),0))", false)]
        [InlineData("=(|(&(1,0),>(1,0)),0)", true)]
        [InlineData("~(=(|(&(1,0),>(0,1)),0))", true)]
        public void Calculator_Mixture_ReturnCorrectValue(string prefixInput, bool result)
        {
            //Arrange
            var calculator = new Calculator();
            var binaryTree = ParsingModule.ParseInput(prefixInput);
            var rootOfComponent = binaryTree.Root;

            //Act
            calculator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.Data, result);
        }
    }
}