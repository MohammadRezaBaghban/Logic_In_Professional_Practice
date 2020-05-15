using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern.Node;
using LPP.NodeComponents;

namespace LPP.Modules
{
    public class Row
    {
        public bool?[] PropositionValues;
        public bool? Result { get; private set; } = null;

        public Row(int numberOfVariables) => PropositionValues = new bool?[numberOfVariables];

        public void SetValue(bool input)
        {
            if (Result == null)
            {
                Result = input;
            }
        }

        public override string ToString() => PropositionValues
                                                 .Select(value => value == true ? "1" : "0")
                                                 .Aggregate("", (current, v) => current + $" {v} ")
                                                  + $" {( Result != null && (bool) Result ? " 1": " 0")}";
    }
}
