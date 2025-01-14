using Expr = MathNet.Symbolics.SymbolicExpression;
using System.Windows.Controls;
using System.Windows;

namespace Plottist;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public GraphsManager GraphsManager { get; }

    public MainWindow()
    {
        InitializeComponent();

        GraphsManager = new(plot.Plot);

        expressionTextBox.TextChanged += (s, e) => DisplayLatex(((TextBox) s).Text);

        variablesTextBox.TextChanged += (s, e) => TryEvaluate(expressionTextBox.Text, ((TextBox)s).Text);
    }

    private void DisplayLatex(string formula)
    {
        try
        {
            latexPanel.Formula = Expr.Parse(formula).ToLaTeX();
        }
        catch (Exception)
        {
            latexPanel.Formula = string.Empty;
        }
    }

    private void TryEvaluate(string expression, string variables)
    {
        var created = GraphsManager.TryCreateGraph("", expression, variables);
        if (created)
        {
            plot.Plot.Clear();
            GraphsManager.AddCurrent();
            plot.Refresh();
        }
    }
}