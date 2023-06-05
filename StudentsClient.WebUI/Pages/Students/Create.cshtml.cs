using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudentsClient.Domain.Models;
using StudentsClient.Domain.Services;

namespace StudentsClient.WebUI.Pages.Students;

public class CreateModel : PageModel
{
    private readonly IStudentService _studentService;

    [BindProperty]
    public IFormFile StudentsFile { get; set; }
    public CreateModel(IStudentService studentService) => _studentService = studentService;
    public void OnGet()
    {
    }

    public async Task<RedirectToPageResult> OnPostAsync()
    {
        var path = @$"d:\{DateTime.Now.Ticks}.json";
        using (var stream = System.IO.File.Create(path))
            await StudentsFile.CopyToAsync(stream);
        var text = System.IO.File.ReadAllText(path);
        var students = JsonConvert.DeserializeObject<List<StudentCreateModel>>(text);
        await _studentService.Create(students);
        return RedirectToPage("Index");
    }
}
