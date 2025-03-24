using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using MyWebApp.Data; 
using System.Threading.Tasks;
using System.Linq;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("")]
    [Route("Home/Index")]
    public IActionResult Index()
    {
        return View();
    }
   [HttpPost]
[Route("Home/SubmitForm")]
public async Task<IActionResult> SubmitForm(Employee model)
{
    if (!ModelState.IsValid)
    {
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation error: {error.ErrorMessage}");
        }
        return View("Index", model);
    }

    _context.Employees.Add(model);
    await _context.SaveChangesAsync();

    return RedirectToAction("Display");
}

    [HttpGet]
    [Route("Home/Display")]
    public async Task<IActionResult> Display()
    {
        var employees = await _context.Employees.ToListAsync();
        return View(employees);
    }
}
