using LPP;
using LPP.Composite_Pattern.Components;
using Xunit;

namespace LPPTestProject
{
    [Collection("Serial")]
    public class SemanticTableauxClass
    {

        [Theory]
        [InlineData(">(P,P)")]
        [InlineData("|(P,~(P))")]
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
        [InlineData("&(|(A,~(A)), |(B,~(B)))")]
        [InlineData(">(|(P,Q),|(P,&(~(P),Q)))")]
        [InlineData(">(>(P,~(Q)),>(>(P,Q),~(P)))")]
        [InlineData(">(>(>(P,Q),~(P)),>(P,~(Q)))")]
        [InlineData(">(&(|(P,Q),&(>(P,R),>(Q,R))),R)")]
        [InlineData(">(>(|(P,Q),R),|(>(P,R),>(Q,R)))")]
        [InlineData("=(&(P,>(Q,R)),>(>(P,Q),&(P,R)))")]
        [InlineData(">(=(P,Q),|(&(P,Q),&(~(P),~(Q))))")]
        [InlineData(">(&(>(~(B),A),C),>(~(B),=(&(B,>(B,=(A,&(B,B)))),=(C,~(C)))))")]
        public void SemanticTableauxProposition_OnTautologiesFormula_RootBeingClosed(string prefixInput)
        {
            //Arrange
            var binaryTreeNormal = ParsingModule.Parse($"~({prefixInput.Trim()})");
            var tableauxRoot = new TableauxNode(binaryTreeNormal.Root as CompositeComponent);

            //Act
            tableauxRoot.IsClosed();

            //Assert
            Assert.Equal(true, tableauxRoot.LeafIsClosed);
        }

        [Theory]
        [InlineData("~(A)")]
        [InlineData("=(P,Q)")]
        [InlineData(">(A,B)")]
        [InlineData("|(A,B)")]
        [InlineData("&(A,B)")]
        [InlineData("&(P,~(P))")]
        [InlineData(">(P,%(P,Q))")]
        [InlineData(">(A,>(A,B))")]
        [InlineData(">(A,&(A,B))")]
        [InlineData(">(|(A,B),A)")]
        [InlineData(">(>(A,B),A)")]
        [InlineData("~(>(|(P,~(P)),P))")]
        [InlineData(">(&(P,~(Q)),~(P,Q))")]
        [InlineData(">(>(P,Q),|(P,>(Q,R)))")]
        [InlineData("&(&(>(A,A),|(B,~(B))),~(>(|(C,>(C,D)),|(&(A,C),D))))")]
        public void SemanticTableauxProposition_OnNonTatologiesFormula_RootNotBeingClosed(string prefixInput)
        {
            //Arrange
            var binaryTreeNormal = ParsingModule.Parse($"~({prefixInput.Trim()})");
            var tableauxRoot = new TableauxNode(binaryTreeNormal.Root as CompositeComponent);

            //Act
            tableauxRoot.IsClosed();

            //Assert
            Assert.Equal(false, tableauxRoot.LeafIsClosed);
        }

        [Theory]
        [InlineData(">(!x.(@y.(P(x,y))),@q.(!p.(P(p,q))))")]
        [InlineData("=(@x.(&(F(x),G(x))),&(@x.(F(x)),@x.(G(x))))")]
        [InlineData(">(!y.(!z.(@x.(&(>(F(x),G(y)),>(G(z),F(x)))))),@x.(!y.(>(F(x),G(y)))))")]
        [InlineData("&(|(~(@x.(&(F(x), G(x)))),&(@x.(F(x)), @x.(G(x)))),|(@x.(&(F(x),G(x))),~(&(@x.(F(x)),@x.(G(x))))))")]
        public void SemanticTableauxPredicate_OnTautologiesFormula_RootBeingClosed(string prefixInput)
        {
            //Arrange
            var binaryTreeNormal = ParsingModule.Parse($"~({prefixInput.Trim()})");
            var tableauxRoot = new TableauxNode(binaryTreeNormal.Root as CompositeComponent);

            //Act
            tableauxRoot.IsClosed();

            //Assert
            Assert.Equal(true, tableauxRoot.LeafIsClosed);
        }


        [Theory]
        [InlineData("@x.(!y.(P(x,y)))")]
        [InlineData("!x.(>(P(x,b,c,x),C(a,x,d)))")]
        [InlineData(">(!x.(&(P(x,y),Z(g,h))),~(!q.(Q(q,y,p,q))))")]
        [InlineData("&(>(~(@x.(&(P(x,y),Z(g,h)))),~(!q.(Q(q,y,p,q)))),~(!x.(P(x,y)))")]
        [InlineData("&(>(~(@x.(&(P(x,y),Z(g,h)))),&(~(!q.(Q(q,y,p,q))),~(!p.(T(p,m))))),~(!x.(P(x,y))))")]
        public void SemanticTableauxPredicate_OnNonTatologiesFormula_RootNotBeingClosed(string prefixInput)
        {
            //Arrange
            var binaryTreeNormal = ParsingModule.Parse($"~({prefixInput.Trim()})");
            var tableauxRoot = new TableauxNode(binaryTreeNormal.Root as CompositeComponent);

            //Act
            tableauxRoot.IsClosed();

            //Assert
            Assert.Equal(false, tableauxRoot.LeafIsClosed);
        }
    }
}
