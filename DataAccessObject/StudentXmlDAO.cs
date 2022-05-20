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
        public async Task AddStudent(string studentName)
        {
            var fileStream = new FileStream(_xmlFilePath, FileMode.Open);
            var xml = await XDocument.LoadAsync(fileStream, LoadOptions.PreserveWhitespace, CancellationToken.None);
            fileStream.Close();

            var lastStudentId = (await GetAll()).LastOrDefault().Id;

            var student = new Student() { Id = lastStudentId + 1, Name = studentName.Trim() };

            var element = new XElement("student", 
                                new XElement("id", student.Id), 
                                new XElement("name", student.Name)
                         );
            xml.Element("students").Add(element);
            using (FileStream fs = new FileStream(_xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                StreamReader sr = new StreamReader(fs);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    fs.SetLength(0);
                    xml.Save(sw);
                }
                fs.Close();
            }
           
        }

        public async Task<List<Student>> GetAll()
        {
            var fileStream = new FileStream(_xmlFilePath, FileMode.Open);
            var xml = await XDocument.LoadAsync(fileStream, LoadOptions.PreserveWhitespace, CancellationToken.None);
            fileStream.Close();
            var students = from c in xml.Root.Descendants("student")
                          select new Student() { Name = c.Element("name").Value, Id = int.Parse(c.Element("id").Value)};
            return students.ToList();
        }

        public async Task<Student> GetStudent(string studentName)
        {
            var fileStream = new FileStream(_xmlFilePath, FileMode.Open);
            var xml = await XDocument.LoadAsync(fileStream, LoadOptions.PreserveWhitespace, CancellationToken.None);
            fileStream.Close();
            var students = from c in xml.Root.Descendants("student")
                           select new Student() { Name = c.Element("name").Value, Id = int.Parse(c.Element("id").Value) };

            return students.FirstOrDefault(s => s.Name == studentName.Trim());
        }
    }
}
