using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPP.Modules
{
    /// <summary>
    /// A class for for simplifying truth table using Quine-McCluskey Minimization algorithm
    /// </summary>
    public class Simplification
    {
        //Fields
        private readonly int numberOfVariables;
        private Row[] nonSimplifiedTruthTable;
        private List<List<Row>> groupedZeros;
        private List<List<Row>> groupedOnes;
        private List<List<List<Row>>> simplificationStepsForOnes;
        private List<List<List<Row>>> simplificationStepsForZeros;

        //Constructor
        public Simplification(Row[] truthTable)
        {
            nonSimplifiedTruthTable = truthTable;
            groupedOnes = new List<List<Row>>();
            groupedZeros = new List<List<Row>>();
            simplificationStepsForOnes = new List<List<List<Row>>>();
            simplificationStepsForZeros = new List<List<List<Row>>>();
            numberOfVariables = truthTable[0].PropositionValues.Length;
            RecursiveSimplification();
        }

        //Methods & Functions
        private void OrderTheRows(Row[] truthTable)

        {
            var RowsWithTrueValue = truthTable.Where(x => x.Result == true).ToList();
            var RowsWithFalseValue = truthTable.Where(x => x.Result == false).ToList();


            for (int i = 0; i <= numberOfVariables; i++)
            {
                var RowsWithSpecificNumberOfOnes = RowsWithTrueValue.Where(x => x.NumberOFOnes == i)
                                                                             .Select(x=>x.Clone())
                                                                             .Cast<Row>().ToList();
                if (RowsWithSpecificNumberOfOnes.Count != 0)
                    groupedOnes.Add(RowsWithSpecificNumberOfOnes);
            }

            for (int i = 0; i <= numberOfVariables; i++)
            {
                var RowsWithSpecificNumberOfZeros = RowsWithFalseValue.Where(x => x.NumberOFOnes == i)
                                                                              .Select(x => x.Clone())
                                                                              .Cast<Row>().ToList();
                if (RowsWithSpecificNumberOfZeros.Count!=0)
                    groupedZeros.Add(RowsWithSpecificNumberOfZeros);
            }

            simplificationStepsForOnes.Add(groupedOnes);
            simplificationStepsForZeros.Add(groupedZeros);
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
            
            for (var i = 0; i < numberOfVariables && numberOfDifference < 2; i++)
            {
                if (row1.PropositionValues[i] != row2.PropositionValues[i])
                {
                    indexOfDifference = i;
                    numberOfDifference++;
                }
            }
            return (numberOfDifference==1)? indexOfDifference : -1;
        }

        private void RecursiveSimplification()
        {
            OrderTheRows(nonSimplifiedTruthTable);

            var lastStep0 = simplificationStepsForZeros[simplificationStepsForZeros.Count - 1];
            while (lastStep0.Count > 1)
            {
                Simplify(lastStep0, simplificationStepsForZeros);
            }

            var lastStep1 = simplificationStepsForOnes[simplificationStepsForOnes.Count - 1];
            while (lastStep1.Count > 1)
            {
                Simplify(lastStep1, simplificationStepsForOnes);
                lastStep1 = simplificationStepsForOnes[simplificationStepsForOnes.Count - 1];
            }

            
        }
        
        private void Simplify(List<List<Row>> list, List<List<List<Row>>> stepList)
        {
            var nextStep = new List<List<Row>>();
            
            if (list.Count > 1)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    var nextList = new List<Row>();
                    foreach (var row in list[i])
                    {
                        foreach (var NextRow in list[i + 1])
                        {
                            var index = MatchPair(row, NextRow);
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
                stepList.Add(nextStep.Distinct().ToList());
            }
            else
            {
                stepList.Add(list);
            }
        }
    }
}
