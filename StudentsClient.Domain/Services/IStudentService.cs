using StudentsClient.Domain.Models;

namespace StudentsClient.Domain.Services;
public interface IStudentService
{
    Task Create(IEnumerable<StudentCreateModel> students);
    Task Delete(int id);
    Task Update(StudentUpdateModel student);
    Task<StudentModel> Get(int id);
    Task<List<StudentModel>> GetAll();
}
