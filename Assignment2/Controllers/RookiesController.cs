using Assignment2.Services;
using Microsoft.AspNetCore.Mvc;
using Assignment2.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
namespace Assignment.Controllers;

public class RookiesController : Controller
{
    private readonly IPersonService _personService;

    public RookiesController(IPersonService personService)
    {
        _personService = personService;
    }

    public IActionResult Index(int pageSize = 5, int pageIndex = 1)
    {
        var peopleViewModel = _personService.GetAll(pageSize, pageIndex);

        return View("RookiesTable", peopleViewModel);
    }

    public IActionResult MalePerson(int pageSize = 5, int pageIndex = 1)
    {
        var people = _personService.GetByFilter(people => people.Gender == GenderType.Male,pageSize, pageIndex);
        ViewBag.Action = "MalePerson";

        return View("RookiesTable", people);
    }

    public IActionResult GetOldestPerson()
    {
        var oldestPerson = _personService.GetOldestPerson();
        ViewBag.Action = "GetOldestPerson";

        return View("RookiesTable", oldestPerson);
    }

    public IActionResult ExportExcel()
    {
        var stream = _personService.ExportToExcel();

        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xlsx");
    }

    [HttpGet]
    public IActionResult Update(int id)
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
    public IActionResult Create(Person person)
    {
        if (ModelState.IsValid)
        {
            _personService.Create(person);

            return RedirectToAction("Index");
        }

        return View("FormPerson", person);
    }

    [HttpPost]
    public IActionResult Update(Person person)
    {
        if (ModelState.IsValid)
        {
            _personService.Update(person);

            return RedirectToAction("Index");
        }

        return View("FormPerson", person);
    }

    public IActionResult Delete(int id)
    {
        var person = _personService.Detete(id);
        ViewBag.UserName = person.FullName;

        return View("DeleteConfirmation");
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
    public IActionResult PersonBirthYearLess2000(int pageSize = 5, int pageIndex = 1)
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year < 2000,  pageSize,  pageIndex);
        ViewBag.Action = "PersonBirthYearLess2000";

        return View("RookiesTable", people);
    }
    public IActionResult PersonBirthYearEqual2000(int pageSize = 5, int pageIndex = 1)
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year == 2000, pageSize, pageIndex);
        ViewBag.Action = "PersonBirthYearEqual2000";

        return View("RookiesTable", people);
    }
    public IActionResult PersonBirthYearGreater2000(int pageSize = 5, int pageIndex = 1)
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year > 2000, pageSize, pageIndex);
        ViewBag.Action = "PersonBirthYearGreater2000";

        return View("RookiesTable", people);
    }
}