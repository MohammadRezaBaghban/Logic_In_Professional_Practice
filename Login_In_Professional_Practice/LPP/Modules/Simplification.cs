﻿using System.Collections.Generic;
using System.Linq;

namespace LPP.Modules
{
    /// <summary>
    /// A class for for simplifying truth table using Quine-McCluskey Minimization algorithm
    /// </summary>
    public class Simplification
    {
        //Fields
        private readonly int _numberOfVariables;
        private readonly Row[] _nonSimplifiedTruthTable;
        private readonly List<List<Row>> _groupedZeros;
        private readonly List<List<Row>> _groupedOnes;
        private readonly List<List<List<Row>>> _simplificationStepsForOnes;
        private readonly List<List<List<Row>>> _simplificationStepsForZeros;

        //Constructor
        public Simplification(TruthTable truthTable)
        {
            _nonSimplifiedTruthTable = truthTable.Rows;
            _groupedOnes = new List<List<Row>>();
            _groupedZeros = new List<List<Row>>();
            _simplificationStepsForOnes = new List<List<List<Row>>>();
            _simplificationStepsForZeros = new List<List<List<Row>>>();
            _numberOfVariables = truthTable.NumberOfVariables;
        }

        //Methods & Functions
        private void OrderTheRows(Row[] truthTable)

        {
            var rowsWithTrueValue = truthTable.Where(x => x.Result == true).ToList();
            var rowsWithFalseValue = truthTable.Where(x => x.Result == false).ToList();


            for (int i = 0; i <= _numberOfVariables; i++)
            {
                var rowsWithSpecificNumberOfOnes = rowsWithTrueValue.Where(x => x.NumberOFOnes == i)
                                                                             .Select(x=>x.Clone())
                                                                             .Cast<Row>().ToList();
                if (rowsWithSpecificNumberOfOnes.Count != 0)
                    _groupedOnes.Add(rowsWithSpecificNumberOfOnes);
            }

            for (int i = 0; i <= _numberOfVariables; i++)
            {
                var rowsWithSpecificNumberOfZeros = rowsWithFalseValue.Where(x => x.NumberOFOnes == i)
                                                                              .Select(x => x.Clone())
                                                                              .Cast<Row>().ToList();
                if (rowsWithSpecificNumberOfZeros.Count!=0)
                    _groupedZeros.Add(rowsWithSpecificNumberOfZeros);
            }

            _simplificationStepsForOnes.Add(_groupedOnes);
            _simplificationStepsForZeros.Add(_groupedZeros);
        }

        private int MatchPair(Row row1, Row row2)
        {
            if (row1 == null || row2 == null ||
                row1.Result != row2.Result ||
                row1.PropositionValues.Length != row2.PropositionValues.Length)
            {
                return -1;
            }

            var numberOfDifference = 0;
            var indexOfDifference = -1;
            
            for (var i = 0; i < _numberOfVariables && numberOfDifference < 2; i++)
            {
                if (row1.PropositionValues[i] != row2.PropositionValues[i])
                {
                    indexOfDifference = i;
                    numberOfDifference++;
                }
            }
            return (numberOfDifference==1)? indexOfDifference : -1;
        }

        public List<Row> RecursiveSimplification()
        {
            OrderTheRows(_nonSimplifiedTruthTable);

            var lastStep0 = _simplificationStepsForZeros[_simplificationStepsForZeros.Count - 1];
            while (lastStep0.Count > 1)
            {
                Simplify(lastStep0, _simplificationStepsForZeros);
            }

            var lastStep1 = _simplificationStepsForOnes[_simplificationStepsForOnes.Count - 1];
            while (lastStep1.Count > 1)
            {
                Simplify(lastStep1, _simplificationStepsForOnes);
                lastStep1 = _simplificationStepsForOnes[_simplificationStepsForOnes.Count - 1];
            }

            var rowComparer = new RowComparer();
            var trueValues = lastStep1[0].Distinct(rowComparer).ToList();
            var falseValues = lastStep0[0].Distinct(rowComparer).ToList();

            var simplifiedRows = Enumerable.Empty<Row>().ToList();
            simplifiedRows.AddRange(falseValues);
            simplifiedRows.AddRange(trueValues);
            return simplifiedRows;

        }

        private void Simplify(List<List<Row>> list, ICollection<List<List<Row>>> stepList)
        {
            var nextStep = new List<List<Row>>();
            
            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    var nextList = new List<Row>();
                    foreach (var row in list[i])
                    {
                        foreach (var nextRow in list[i + 1])
                        {
                            var index = MatchPair(row, nextRow);
                            if (index != -1)
                            {
                                var clonedRow = (Row)row.Clone();
                                clonedRow.PropositionValues[index] = null;
                                nextList.Add(clonedRow);
                            }
                        }
                    }
                    if (nextList.Count != 0) nextStep.Add(nextList);
                }
                stepList.Add(nextStep.ToList());
            }
            else
            {
                stepList.Add(list);
            }
        }
    }
}
