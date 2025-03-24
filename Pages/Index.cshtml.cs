using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Use a list for displaying employees but a single object for adding
        public List<Employee> Employees { get; set; } = new List<Employee>();

        [BindProperty]
        public Employee Employee { get; set; } = new Employee(); // ✅ Single Employee for Form Submission

        // ✅ Fetch Employees from Database on GET request
        public async Task OnGetAsync()
        {
            Employees = await _context.Employees.ToListAsync(); // ✅ Ensure consistent DbSet usage
        }

        // ✅ Handle Adding a Single Employee
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Employees = await _context.Employees.ToListAsync(); // Reload list on validation failure
                    return Page();
                }

                _context.Employees.Add(Employee);
                await _context.SaveChangesAsync();

                return RedirectToPage("Display"); // Redirect after saving
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while saving data.");
                Employees = await _context.Employees.ToListAsync();
                return Page();
            }
        }

        // ✅ Fix: Move Delete method inside the class and ensure DbSet is used correctly
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id); // ✅ Ensure correct table name
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index"); // ✅ Redirect after delete
        }
    }
}
