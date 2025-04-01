
using Microsoft.AspNetCore.Mvc;
using Assignment1.Models;
using Assignment1.Services;
namespace Assignment1.Controllers;

public class RookieController : ControllerBase
{
    private readonly IPersonService _personService;

    public RookieController(IPersonService personService)
    {
        _personService = personService;
    }


    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_personService.GetAll());
    }
    public IActionResult GetMalePerson()
    {
        var people = _personService.GetByFilter(p => p.Gender == GenderType.Male);

        return Ok(people);
    }
    public IActionResult GetOldestPerson()
    {
        var person = _personService.GetOldestPerson();

        return Ok(person);
    }
    public IActionResult GetPeopleFullName()
    {
        var people = _personService.GetPeopleFullName();

        return Ok(people);
    }
    public IActionResult RedirectByAction([FromQuery] string action)
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

        return Ok(people);
    }
    public IActionResult PersonBirthYearEqual2000()
    {

        var people = _personService.GetByFilter(p => p.DateOfBirth.Year == 2000);

        return Ok(people);


    }
    public IActionResult PersonBirthYearGreater2000()
    {
        var people = _personService.GetByFilter(p => p.DateOfBirth.Year > 2000);

        return Ok(people);
    }
    public IActionResult ExportExcel()
    {
        var stream = _personService.ExportToExcel();
        return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Rookies.xlsx");
    }

}
