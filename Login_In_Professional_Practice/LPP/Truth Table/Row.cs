﻿using System;
using System.Linq;

namespace LPP.Modules
{
    public class Row : ICloneable
    {
        public readonly bool?[] PropositionValues;
        public bool? Result { get; private set; }

        public int NumberOfOnes => PropositionValues.Count(x => x != null && x.Value);

        public Row(int numberOfVariables) => PropositionValues = new bool?[numberOfVariables];

        public void SetValue(bool input)
        {
            if (Result == null) Result = input;
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
