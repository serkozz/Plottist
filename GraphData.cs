using MathNet.Symbolics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Plottist;

public class GraphData
{
    public string Name { get; }

    public Expr Expression { get; }

    public List<double> Points { get; }

    public List<Dictionary<string, FloatingPoint>> Variables { get; }

    public static GraphData Undefined { get => undefined; }
    
    private static readonly GraphData undefined = new("undefined", "no_expression", "no_variables");

    public GraphData(string name, string expressionString, string variablesString)
    {
        Name = name;
        Expression = GetExpression(expressionString) ?? Expr.Undefined;
        Variables = GetVariables(variablesString);
        Points = GetPoints(Expression, Variables);
    }

    private static Expr GetExpression(string expression)
    {
        try
        {
            return Expr.Parse(expression);
        }
        catch (Exception)
        {
            return Expr.Undefined;
        }
    }

    private static List<Dictionary<string, FloatingPoint>> GetVariables(string variablesText)
    {
        try
        {
            var arrayOfVarEqualsValueStrings = variablesText.Split(";", StringSplitOptions.TrimEntries); // "a=5.2,b=3.4,c=2.1"[]
            Dictionary<string, FloatingPoint> variables = [];
            List<Dictionary<string, FloatingPoint>> variablesList = []; // { { "a", 5.2f }, { "b", 3.4f }, ...}

            foreach (var arrayOfVarEqualsValueString in arrayOfVarEqualsValueStrings)
            {
                string[] strings = arrayOfVarEqualsValueString.Split(",", StringSplitOptions.TrimEntries); // "a=5.2"[]
                foreach (var ch in strings)
                {
                    var res = ch.Split("=", StringSplitOptions.TrimEntries); // "["a", "5.2"]
                    variables.Add(res[0], double.Parse(res[1], CultureInfo.InvariantCulture));
                }
                variablesList.Add(variables);
                variables = [];
            }

            // Если список пустой или содержит только один словарь, возвращаем true
            if (variablesList == null || variablesList.Count == 0) throw new ArgumentException("variablesList is empty");

            // Получаем ключи первого словаря
            var firstDictionaryKeys = new HashSet<string>(variablesList.FirstOrDefault()!.Keys);

            // Сравниваем множества ключей каждого словаря с множеством ключей первого словаря
            var sameVariables = variablesList.All(dict => new HashSet<string>(dict.Keys).SetEquals(firstDictionaryKeys));
            return variablesList;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Enumerable.Empty<Dictionary<string, FloatingPoint>>().ToList();
        }
    }

    private static List<double> GetPoints(Expr expression, IEnumerable<Dictionary<string, FloatingPoint>> variables)
    {
        try
        {
            List<double> points = [];
            foreach (var variable in variables)
            {
                points.Add(expression.Evaluate(variable).RealValue);
            }
            return points;
        }
        catch (Exception)
        {
            return Enumerable.Empty<double>().ToList();
        }
    }
}
