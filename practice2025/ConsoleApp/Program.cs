using System;
using System.Reflection;

namespace ConsoleApp
{
    public class ConsolePrinter
    {
        public static void PrintConstructorInfo(ConstructorInfo constructor)
        {
            Console.WriteLine($"Имя конструктора: {constructor.Name}");

            var paramsOfConstructor = constructor.GetParameters();

            Console.WriteLine(paramsOfConstructor.Any() ?
                string.Join("\n", paramsOfConstructor.Select(param => $"Имя параметра - {param.Name}," +
                    $" тип параметра - {param.ParameterType}")) :
                "Параметры отсутствуют");

            Console.WriteLine();
        }
        public static void PrintMethodInfo(MethodInfo method)
        {
            Console.WriteLine($"Имя метода: {method.Name}");

            var paramsOfMethod = method.GetParameters();

            Console.WriteLine(paramsOfMethod.Any() ?
                string.Join("\n", paramsOfMethod.Select(param => $"Имя параметра - {param.Name}," +
                    $" тип параметра - {param.ParameterType}")) :
                "Параметры отсутствуют");

            Console.WriteLine();
        }

        public static void PrintTypeInfo(Type type)
        {
            Console.WriteLine($"Имя класса: {type.FullName}\n");

            var methodsOfType = type.GetMethods();

            if (methodsOfType.Any())
            {
                Console.WriteLine("Список методов:");
                foreach (var method in methodsOfType) PrintMethodInfo(method);
            }
            else Console.WriteLine("Методы отсутствуют");

            Console.WriteLine();

            var constructorsOfType = type.GetConstructors();
            if (constructorsOfType.Any())
            {
                Console.WriteLine("Список конструкторов:");
                foreach (var constructor in constructorsOfType) PrintConstructorInfo(constructor);
            }
            else Console.WriteLine("Конструкторы отсутствуют");

            var attributesOfType = type.GetCustomAttributes(true);

            Console.WriteLine(attributesOfType.Any() ? "Список атрибутов:\n" +
            string.Join("\n", attributesOfType.Select(attribute => $"Имя атрибута {attribute.ToString}"))
            : "Атрибуты отсутствуют");


            Console.WriteLine();

        }
        static void Main(string[] args)
        {
            string libraryPath = args[0];
            Assembly assembly = Assembly.LoadFrom(libraryPath);

            var classesOfLibrary = assembly.GetTypes();

            foreach (var type in classesOfLibrary) PrintTypeInfo(type);

        }
    }
}