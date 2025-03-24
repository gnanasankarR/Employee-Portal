using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApp.Pages
{
    public class DisplayModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DisplayModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Employee> Employees { get; set; } = new List<Employee>(); // ✅ Initialize list

        public async Task OnGetAsync()
        {
            Employees = await _context.Employees.ToListAsync(); // ✅ Use "Employees" instead of "Employee"
        }
    }
}
