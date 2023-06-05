using Microsoft.Extensions.Logging;
using StudentsClient.Domain.Models;
using StudentsClient.Domain.Repositories;
using StudentsClient.Domain.Services;

namespace StudentsClient.BLL.Services;
public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Create(IEnumerable<StudentCreateModel> students)
    {
        await foreach (var item in _repository.Create(students))
            _logger.LogInformation("Student Created by Id: {StudentId} at {DateTime}", item, DateTime.Now);

        await Task.CompletedTask;
    }

    public async Task Delete(int id) => await _repository.Delete(id);

    public async Task<StudentModel> Get(int id) => await _repository.Get(id);

    public async Task<List<StudentModel>> GetAll()
    {
        List<StudentModel> result = new List<StudentModel>();
        await foreach (var student in _repository.GetAll()) result.Add(student);
        return result;
    }

    public async Task Update(StudentUpdateModel student) => await _repository.Update(student);
}
