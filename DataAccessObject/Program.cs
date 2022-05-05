// See https://aka.ms/new-console-template for more information
using DataAccessObject;
using DataAccessObject.Database;

using (var context = new ApplicationDbContext("Server=localhost;Database=StudentsHoroscope;Trusted_Connection=True;"))
{

    var std = new Student()
    {
        Name = "Tom Costica"
    };

    context.Students.Add(std);
    context.SaveChanges();
}
