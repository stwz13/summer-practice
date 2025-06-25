using System;
using System.Reflection;


namespace task05
{

    public class ClassAnalyzer
    {
        private Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type;
        }

        public IEnumerable<string> GetPublicMethods() => _type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(method => method.Name);
        public IEnumerable<string> GetMethodParams(string methodname) => _type.GetMethod(methodname)!.GetParameters().Select(param => param.Name)!;

        public IEnumerable<string> GetAllFields() => _type
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
            .Select(field => field.Name);

        public IEnumerable<string> GetProperties() => _type.GetProperties().Select(property => property.Name);


        public bool HasAttribute<T>() where T : Attribute => _type.GetCustomAttribute<T>() != null;
    }
}


