using CodeGenerator;
using ConsoleApp;

namespace task11tests
{
    public class CalculatorGenericTest
    {
        [Fact]
        public void Calculator_AddsCorrect()
        {
            Type calculatorType = CalculatorGenerator.GenerateCalculatorClass();
            ICalculator calculator = (ICalculator)Activator.CreateInstance(calculatorType)!;

            Assert.Equal(12, calculator.Add(5, 7));
        }

        [Fact]
        public void Calculator_MultsCorrect()
        {
            Type calculatorType = CalculatorGenerator.GenerateCalculatorClass();
            ICalculator calculator = (ICalculator)Activator.CreateInstance(calculatorType)!;

            Assert.Equal(6, calculator.Mul(2, 3));
        }

        [Fact]
        public void Calculator_MinusCorrect()
        {
            Type calculatorType = CalculatorGenerator.GenerateCalculatorClass();
            ICalculator calculator = (ICalculator)Activator.CreateInstance(calculatorType)!;

            Assert.Equal(2, calculator.Div(6, 3));
        }
        [Fact]
        public void CalculatorApp_PrintsAddOperationMainTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            CalculateApp.Main(new string[]{"1", "+", "2"});

            Assert.Contains("1 + 2 = 3", output.ToString());

        }
        [Fact]
        public void CalculatorApp_PrintsMinusOperationMainTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            CalculateApp.Main(new string[] { "2", "-", "1" });

            Assert.Contains("2 - 1 = 1", output.ToString());

        }
        [Fact]
        public void CalculatorApp_PrintsMulOperationMainTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            CalculateApp.Main(new string[] { "3", "*", "2" });

            Assert.Contains("3 * 2 = 6", output.ToString());

        }
        [Fact]
        public void CalculatorApp_PrintsDivOperationMainTest()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            CalculateApp.Main(new string[] { "9", "/", "3" });

            Assert.Contains("9 / 3 = 3", output.ToString());

        }
        [Fact]
        public void CalculatorApp_PrintsMessageWithWrongOperation()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            CalculateApp.Main(new string[] { "1", "***", "5" });

            Assert.Contains("Введена некорректная операция.", output.ToString());

        }

    }
}
