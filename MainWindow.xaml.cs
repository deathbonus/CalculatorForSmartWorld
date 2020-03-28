using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorCs
{
    public partial class MainWindow : Window
    {
        Calculator calculator;
        StringBuilder input = new StringBuilder();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            input.Append(0);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            input.Append(1);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            input.Append(2);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            input.Append(3);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            input.Append(4);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            input.Append(5);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            input.Append(6);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            input.Append(7);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            input.Append(8);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            input.Append(9);
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Sum(object sender, RoutedEventArgs e)
        {
            input.Append("+");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Sub(object sender, RoutedEventArgs e)
        {
            input.Append("-");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Multi(object sender, RoutedEventArgs e)
        {
            input.Append("*");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Div(object sender, RoutedEventArgs e)
        {
            input.Append("/");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Pow(object sender, RoutedEventArgs e)
        {
            input.Append("^");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Sqrt(object sender, RoutedEventArgs e)
        {
            input.Append("\u221A");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Dot(object sender, RoutedEventArgs e)
        {
             input.Append(",");
            calculatorDisplay.Text = input.ToString();
        }
        private void Button_Click_Del(object sender, RoutedEventArgs e)
        {
            if (input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
                calculatorDisplay.Text = input.ToString();
            }
        }
        private void Button_Click_Ans(object sender, RoutedEventArgs e)
        {
            calculator = new Calculator();
            try
            {
                calculatorDisplay.Text = calculator.calculate(input.ToString());
            }
            catch{ }
            input.Clear();
        }
    }

    internal class Calculator
    {
        StackForChars stackForChars = new StackForChars();
        StackForDoubles stackForDoubles = new StackForDoubles();
        StringBuilder postfixExpression = new StringBuilder();
        public String calculate(String infixExpression) {
            getPostfixExpressionFrom(infixExpression);
            //return postfixExpression.ToString();
            return calculatePostfixExpression(postfixExpression.ToString());
        }

        private void getPostfixExpressionFrom(String infixExpression) 
        {
            for (int i = 0; i < infixExpression.Length; i++)
            {
                char symbol = infixExpression.ElementAt(i);
                switch (symbol)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                        checkOperator(symbol);
                        break;
                    case '\u221A':
                        stackForChars.push(symbol);
                        break;
                    default:
                        if (i != 0)
                        {
                            checkOperand(symbol, infixExpression.ElementAt(i - 1));
                        }
                        else
                        {
                            postfixExpression.Append(symbol);
                        }
                        break;
                }
            }
            while (!stackForChars.isEmpty())
            {
                postfixExpression.Append("_").Append(stackForChars.pop());
            }
        }

        private void checkOperator(char symbol)
        {
            if (!stackForChars.isEmpty())
            {
                switch (stackForChars.peek())
                {
                    case '\u221A':
                    case '^':
                        postfixExpression.Append("_").Append(stackForChars.pop());
                        while (!stackForChars.isEmpty())
                        {
                            postfixExpression.Append("_").Append(stackForChars.pop());
                        }
                        stackForChars.push(symbol);
                        break;
                    case '*':
                    case '/':
                        if (symbol == '+' || symbol == '-')
                        {
                            postfixExpression.Append("_").Append(stackForChars.pop());
                            stackForChars.push(symbol);
                            break;
                        }
                        stackForChars.push(symbol);
                        break;
                    case '+':
                    case '-':
                        stackForChars.push(symbol);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                stackForChars.push(symbol);
            }
        }

        private void checkOperand(char symbol, char previousSymbol)
        {
            switch (previousSymbol)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case '\u221A':
                    postfixExpression.Append("_").Append(symbol);
                    break;
                default:
                    postfixExpression.Append(symbol);
                    break;
            }
        }

        private String calculatePostfixExpression(String postfixExpression)
        {
            String[] postfixExpressionArray = postfixExpression.Split('_');
            double result = 0;
            double last;
            double previous;
            for (int i = 0; i < postfixExpressionArray.Length; i++)
            {
                String symbol = postfixExpressionArray[i];
                switch (symbol)
                {
                    case "+":
                        last = stackForDoubles.pop();
                        previous = stackForDoubles.isEmpty() ? result : stackForDoubles.pop();
                        result = previous + last;
                        stackForDoubles.push(result);
                        break;
                    case "-":
                        last = stackForDoubles.pop();
                        previous = stackForDoubles.isEmpty() ? result : stackForDoubles.pop();
                        result = previous - last;
                        stackForDoubles.push(result);
                        break;
                    case "*":
                        last = (double)stackForDoubles.pop();
                        previous = stackForDoubles.isEmpty() ? result : stackForDoubles.pop();
                        result = previous * last;
                        stackForDoubles.push(result);
                        break;
                    case "/":
                        last = (double)stackForDoubles.pop();
                        previous = stackForDoubles.isEmpty() ? result : stackForDoubles.pop();
                        result = previous / last;
                        stackForDoubles.push(result);
                        break;
                    case "^":
                        last = (double)stackForDoubles.pop();
                        previous = stackForDoubles.isEmpty() ? result : stackForDoubles.pop();
                        result = Math.Pow(previous, last);
                        stackForDoubles.push(result);
                        break;
                    case "\u221A":
                        last = (double)stackForDoubles.pop();
                        result = Math.Sqrt(last);
                        stackForDoubles.push(result);
                        break;
                    case "":
                        break;
                    default:
                        stackForDoubles.push(Double.Parse(symbol));
                        break;
                }
            }
            return result.ToString();
        }

        internal class StackForChars
        {
            List<Char> elements = new List<char>();

            public void push(char element) 
            {
                elements.Add(element);
            }

            public char pop() 
            {
                char character = elements.ElementAt(elements.Count - 1);
                elements.RemoveAt(elements.Count - 1);
                return character;
            }

            public char peek() 
            {
                return elements.ElementAt(elements.Count - 1);
            }

            public bool isEmpty() 
            {
                return elements.Count <= 0;
            }
        }

        internal class StackForDoubles 
        {
            List<Double> elements = new List<double>();

            public void push(double element)
            {
                elements.Add(element);
            }

            public double pop()
            {
                double element = elements.ElementAt(elements.Count - 1);
                elements.RemoveAt(elements.Count - 1);
                return element;
            }

            public double peek()
            {
                return elements.ElementAt(elements.Count - 1);
            }

            public bool isEmpty()
            {
                return elements.Count <= 0;
            }
        }
    }
}
