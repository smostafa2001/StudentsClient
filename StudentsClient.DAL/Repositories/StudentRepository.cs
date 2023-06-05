using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using StudentsClient.DAL.Protos.v1;
using StudentsClient.Domain.Models;
using StudentsClient.Domain.Repositories;
using static StudentsClient.DAL.Protos.v1.StudentService;

namespace StudentsClient.DAL.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly StudentServiceClient _serviceClient;

    public StudentRepository(StudentServiceClient serviceClient) => _serviceClient = serviceClient;

    public async IAsyncEnumerable<int> Create(IEnumerable<StudentCreateModel> students)
    {
        var request = _serviceClient.CreateStudent();
        foreach (var student in students)
        {
            StudentCreateRequest createRequest = new()
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description,
                StudentNumber = student.StudentNumber
            };
            student.PhoneNumbers.AddRange(student.PhoneNumbers);
            await request.RequestStream.WriteAsync(createRequest);
        }

        await request.RequestStream.CompleteAsync();
        while (await request.ResponseStream.MoveNext())
        {
            var response = request.ResponseStream.Current;
            yield return response.Id;
        }
    }

    public async Task Delete(int id)
    {
        var byIdRequest = new StudentByIdRequest { Id = id };
        await _serviceClient.DeleteStudentAsync(byIdRequest);
    }

    public async Task<StudentModel> Get(int id)
    {
        var byIdRequest = new StudentByIdRequest { Id = id };
        var result = await _serviceClient.GetAsync(byIdRequest);
        var model = new StudentModel
        {
            FirstName = result.FirstName,
            LastName = result.LastName,
            Description = result.Description,
            StudentNumber = result.StudentNumber
        };
        model.PhoneNumbers.AddRange(result.PhoneNumbers);
        return model;
    }

    public async IAsyncEnumerable<StudentModel> GetAll()
    {
        var request = _serviceClient.GetAll(new Empty());
        while (await request.ResponseStream.MoveNext())
        {
            var reply = request.ResponseStream.Current;
            var student = new StudentModel
            {
                Id = reply.Id,
                FirstName = reply.FirstName,
                LastName = reply.LastName,
                Description = reply.Description,
                StudentNumber = reply.StudentNumber
            };
            student.PhoneNumbers.AddRange(reply.PhoneNumbers);
            yield return student;
        }
    }

    public async Task Update(StudentUpdateModel student)
    {
        var updateRequest = new StudentUpdateRequest
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Description = student.Description
        };
        await _serviceClient.UpdateStudentAsync(updateRequest);
    }
}
