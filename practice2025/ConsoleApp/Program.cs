using System.Reflection;

namespace ConsoleApp
{
    public class ConsolePrinter
    {
        public static void PrintConstructorInfo(ConstructorInfo constructor)
        {
            Console.WriteLine($"Имя конструктора: {constructor.Name}");

            var paramsOfConstructor = constructor.GetParameters();
            if (paramsOfConstructor.Any())
            {
                foreach (var param in paramsOfConstructor) Console.WriteLine($"Имя параметра - {param.Name}," +
                    $" тип параметра - {param.ParameterType}");
            }
            else Console.WriteLine("Параметры отсутствуют");

            Console.WriteLine();
        }
        public static void PrintMethodInfo(MethodInfo method)
        {
            Console.WriteLine($"Имя метода: {method.Name}");

            var paramsOfMethod = method.GetParameters();
            if (paramsOfMethod.Any())
            {
                foreach (var param in paramsOfMethod) Console.WriteLine($"Имя параметра - {param.Name}," +
                    $" тип параметра - {param.ParameterType}");
            }
            else Console.WriteLine("Параметры отсутствуют");

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
                foreach(var constructor in constructorsOfType) PrintConstructorInfo(constructor);
            }
            else Console.WriteLine("Конструкторы отсутствуют");

            Console.WriteLine();

            var attributesOfType = type.GetCustomAttributes(true);
            if (attributesOfType.Any())
            {
                Console.WriteLine($"Список атрибутов:");
                foreach (var attribute in attributesOfType) Console.WriteLine($"Имя атрибута {attribute.ToString}");
            }
            else Console.WriteLine("Кастомные атрибуты отсутствуют");

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