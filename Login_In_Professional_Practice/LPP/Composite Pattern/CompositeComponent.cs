using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern;

namespace LPP.NodeComponents
{
    public class CompositeComponent : Component
    {

        public override string GraphVizFormula
        {
            get
            {
                string temp = "";
                temp += $"node{NodeNumber} [ label = \"{Symbol}\" ]";
                if (LeftNode != null)
                {
                    temp += $"\nnode{NodeNumber} -- node{LeftNode.NodeNumber}\n";
                    temp += LeftNode.GraphVizFormula;
                }
                if (RightNode != null)
                {
                    temp += $"\nnode{NodeNumber} -- node{RightNode.NodeNumber}\n";
                    temp += RightNode.GraphVizFormula;
                }
                return temp;
            }
        }

        public override void Evaluate(Composite_Pattern.IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override string ToString() =>
            $"Object Type: {this.GetType().Name}"
            + $" | Data: {this.Data.ToString()}"
            + $" | Parent: {(this.Parent?.GetType().Name) ?? "Null"}"
            + $" | RightNode: {RightNode.GetType().Name}"
            + $" | LeftNode: {LeftNode.GetType().Name}";

    }

    public interface IVisitor
    {
    }
}
