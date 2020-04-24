using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Composite_Pattern.Node
{
    class TrueFalse:SingleComponent
    {

        public TrueFalse(bool data)
        {
            Data = data;
            Symbol = (data) ? 'T' : 'F';
            NodeNumber = ++ParsingModule.nodeCounter;
        }

    }
}
