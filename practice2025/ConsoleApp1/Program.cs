using task13;


namespace ConsoleSerializatorApp
{
    public class JsonFileWork
    {
        public static void SaveSerializationInFile(Student student, string path) =>
            File.WriteAllText(path, Serilazator.Serilaze(student));


        public static Student? DeserelizationFromFile(string file) => Deserializator.DeserializationFromString(File.ReadAllText(file));
    }
    public class Program
    {

        public static void Main(string[] args)
        {

            Student student = new Student()
            {
                FirstName = "Иван",
                LastName = "Иванов",
                BirthDate = new DateTime(2000, 1, 1),
                Grades = new List<Subject>
                {
                    new Subject{Name = "Математика", Grade = 5 },
                    new Subject{Name = "Физика", Grade = 5 },
                    new Subject{Name = "Черчение", Grade = 5 }
                }
            };

            JsonFileWork.SaveSerializationInFile(student, "studentInfo.json");

            var loadedStudent = JsonFileWork.DeserelizationFromFile("studentInfo.json")!;


            Console.WriteLine($"{loadedStudent.FirstName}\n{loadedStudent.LastName}\n{loadedStudent.BirthDate}\n" +
                $"{string.Join("\n", loadedStudent.Grades.Select(subject => $"{subject.Name} - {subject.Grade}"))}");


        }
    }
}