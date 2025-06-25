using Xunit;
using task05;
using System.Runtime.Serialization;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField;
        public int Property { get; set; }

        public void Method() { }

        public void MethodWithParams(int param1) { }
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods();

            Assert.Contains("Method", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields();

            Assert.Contains("PublicField", fields);
            Assert.Contains("_privateField", fields);
        }

        [Fact]
        public void GetMethodParams_ReturnsCorrectParams()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var result = analyzer.GetMethodParams("MethodWithParams").ToList();

            Assert.Single(result);

            Assert.Contains("param1", result);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var result = analyzer.GetProperties().ToList();

            Assert.Single(result);
            Assert.Contains("Property", result);
        }

        [Fact]
        public void HasAttribute_ReturnsCorrectData()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            var result = analyzer.HasAttribute<SerializableAttribute>();

            Assert.True(result);
        }
    }
}

