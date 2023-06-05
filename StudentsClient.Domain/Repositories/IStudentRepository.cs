using StudentsClient.Domain.Models;

namespace StudentsClient.Domain.Repositories;
public interface IStudentRepository
{
    IAsyncEnumerable<int> Create(IEnumerable<StudentCreateModel> students);
    Task Delete(int id);
    Task Update(StudentUpdateModel student);
    Task<StudentModel> Get(int id);
    IAsyncEnumerable<StudentModel> GetAll();
}
