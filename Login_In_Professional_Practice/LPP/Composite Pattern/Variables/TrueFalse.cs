using LPP.Composite_Pattern.Components;
using LPP.Modules;

namespace LPP.Composite_Pattern.Variables
{
    class TrueFalse:SingleComponent
    {
        public TrueFalse(bool data)
        {
            Data = data;
            Symbol = (data) ? '1' : '0';
            InFixFormula = (data) ? "True" : "False";
            NodeNumber = ++ParsingModule.NodeCounter;
        }
    }
}
