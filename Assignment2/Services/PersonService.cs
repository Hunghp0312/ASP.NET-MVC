using System.Data;
using Assignment2.Models;
using ClosedXML.Excel;
namespace Assignment2.Services;

public class PersonService : IPersonService
{
    public List<Person> _people =
    [
        new Person { Id = 1, FirstName = "John", LastName = "Doe", Gender = GenderType.Male, DateOfBirth = new DateTime(1995, 5, 23), PhoneNumber = "123-456-7890", BirthPlace = "New York", IsGraduated = true },
        new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Gender = GenderType.Female, DateOfBirth = new DateTime(1998, 8, 14), PhoneNumber = "987-654-3210", BirthPlace = "Los Angeles", IsGraduated = false },
        new Person { Id = 3, FirstName = "Alice", LastName = "Johnson", Gender = GenderType.Other, DateOfBirth = new DateTime(2000, 1, 10), PhoneNumber = "555-123-4567", BirthPlace = "Chicago", IsGraduated = true },
        new Person { Id = 4, FirstName = "Bob", LastName = "Brown", Gender = GenderType.Male, DateOfBirth = new DateTime(1992, 12, 30), PhoneNumber = "444-789-1234", BirthPlace = "Houston", IsGraduated = false },
        new Person { Id = 5, FirstName = "Charlie", LastName = "Davis", Gender = GenderType.Male, DateOfBirth = new DateTime(1990, 3, 15), PhoneNumber = "222-333-4444", BirthPlace = "San Francisco", IsGraduated = true },
        new Person { Id = 6, FirstName = "Emily", LastName = "White", Gender = GenderType.Female, DateOfBirth = new DateTime(1997, 7, 21), PhoneNumber = "111-222-3333", BirthPlace = "Miami", IsGraduated = false },
        new Person { Id = 7, FirstName = "Frank", LastName = "Green", Gender = GenderType.Male, DateOfBirth = new DateTime(1985, 11, 2), PhoneNumber = "666-777-8888", BirthPlace = "Seattle", IsGraduated = true },
        new Person { Id = 8, FirstName = "Grace", LastName = "Hall", Gender = GenderType.Female, DateOfBirth = new DateTime(1993, 6, 18), PhoneNumber = "999-888-7777", BirthPlace = "Boston", IsGraduated = false },
        new Person { Id = 9, FirstName = "Henry", LastName = "Moore", Gender = GenderType.Other, DateOfBirth = new DateTime(1988, 9, 5), PhoneNumber = "333-444-5555", BirthPlace = "Denver", IsGraduated = true },
        new Person { Id = 10, FirstName = "Isabella", LastName = "Clark", Gender = GenderType.Female, DateOfBirth = new DateTime(2001, 4, 12), PhoneNumber = "777-666-5555", BirthPlace = "Austin", IsGraduated = false }
    ];
    public List<Person> GetAll()
    {
        return _people;
    }
    public List<Person> GetByFilter(Func<Person, bool> filter)
    {
        return _people.Where(filter).ToList();
    }
    public Person GetOldestPerson()
    {
        return _people.OrderBy(p => p.DateOfBirth).First();
    }
    public List<string> GetPeopleFullName()
    {
        return _people.Select(p => p.FullName).ToList();
    }
    public Stream ExportToExcel()
    {
        DataTable dt = new("Rookies");
        dt.Columns.AddRange(
        [
            new DataColumn("ID", typeof(int)),
            new DataColumn("First Name", typeof(string)),
            new DataColumn("Last Name", typeof(string)),
            new DataColumn("Gender", typeof(string)),
            new DataColumn("DateOfBirth", typeof(DateTime)),
            new DataColumn("PhoneNumber", typeof(string)),
            new DataColumn("BirthPlace", typeof(string)),
            new DataColumn("IsGraduated", typeof(bool)),

        ]);
        foreach (var person in _people)
        {
            dt.Rows.Add(person.Id, person.FirstName, person.LastName, person.Gender, person.DateOfBirth, person.PhoneNumber, person.BirthPlace, person.IsGraduated);
        }
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(dt, "Rookies");

        // Adjust column widths
        worksheet.Columns().AdjustToContents();

        // Save to MemoryStream
        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return stream;
    }
    public Person? GetById(int id)
    {
        return _people.FirstOrDefault(p => p.Id == id);
    }
    public void Insert(Person person)
    {
        person.Id = _people.Max(p => p.Id) + 1;
        _people.Add(person);
    }
    public int Update(Person person)
    {
        var index = _people.FindIndex(p => p.Id == person.Id);
        if (index != -1)
        {
            _people[index] = person;
        }
        return index;
    }
    public void Detete(int id)
    {
        var index = _people.FindIndex(p => p.Id == id);
        if (index != -1)
        {
            _people.RemoveAt(index);
        }
    }
}
