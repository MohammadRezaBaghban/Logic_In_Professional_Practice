using System.Collections.Generic;
using System.Linq;
using LPP.Truth_Table;

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
            _nonSimplifiedTruthTable = truthTable.NormalRows;
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
                var rowsWithSpecificNumberOfOnes = rowsWithTrueValue.Where(x => x.NumberOfOnes == i)
                                                                             .Select(x=>x.Clone())
                                                                             .Cast<Row>().ToList();
                if (rowsWithSpecificNumberOfOnes.Count != 0)
                    _groupedOnes.Add(rowsWithSpecificNumberOfOnes);
            }

            for (int i = 0; i <= _numberOfVariables; i++)
            {
                var rowsWithSpecificNumberOfZeros = rowsWithFalseValue.Where(x => x.NumberOfOnes == i)
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
            var lastStep1 = _simplificationStepsForOnes[_simplificationStepsForOnes.Count - 1];

            for (int i = 0; i < _numberOfVariables; i++)
            {
                Simplify(lastStep0, _simplificationStepsForZeros);
                Simplify(lastStep1, _simplificationStepsForOnes);
                lastStep0 = _simplificationStepsForZeros[_simplificationStepsForZeros.Count - 1];
                lastStep1 = _simplificationStepsForOnes[_simplificationStepsForOnes.Count - 1];
            }

            var rowComparer = new RowComparer();
            var trueValues = lastStep1.SelectMany(x=>x).Distinct(rowComparer);
            var falseValues = lastStep0.SelectMany(x => x).Distinct(rowComparer);
            
            return falseValues.Concat(trueValues).ToList();
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
                if(nextStep.Count!=0) stepList.Add(nextStep.ToList());
            }
            else
            {
                stepList.Add(list);
            }
        }
    }
}
