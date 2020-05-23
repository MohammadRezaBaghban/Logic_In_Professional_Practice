using System;
using System.Collections.Generic;
using System.Linq;

namespace LPP.Modules
{
    public class Row : IComparable<Row>, ICloneable
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

        public int CompareTo(Row other)
        {
            if (other == null || other.Result != this.Result ||
                other.PropositionValues.Length != this.PropositionValues.Length)
            {
                return -1;
            }

            var numberOfDifference = 0;
            var indexOfNullDifference = -1;
            var indexOfDifference = -1;
            for (var i = 0; i < PropositionValues.Length && numberOfDifference < 2; i++)
            {
                if (this.PropositionValues[i] != other.PropositionValues[i])
                {
                    if(this.PropositionValues[i] ==null || other.PropositionValues[i] == null)
                    {
                        indexOfNullDifference = i;
                    }
                    else
                    {
                        if (this.PropositionValues[i] != null && other.PropositionValues[i] != null)
                        {
                            indexOfDifference = i;
                            numberOfDifference++;
                        }
                    }
                }
            }

            if (numberOfDifference < 2)
            {
                return indexOfNullDifference != -1 ? Math.Min(indexOfNullDifference, indexOfDifference) : indexOfDifference;
            }
            else
            {
                return -1;
            }
            
        }

        public override string ToString() => PropositionValues
                                                 .Select(value => value == null ? "*" : value == true ? "1" : "0")
                                                 .Aggregate("", (current, v) => current + $" {v} ")
                                             + $" {(Result != null && (bool)Result ? " 1" : " 0")}";

        public object Clone()
        {
            var newRow = new Row(this.PropositionValues.Length);
            this.PropositionValues.CopyTo(newRow.PropositionValues, 0);
            newRow.Result = this.Result;
            return newRow;
        }

        
    }
}
