using System.Xml.Linq;

namespace DataAccessObject
{
    public class StudentXmlDAO : IStudentDAO
    {
        private readonly string _xmlFilePath;

        public StudentXmlDAO(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }
        public Task AddStudent(string studentName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> GetAll()
        {
            var xml = await XDocument.LoadAsync(new FileStream(_xmlFilePath, FileMode.Open), LoadOptions.PreserveWhitespace, CancellationToken.None);

            var students = from c in xml.Root.Descendants("student")
                          select new Student() { Name = c.Element("name").Value };
            return students.ToList();
        }

        public async Task<Student> GetStudent(string studentName)
        {
            var xml = await XDocument.LoadAsync(new FileStream(_xmlFilePath, FileMode.Open), LoadOptions.PreserveWhitespace, CancellationToken.None);

            var student = from c in xml.Root.Descendants("student")
                        where c.Element("name").Value == studentName
                        select new Student() { Name = c.Element("name").Value };

            return student;
        }
    }
}
