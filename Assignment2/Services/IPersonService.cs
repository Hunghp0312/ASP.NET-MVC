using Assignment2.Models;
namespace Assignment2.Services;

public interface IPersonService
{
    
    List<Person> GetAll();
    List<Person> GetByFilter(Func<Person, bool> filter);
    Person GetOldestPerson();
    List<string> GetPeopleFullName();
    Stream ExportToExcel();
    Person? GetById(int id);
    void Insert(Person person);
    int Update(Person person);
    void Detete(int id);

}
