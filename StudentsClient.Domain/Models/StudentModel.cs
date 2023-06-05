namespace StudentsClient.Domain.Models;
public class StudentModel
{
    public int Id { get; init; }
    public string StudentNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Description { get; init; }
    public List<string> PhoneNumbers { get; init; } = new();
}