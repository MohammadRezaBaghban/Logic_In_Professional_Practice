﻿using LPP;
using LPP.Modules;
using LPP.Visitor_Pattern;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class FormulaParserClass
    {
        
        [Theory]
        [InlineData(">(&(|(~(A),D),=(A,E)),>(&(C,~(D)),B))", "(((¬A)⋁D)⋀(A⇔E))⇒((C⋀(¬D))⇒B)")]
        [InlineData(">(&(>(Q,R),>(~(Q),P)),|(P,R))", "((Q⇒R)⋀((¬Q)⇒P))⇒(P⋁R)")]
        [InlineData(">(|(A,>(B,C)),=(D,&(A,E))", "(A⋁(B⇒C))⇒(D⇔(A⋀E))")]
        [InlineData(">(>(P,Q),>(&(R,1),Q))", "(P⇒Q)⇒((R⋀True)⇒Q)")]
        [InlineData(">(=(P,Q),&(R,~(P)))", "(P⇔Q)⇒(R⋀(¬P))")]
        [InlineData(">(A,=(B,&(0,C))", "A⇒(B⇔(False⋀C))")]
        [InlineData(">(&(A,B),~(C))", "(A⋀B)⇒(¬C)")]
        [InlineData("&(|(A,~(B)),C)", "(A⋁(¬B))⋀C")]

        public void InFixFormulaTesting_OnCorrectPrefixFormula_CorrectInfixFormula(string prefixInput,string infixOutput)
        {
            //Arrange
            var formulaGenerator = new InfixFormulaGenerator();
            var rootOfComponent = ParsingModule.ParseInput(prefixInput);

            //Act
            formulaGenerator.Calculate(rootOfComponent);

            //Assert
            Assert.Equal(rootOfComponent.InFixFormula,infixOutput);
        }
    }
}
