using System.Collections.Generic;

namespace LPP.Modules
{
    public class RowComparer:IEqualityComparer<Row>
    {
        public bool Equals(Row x, Row y) => x.ToString().Equals(y.ToString());
        public int GetHashCode(Row obj) => obj.ToString().GetHashCode();
    }
}
