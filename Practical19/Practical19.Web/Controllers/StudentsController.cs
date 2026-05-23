namespace Practical19.Web.Controllers;

public class StudentsController(IStudentService studentService) : Controller
{
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Index()
    {
        return View(await studentService.GetAllAsync());
    }

    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Details(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateStudentViewModel student)
    {
        if (ModelState.IsValid)
        {
            await studentService.CreateAsync(student);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateStudentViewModel student)
    {
        if (ModelState.IsValid)
        {
            var success = await studentService.UpdateAsync(id, student);
            if (!success) return NotFound();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await studentService.DeleteAsync(id);
        if (!success) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
