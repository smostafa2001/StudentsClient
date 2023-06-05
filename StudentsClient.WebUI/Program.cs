using StudentsClient.BLL.Services;
using StudentsClient.DAL.Repositories;
using StudentsClient.Domain.Repositories;
using StudentsClient.Domain.Services;
using static StudentsClient.DAL.Protos.v1.StudentService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddGrpcClient<StudentServiceClient>(c =>
{
    c.Address = new Uri("https://localhost:7253");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
