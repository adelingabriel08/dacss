// See https://aka.ms/new-console-template for more information
using DataAccessObject;
using DataAccessObject.Database;

while (true)
{
    var dbConnectionString = @"Server=localhost;Database=StudentsHoroscope;Trusted_Connection=True;";
    var xmlFilePath = @"students.xml";
    var dbContext = new ApplicationDbContext(dbConnectionString);
    var studentDbDAO = new StudentDatabaseDAO(dbContext);
    var studentXmlDAO = new StudentXmlDAO(xmlFilePath);

    Console.WriteLine();
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. Get all students from database horoscope");
    Console.WriteLine("2. Get all students from xml file horoscope");
    Console.WriteLine("3. Get a student by name from database horoscope");
    Console.WriteLine("4. Get a student by name from xml file horoscope");
    Console.WriteLine("5. Add a student to database");
    Console.WriteLine("6. Add a student to xml file");
    Console.WriteLine("0. Exit");
    Console.Write("Enter the option number: ");

    int option;
    var input = Console.ReadLine();
    if (!int.TryParse(input, out option) || option < 0 || option > 6)
    {
        Console.Clear();
        Console.WriteLine("Invalid option entered! Please retry");
        continue;
    }
    if (option == 0)
        break;

    Console.WriteLine();

    switch(option)
    {
        case 1:
            var horoscopeCalculator1 = new HoroscopeCalculator(studentDbDAO);
            Console.WriteLine("Getting all students from database:");
            await horoscopeCalculator1.PrintHoroscopeForAllStudents();
            break;
        case 2:
            var horoscopeCalculator2 = new HoroscopeCalculator(studentXmlDAO);
            Console.WriteLine("Getting all students from xml file:");
            await horoscopeCalculator2.PrintHoroscopeForAllStudents();
            break;
        case 3:
            var horoscopeCalculator3 = new HoroscopeCalculator(studentDbDAO);
            Console.Write("Enter the student name: ");
            var studentName = Console.ReadLine();
            await horoscopeCalculator3.PrintHoroscopeForStudent(studentName);
            break;
        case 4:
            var horoscopeCalculator4 = new HoroscopeCalculator(studentXmlDAO);
            Console.Write("Enter the student name: ");
            var studentNameXml = Console.ReadLine();
            await horoscopeCalculator4.PrintHoroscopeForStudent(studentNameXml);
            break;
        case 5:
            Console.Write("Enter the student name to add: ");
            var studentToAddName = Console.ReadLine();
            await AddStudent(studentDbDAO, studentToAddName);
            Console.WriteLine("Student added to database!");
            break;
        case 6:
            Console.Write("Enter the student name to add: ");
            var studentToAddNameXml = Console.ReadLine();
            await AddStudent(studentXmlDAO, studentToAddNameXml);
            Console.WriteLine("Student added to xml file!");
            break;
    }
}

async Task AddStudent(IStudentDAO studentDao, string studentName)
    => await studentDao.AddStudent(studentName);




