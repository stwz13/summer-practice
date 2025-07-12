using task13;
using ConsoleSerializatorApp;

namespace task13tests
{
    public class UnitTest1
    {
        string testJsonStudent = @"{
          ""FirstName"": ""Петр"", ""LastName"": ""Петров"", ""BirthDate"": ""05/05/2005"", ""Grades"": [
            {
              ""Name"": ""Математика"",
              ""Grade"": 5
            },
            {
              ""Name"": ""Метрология"",
              ""Grade"": 5
            },
            {
              ""Name"": ""Английский"",
              ""Grade"": 5
            }
          ]
        }";
        Student testStudent = new Student()
        {
                FirstName = "Петр",
                LastName = "Петров",
                BirthDate = new DateTime(2005, 5, 5),
                Grades = new List<Subject>
                {
                    new Subject{Name = "Математика", Grade = 5 },
                    new Subject{Name = "Метрология", Grade = 5 },
                    new Subject{Name = "Английский", Grade = 5 }
                }
            };
        Student wrongTestStudent = new Student()
        {
            FirstName = null,
            LastName = "Петров",
            BirthDate = new DateTime(2005, 5, 5),
            Grades = new List<Subject>
                {
                    new Subject{Name = null, Grade = 5 },
                    new Subject{Name = "Метрология", Grade = 5 },
                    new Subject{Name = "Английский", Grade = 5 }
                }
        };
        [Fact]
        public void isValidMethod_ReturnsTrueWithCorrectData() => 
            Assert.True(Deserializator.CheckForNull(testStudent));
        

        [Fact]
        public void isValidMethod_ReturnsTrueWithIncorrectFirstName() => 
            Assert.False(Deserializator.CheckForNull(wrongTestStudent));
        
        [Fact]
        public void isValidMethod_ReturnsTrueWithSubjectsName() =>
            Assert.False(Deserializator.CheckForNull(wrongTestStudent));
        
        [Fact]
        public void Deserialization_ReturnsNotNullWithCorrectStudentsFields()
        {
            Student student = Deserializator.DeserializationFromString(testJsonStudent)!;

            Assert.True(student != null && 
                student.FirstName != null &&
                student.LastName != null && 
                student.Grades != null);

        }

        [Fact]
        public void Deserialization_ReturnsCorrectFields()
        {
            Student student = Deserializator.DeserializationFromString(testJsonStudent)!;

            Assert.Equal(student.FirstName, testStudent.FirstName);
            Assert.Equal(student.LastName, testStudent.LastName);
            Assert.Equal(student.BirthDate, testStudent.BirthDate);
            Assert.Equal(3, student.Grades.Count);
            Assert.Equal("Математика", student.Grades[0].Name);
            Assert.Equal("Метрология", student.Grades[1].Name);
            Assert.Equal("Английский", student.Grades[2].Name);
        }

        [Fact]
        public void Deserialization_ReturnsExceptionWithIncorrectData()
        {
            string wrongTestJsonStudent = @"{
                  ""FirstName"": """", ""LastName"": """", ""BirthDate"": ""05/05/2005"", ""Grades"": [
                    {
                      ""Name"": """",
                      ""Grade"": 5
                    },
                    {
                      ""Name"": """",
                      ""Grade"": 5
                    },
                    {
                      ""Name"": """",
                      ""Grade"": 5
                    }
                  ]
            }";
            var exception = Assert.Throws<Exception>(() => Deserializator.DeserializationFromString(wrongTestJsonStudent));
            Assert.Contains("Данные содержат пустую строку или значение null", exception.Message);
        }
        [Fact]
        public void Deserialization_ReturnsExceptionWithWrongData()
        {
            string wrongDataJsonStudent = @"{
                  ""FirstName"": ""Петр"", ""LastName"": ""Петров"", ""BirthDate"": ""05/05/1895"", ""Grades"": [
                    {
                      ""Name"": ""Математика"",
                      ""Grade"": 5
                    },
                    {
                      ""Name"": ""Метрология"",
                      ""Grade"": 5
                    },
                    {
                      ""Name"": ""Английский"",
                      ""Grade"": 5
                    }
                  ]
             }";

            var exception = Assert.Throws<Exception>(() => Deserializator.DeserializationFromString(wrongDataJsonStudent));
            Assert.Contains("Некорректная дата", exception.Message);

        }
        [Fact]
        public void Deserialization_ReturnsExceptionWithWrongGrades()
        {
            string wrongGradesJsonStudent = @"{
                  ""FirstName"": ""Петр"", ""LastName"": ""Петров"", ""BirthDate"": ""05/05/1995"", ""Grades"": [
                    {
                      ""Name"": ""Математика"",
                      ""Grade"": -3
                    },
                    {
                      ""Name"": ""Метрология"",
                      ""Grade"": 5
                    },
                    {
                      ""Name"": ""Английский"",
                      ""Grade"": 5
                    }
                  ]
             }";

            var exception = Assert.Throws<Exception>(() => Deserializator.DeserializationFromString(wrongGradesJsonStudent));
            Assert.Contains("Оценка должна быть натуральным числом", exception.Message);

        }

        [Fact]            
        public void Serializator_ReturnCorrectField()
        {
            string jsonStudent = Serilazator.Serilaze(testStudent);

            Student student = Deserializator.DeserializationFromString(jsonStudent)!;

            Assert.Equal(student.FirstName, testStudent.FirstName);
            Assert.Equal(student.LastName, testStudent.LastName);
            Assert.Equal(student.BirthDate, testStudent.BirthDate);
            Assert.Equal(3, student.Grades.Count);
            Assert.Equal("Математика", student.Grades[0].Name);
            Assert.Equal("Метрология", student.Grades[1].Name);
            Assert.Equal("Английский", student.Grades[2].Name);
        }

        [Fact]
        public void SaveSerializationInFile_SavesDataInFile()
        {
            string filePath = "studentInfo.json";

            JsonFileWork.SaveSerializationInFile(testStudent, filePath);
            Assert.True(File.Exists(filePath));

            string fileData = File.ReadAllText(filePath);
            Student student = Deserializator.DeserializationFromString(fileData)!;
            Assert.NotNull(student);
        }
        [Fact]
        public void DeserelizationFromFile_ReturnsCorrectData()
        {
            string filePath = "studentInfo.json";
            JsonFileWork.SaveSerializationInFile(testStudent, filePath);

            Student student = JsonFileWork.DeserelizationFromFile(filePath)!;

            Assert.Equal(student.FirstName, testStudent.FirstName);
            Assert.Equal(student.LastName, testStudent.LastName);
            Assert.Equal(student.BirthDate, testStudent.BirthDate);
            Assert.Equal(3, student.Grades.Count);
            Assert.Equal("Математика", student.Grades[0].Name);
            Assert.Equal("Метрология", student.Grades[1].Name);
            Assert.Equal("Английский", student.Grades[2].Name);

        }

    }
}
