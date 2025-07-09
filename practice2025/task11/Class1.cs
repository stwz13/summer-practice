using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace CodeGenerator
{
    public interface ICalculator
    {
        int Add(int a, int b);
        int Minus(int a, int b);
        int Mul(int a, int b);
        int Div(int a, int b);
    }
    public static class CalculatorGenerator
    {
        public static Type GenerateCalculatorClass()
        {

            string calculatorClassCode = @"public class Calculator : CodeGenerator.ICalculator
                {
                    public int Add(int a, int b) => a + b;
                    public int Minus(int a, int b) => a - b;
                    public int Mul(int a, int b) => a * b;
                    public int Div(int a, int b) => a / b;
                }";


            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var refOfClass = new []
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
            };

            
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(calculatorClassCode);

            var comp = CSharpCompilation.Create("CalculatorAssambly")
                .WithOptions(compilationOptions)
                .AddReferences(refOfClass)
                .AddSyntaxTrees(syntaxTree);
                
                

           var memoryStream = new MemoryStream();
           var emitMemoryStream = comp.Emit(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            Assembly assemblyWithCalculator = Assembly.Load(memoryStream.ToArray());

            return assemblyWithCalculator.GetType("Calculator")!;
            

        }
    }
}
