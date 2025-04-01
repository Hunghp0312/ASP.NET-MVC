using System.Data;
using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;
namespace Assignment.Controllers;


public class RookiesController : Controller
{
    private readonly IPersonService _personService;
    public RookiesController(IPersonService personService)
    {
        _personService = personService;
    }
    public IActionResult Index()
    {
        var people = _personService.GetAll();

        return View("RookiesTable", people);
    }
    public IActionResult GetMalePerson()
    {
        var people = _personService.GetByFilter(people => people.Gender == GenderType.Male);

        return View("RookiesTable", people);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
    public IActionResult GetOldestPerson()
    {
        var oldestPerson = new List<Person> { _personService.GetOldestPerson() };

        return View("RookiesTable", oldestPerson);
    }
    public IActionResult ExportExcel()
    {
        var stream = _personService.ExportToExcel();

        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xlsx");
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var person = _personService.GetById(id);

        return View("FormPerson", person);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View("FormPerson");
    }
    [HttpPost]
    public IActionResult Save(Person person)
    {
        if (ModelState.IsValid)
        {
            if (person.Id == null)
            {
                _personService.Insert(person);
            }
            else
            {
                _personService.Update(person);
            }
            return RedirectToAction("Index");
        }

        return View("FormPerson", person);
    }
    public IActionResult GetFullName()
    {
        var fullNames = _personService.GetPeopleFullName();

        return Ok(fullNames);
    }
    public IActionResult RedirectByYear([FromQuery] string action)
    {
        if (string.IsNullOrEmpty(action))
        {
            return BadRequest("action parameter is required.");
        }
        var actionLower = action.ToLower();
        if (actionLower == "lessthan")
        {
            return RedirectToAction("PersonBirthYearLess2000");
        }
        if (actionLower == "equal")
        {
            return RedirectToAction("PersonBirthYearEqual2000");
        }
        if (actionLower == "greaterthan")
        {
            return RedirectToAction("PersonBirthYearGreater2000");
        }

        return BadRequest("Invalid action parameter. Valid values are 'lessthan', 'equal', 'greaterthan'.");
    }
    public IActionResult PersonBirthYearLess2000()
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year < 2000);

        return View("RookiesTable", people);
    }
    public IActionResult PersonBirthYearEqual2000()
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year == 2000);

        return View("RookiesTable", people);
    }
    public IActionResult PersonBirthYearGreater2000()
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year > 2000);

        return View("RookiesTable", people);
    }
}