namespace Assignment1.Models;
public enum GenderType
{
    Male,
    Female,
    Other
}

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public GenderType Gender { get; set; } = GenderType.Male;
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string BirthPlace { get; set; } = string.Empty;
    public bool IsGraduated { get; set; } = false;
}