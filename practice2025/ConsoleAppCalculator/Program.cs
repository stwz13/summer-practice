using CodeGenerator;
namespace ConsoleApp
{
    public class CalculateApp
    {
        public static void Main(string[] args)
        {
            Type calculatorType = CalculatorGenerator.GenerateCalculatorClass();
            ICalculator calculator = (ICalculator)Activator.CreateInstance(calculatorType)!;

            int number1 = int.Parse(args[0]);
            string operation = args[1];
            int number2 = int.Parse(args[2]);

            switch(operation)
            {
                case "+":
                    {
                        Console.WriteLine($"{number1} + {number2} = {calculator.Add(number1, number2)}");
                        break;
                    }
                case "-":
                    {
                        Console.WriteLine($"{number1} - {number2} = {calculator.Minus(number1, number2)}");
                        break;
                    }
                case "*":
                    {
                        Console.WriteLine($"{number1} * {number2} = {calculator.Mul(number1, number2)}");
                        break;
                    }
                case "/":
                    {
                        Console.WriteLine($"{number1} / {number2} = {calculator.Div(number1, number2)}");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Введена некорректная операция.");
                        break;
                    }
                    
            }
        }
    }
}
