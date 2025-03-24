using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Threading.Tasks;

namespace MyWebApp.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee? Employee { get; set; }  // Make it nullable to avoid warnings

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Employee = await _context.Employees.FindAsync(id);

            if (Employee == null)
            {
                return NotFound();  // Handle not found case
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Employee == null)  
            {
                return NotFound(); // Prevent null issues
            }

            _context.Attach(Employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Employee updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.EmployeeID == Employee.EmployeeID))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToPage("Index");
        }
    }
}
