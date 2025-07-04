using System.Reflection;
using task07;
using Xunit;
namespace task07tests
{
   
    public class AttributeReflectionTests
    {
        [Fact]
        public void Class_HasDisplayNameAttribute()
        {
            var type = typeof(SampleClass);
            var attribute = type.GetCustomAttribute<task07.DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Пример класса", attribute.DisplayName);
        }

        [Fact]
        public void Method_HasDisplayNameAttribute()
        {
            var method = typeof(SampleClass).GetMethod("TestMethod");
            var attribute = method?.GetCustomAttribute<task07.DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Тестовый метод", attribute.DisplayName);
        }

        [Fact]
        public void Property_HasDisplayNameAttribute()
        {
            var prop = typeof(SampleClass).GetProperty("Number");
            var attribute = prop?.GetCustomAttribute<task07.DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Числовое свойство", attribute.DisplayName);
        }

        [Fact]
        public void Class_HasVersionAttribute()
        {
            var type = typeof(SampleClass);
            var attribute = type.GetCustomAttribute<VersionAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(1, attribute.Major);
            Assert.Equal(0, attribute.Minor);
        }

        [Fact]
        public void PrintTypeInfo_WritesCorrectInfo()
        {
            var type = typeof(SampleClass);
            var output = new StringWriter();
            Console.SetOut(output);
            var expectedValue = "Отображаемое имя класса: Пример класса\r\nTestMethod\r\nNumber\r\nВерсия класса: 1.0\r\n";
            ReflectionHeplper.PrintTypeInfo(type);
            Assert.Equal(expectedValue, output.ToString());
        }
    }
}

