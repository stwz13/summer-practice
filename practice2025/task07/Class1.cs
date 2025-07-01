using System.Reflection;
namespace task07
{
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; set; } = string.Empty;
        public DisplayNameAttribute() { }
        public DisplayNameAttribute(string displayName) => DisplayName = displayName;
    }

    public class VersionAttribute : Attribute
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public VersionAttribute() { }
        public VersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }

    [Version(1,0)]
    [DisplayName("Пример класса")]
    public class SampleClass 
    {
        [DisplayName("Тестовый метод")]
        public void TestMethod() { }
        [DisplayName("Числовое свойство")]
        public int Number {  get; set; }

    };

    public static class ReflectionHeplper
    {
        public static void PrintTypeInfo(Type type)
        {
            var attributesClassDisplayName = type.GetCustomAttribute<DisplayNameAttribute>();
            if (attributesClassDisplayName != null) Console.WriteLine($"Отображаемое имя класса {attributesClassDisplayName.DisplayName}");
            else Console.WriteLine($"{type.Name} не поддерживает аттрибут DisplayNameAttribute");


            var attributesMethodsDisplayName = type.GetMethods()
                .Where(method => method.GetCustomAttribute<DisplayNameAttribute>() != null)
                .Select(method => method.Name)
                .ToList();
            if (attributesMethodsDisplayName.Count != 0) foreach (var attributesMethod in attributesMethodsDisplayName) Console.WriteLine(attributesMethod);
            else Console.WriteLine("Нет методов с аттрибутом DisplayNameAttribute");


            var attributesPropertiesDisplayName = type.GetProperties()
                .Where(property => property.GetCustomAttribute<DisplayNameAttribute>() != null)
                .Select(property => property.Name)
                .ToList();

            if (attributesPropertiesDisplayName.Count != 0) foreach (var attributesProperty in attributesPropertiesDisplayName) Console.WriteLine(attributesProperty);
            else Console.WriteLine("Нет свойств с аттрибутом DisplayNameAttribute");

            var attributesVersion = type.GetCustomAttributes<VersionAttribute>();
            if (attributesVersion.Any()) foreach (var attr in attributesVersion) Console.WriteLine($"Версия класса {attr.Major}.{attr.Minor}");
            else Console.WriteLine($"Тип {type.Name} не содержит аттрибут Version");
        }

    }
}

