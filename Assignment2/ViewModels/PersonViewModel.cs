namespace Assignment2.ViewModels;

public class PersonViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string BirthPlace { get; set; } = string.Empty;
    bool IsGraduated {get; set; } = false;
}