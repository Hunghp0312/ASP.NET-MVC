using Assignment1.Models;
namespace Assignment1.Services;
public interface IPersonService
{
    List<Person> GetAll();
    List<Person> GetByFilter(Func<Person, bool> filter);
    Person GetOldestPerson();
    List<string> GetPeopleFullName();
    Stream ExportToExcel();
}