using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;


namespace task13
{
    public class Subject
    {
        public string Name { get; set; }
        public int Grade { get; set; }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Subject> Grades { get; set; }
    }

    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            DateTime.ParseExact(reader.GetString()!, "dd/MM/yyyy", CultureInfo.InvariantCulture);


        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));

    }

    public class Serilazator
    {
        public static string Serilaze(Student student)
        {

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new DateTimeJsonConverter() },
                WriteIndented = true,
            };
            return JsonSerializer.Serialize(student, options);
            
        }
    }
    public class Deserializator
    {
        public static Student DeserializationFromString(string input)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { new DateTimeJsonConverter() },
                WriteIndented = true,
            };

            Student student = JsonSerializer.Deserialize<Student>(input, options)!;

            if (student == null) throw new Exception("Ошибка десериализации");

            if (!CheckForNull(student)) throw new Exception("Данные содержат пустую строку или значение null");

            if (DateTime.Today < student.BirthDate || student.BirthDate.Year < 1900) throw new Exception("Некорректная дата");

            if (student.Grades.Select(subj => subj.Grade <= 0).Contains(true)) throw new Exception("Оценка должна быть натуральным числом");

            return student;
        }
        public static bool CheckForNull(Student student) => !string.IsNullOrEmpty(student.FirstName)
            && !string.IsNullOrEmpty(student.LastName) && (student.Grades != null)
            && !student.Grades.Select(subj => string.IsNullOrEmpty(subj.Name)).Contains(true);

    }
}
