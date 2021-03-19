using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window
    {

        double lastNumber, result;
        string calculation;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += acButton_Click;
            neagtivButton.Click += neagtivButton_Click;
            percenatgeButton.Click += percenatgeButton_Click;
            resultButton.Click += resultButton_Click;
        }

        private void resultButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks which operation was used, and after calculates it by using another "SimpleMath" class

            double newNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.substract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        private void percenatgeButton_Click(object sender, RoutedEventArgs e)
        {
            double currentNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out currentNumber))
            {
                //This makes it, that if you go for 50 + 5% you get 5% out of fifty and not just 0,05 added to 50
                //But also checks, that if there is no last number, so u just type in 5%, it still works
                if (lastNumber != 0)
                    currentNumber = (currentNumber / 100) * lastNumber;
                else
                    currentNumber = (currentNumber / 100);

                resultLabel.Content = currentNumber.ToString();
                calculation += "%";
                calculationLabel.Content = calculation;
            }

        }

        private void neagtivButton_Click(object sender, RoutedEventArgs e)
        {
            //Converts the number from positiv to negativ and the other way
            double currentNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out currentNumber))
            {
                double currentCalculation = double.Parse(calculationLabel.Content.ToString());
                currentCalculation *= -1;
                calculation = currentCalculation.ToString();
                calculationLabel.Content = calculation;

                currentNumber = currentNumber * -1;
                resultLabel.Content = currentNumber.ToString();
            }
        }

        private void acButton_Click(object sender, RoutedEventArgs e)
        {
            //Resets everything
            resultLabel.Content = "0";
            lastNumber = 0;
            result = 0;
            calculation = "";
            calculationLabel.Content = calculation;
        }



        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            //Sets a operation
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplyButton)
            {
                selectedOperator = SelectedOperator.Multiplication;
                calculation += " " + "*" + " ";
            }
            else if (sender == divideButton)
            {
                selectedOperator = SelectedOperator.Division;
                calculation += " " + "/" + " ";
            }
            else if (sender == plusButton)
            {
                selectedOperator = SelectedOperator.Addition;
                calculation += " " + "+" + " ";
            }
            else if (sender == minusButton)
            {
                selectedOperator = SelectedOperator.Substraction;
                calculation += " " + "-" + " ";
            }

            calculationLabel.Content = calculation;
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            //Adds a comma to the number (System is not taking . weirdly)
            if (!resultLabel.Content.ToString().Contains(","))
            {
                resultLabel.Content += ",";
                calculation += ",";
                calculationLabel.Content = calculation;
            }
        }


        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            //Gets the content of the Button into a int
            int selectedValue = int.Parse((sender as Button).Content.ToString());


            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = selectedValue.ToString();
                calculation += selectedValue.ToString();
            }
            else
            {
                resultLabel.Content += selectedValue.ToString();
                calculation += selectedValue.ToString();
            }

            calculationLabel.Content = calculation;
        }
    }

    //New Types
    public enum SelectedOperator{
        Addition, Substraction, Multiplication, Division
    }

    public class SimpleMath
    {
        public static double add(double a, double b)
        {
            return a + b;
        }

        public static double substract(double a, double b)
        {
            return a - b;
        }

        public static double divide(double a, double b)
        {
            if (b == 0)
            {
                MessageBox.Show("Divison by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return 0;
            }

            return a / b;
        }
        public static double multiply(double a, double b)
        {
            return a * b;
        }
    }
}