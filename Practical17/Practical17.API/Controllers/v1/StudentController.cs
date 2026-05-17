namespace Practical17.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/students")]
[ApiVersion("1.0")]
public class StudentController(IStudentService StudentService) : Controller
{

    [HttpGet]
    [Authorize(Roles = "Admin, NormalStudent")]
    public async Task<IActionResult> GetAllStudents()
    {
        var result = await StudentService.GetAll();
        return Ok(result);
    }

    [HttpGet("id/{StudentId}")]
    [Authorize(Roles = "Admin, NormalStudent")]
    public async Task<IActionResult> GetStudentById(string StudentId)
    {
        var result = await StudentService.GetById(StudentId);
        return Ok(result);
    }

    [HttpGet("gr/{gr}")]
    [Authorize(Roles = "Admin, NormalStudent")]
    public async Task<IActionResult> GetStudentByGR(long gr)
    {
        var result = await StudentService.GetByGRNumber(gr);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStudent([FromBody] StudentRequestDto dto)
    {
        var result = await StudentService.CreateStudent(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStudent(string id, [FromBody] StudentUpdateDto dto)
    {
        var result = await StudentService.UpdateStudent(Guid.Parse(id), dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStudent(string id)
    {
        var result = await StudentService.DeleteStudent(Guid.Parse(id));
        return Ok(result);
    }
}
