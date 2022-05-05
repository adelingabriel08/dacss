
namespace DataAccessObject
{
    // business object
    public class HoroscopeCalculator
    {
        private readonly IStudentDAO _studentDAO;
        private const string Vowels = "aeiou";

        public HoroscopeCalculator(IStudentDAO studentDAO)
        {
            _studentDAO = studentDAO;
        }

        public async Task PrintHoroscopeForStudent(string studentName)
        {
            var student = await _studentDAO.GetStudent(studentName);

            if (student is null)
            {
                Console.WriteLine($"Student {studentName} not found in the database");
                return;
            }

            Console.WriteLine(GetStudentHoroscopeMessage(student));
        }

        public async Task PrintHoroscopeForAllStudents()
        {
            var students = await _studentDAO.GetAll();

            if (!students.Any())
            {
                Console.WriteLine("No student found in the database");
                return;
            }

            students.ForEach(student => Console.WriteLine(GetStudentHoroscopeMessage(student)));
        }

        private bool IsStudentHavingAGoodDay(Student student)
        {
            var numberOfVowels = student.Name.ToLower().Count(letter => Vowels.Contains(letter));
            var day = DateTime.Now.Day;

            if (day % numberOfVowels == 0) return false;

            return true;
        }

        private string GetStudentHoroscopeMessage(Student student)
            => $"Student {student.Name} will have a " + (IsStudentHavingAGoodDay(student) ? "good" : "bad") + "day";


    }
}
