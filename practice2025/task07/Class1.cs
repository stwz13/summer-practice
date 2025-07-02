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
            Console.WriteLine(attributesClassDisplayName != null ? $"Отображаемое имя класса: {attributesClassDisplayName.DisplayName}" :
                $"{type.Name} не поддерживает аттрибут DisplayNameAttribute");

            var attributesMethodsDisplayName = type.GetMethods()
                .Where(method => method.GetCustomAttribute<DisplayNameAttribute>() != null)
                .Select(method => method.Name)
                .ToList();
            Console.WriteLine(attributesMethodsDisplayName.Count != 0 ? string.Join("\n", attributesMethodsDisplayName) : "Нет методов с аттрибутом DisplayNameAttribute");

            var attributesPropertiesDisplayName = type.GetProperties()
                .Where(property => property.GetCustomAttribute<DisplayNameAttribute>() != null)
                .Select(property => property.Name)
                .ToList();
            Console.WriteLine(attributesPropertiesDisplayName.Count != 0 ? string.Join("\n", attributesPropertiesDisplayName) : "Нет свойств с аттрибутом DisplayNameAttribute");

            var attributesVersion = type.GetCustomAttribute<VersionAttribute>();
            Console.WriteLine(attributesVersion != null ? $"Версия класса: {attributesVersion.Major}.{attributesVersion.Minor}" : $"Тип {type.Name} не содержит аттрибут Version");

        }

    }
}


