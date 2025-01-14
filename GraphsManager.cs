using MathNet.Numerics.Distributions;
using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace Plottist;

public class GraphsManager
{
    private readonly Plot _plot;

    private readonly List<GraphData> _graphs;
    
    private GraphData _currentGraph;

    public GraphData Current => _currentGraph;

    public GraphsManager(Plot plot)
    {
        _plot = plot;
        _graphs = [];
        _currentGraph = GraphData.Undefined;
    }

    public bool AddCurrent()
    {
        try
        {
            if (_graphs.Contains(_currentGraph))
                return false;
            _graphs.Add(_currentGraph);
            _plot.Add.Signal(/*_currentGraph.Points.Select((_, ind) => ind + 1).ToList() ,*/ _currentGraph.Points.ToList());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool TryCreateGraph(string name, string expressionString, string variablesString)
    {
        _currentGraph = GraphData.Undefined;
        _currentGraph = new(name, expressionString, variablesString);
        if (_currentGraph == GraphData.Undefined || _currentGraph.Variables.Count != _currentGraph.Points.Count || _currentGraph.Variables.Count <= 1)
            return false;
        return true;
    }

}
