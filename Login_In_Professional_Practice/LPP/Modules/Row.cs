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
            var indexOfDifference = 0;
            for (var i = 0; i < PropositionValues.Length && numberOfDifference < 2; i++)
            {
                if (this.PropositionValues[i] != other.PropositionValues[i])
                {
                    if(this.PropositionValues[i] !=null && other.PropositionValues[i] != null)
                    {
                        indexOfDifference = i;
                        numberOfDifference++;
                    }
                }
            }
            return numberOfDifference < 2 ? indexOfDifference : -1;
        }

        public static void Simplify(Row row1, Row row2)
        {
            var indexOfDifference = row1.CompareTo(row2);
            if (indexOfDifference != -1)
            {
                row1.PropositionValues[indexOfDifference] = null;
                row2 = null;
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
