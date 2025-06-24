using Xunit;
using task02;

namespace task02tests
{
    public class StudentServiceTests
    {
        private List<Student> _testStudents;
        private StudentService _service;

        public StudentServiceTests()
        {
            _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
            _service = new StudentService(_testStudents);
        }

        [Fact]
        public void GetStudentsByFaculty_ReturnsCorrectStudents()
        {
            var result = _service.GetStudentsByFaculty("ФИТ").ToList();
            Assert.Equal(2, result.Count);
            Assert.True(result.All(s => s.Faculty == "ФИТ"));
        }

        [Fact]
        public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
        {
            var result = _service.GetFacultyWithHighestAverageGrade();
            Assert.Equal("Экономика", result);
        }

        [Fact]
        public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
        {
            var result = _service.GetStudentsWithMinAverageGrade(4);

            Assert.Equal(2, result.Count());
            Assert.True(result.All(s => s.Grades.Average() >= 4));
        }

        [Fact]
        public void GetStudentsOrderedByName_ReturnsCorrectOrder()
        {
            var result = _service.GetStudentsOrderedByName();

            var arrayResult = result.ToArray();

            Assert.Equal("Анна", arrayResult[0].Name);
            Assert.Equal("Иван", arrayResult[1].Name);
            Assert.Equal("Петр", arrayResult[2].Name);
        }

        [Fact]
        public void GetStudentsGroupedByFaculty_ReturnsCorrectGroup()
        {
            var result = _service.GetStudentsByFaculty("ФИТ");

            var arrayResult = result.ToArray();

            Assert.Equal(2, arrayResult.Length);
            Assert.Equal("Иван", arrayResult[0].Name);
            Assert.Equal("Анна", arrayResult[1].Name);
        }
    }
}
