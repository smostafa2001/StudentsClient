namespace StudentsClient.Domain.Models;

public class StudentUpdateModel
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Description { get; init; }
}