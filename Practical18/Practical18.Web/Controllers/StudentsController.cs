namespace Practical18.Web.Controllers;

public class StudentsController(IStudentService studentService) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await studentService.GetAllAsync());
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    public IActionResult Create()
    {
        return View();
    }

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
    public async Task<IActionResult> Edit(Guid id)
    {
        var student = await studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

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

    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await studentService.DeleteAsync(id);
        if (!success) return NotFound();
        return RedirectToAction(nameof(Index));
    }
}
