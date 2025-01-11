using Expr = MathNet.Symbolics.SymbolicExpression;
using System.Windows.Controls;
using MathExpressionsParser;
using System.Windows.Input;
using System.Globalization;
using System.Diagnostics;
using MathNet.Symbolics;
using System.Windows;

namespace Plottist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Expr Expression { get; private set; } = Expr.Undefined;

        public List<float> Points { get; private set; } = [];

        public List<Dictionary<string, FloatingPoint>> Variables { get; private set; } = [];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            ArgumentNullException.ThrowIfNull(btn);

            if (btn.Name.Contains("build", StringComparison.InvariantCultureIgnoreCase))
            {
                BuildPlot();
            }
            else if (btn.Name.Contains("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                Close();
            }

            ArgumentNullException.ThrowIfNull(btn);
        }

        private Expr GetExpression(string expression)
        {
            var expr = Parser.ParseExpression(expression);
            ArgumentNullException.ThrowIfNull(expr);
            return expr;
        }

        private List<Dictionary<string, FloatingPoint>> GetVariables(string variablesText)
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
                        variables.Add(res[0], float.Parse(res[1], CultureInfo.InvariantCulture));
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
                Variables = [];
                return variablesList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Enumerable.Empty<Dictionary<string, FloatingPoint>>().ToList();
            }
        }

        private List<float> GetPoints()
        {
            List<float> points = [];
            foreach (var variable in Variables)
            {
                points.Add(float.Parse(Parser.EvaluateExpression(Expression, variable).ToString().Split(" ")[1], CultureInfo.InvariantCulture));
            }
            return points;
        }

        private void BuildPlot()
        {
            Points = GetPoints();
            if (Points.Count == 0) return;
            plot.Plot.Add.Scatter(Points.Select((_, i) => i).ToList(), Points);
            plot.Refresh();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var frameWorkElement = sender as FrameworkElement;
            ArgumentNullException.ThrowIfNull(frameWorkElement);    
            if (frameWorkElement.Name.Contains("expression"))
            {
                if (e.Key == Key.Enter)
                {
                    Expression = GetExpression(expressionTextBox.Text);
                }
            }
            else if (frameWorkElement.Name.Contains("points"))
            {
                if (e.Key == Key.Enter)
                {
                    Variables = GetVariables(pointsTextBox.Text);
                }
            }
        }
    }
}