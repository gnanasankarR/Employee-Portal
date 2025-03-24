using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApp.Pages;

public class Recieveddata : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string EmployeeName { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string DateOfBirth { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string EmployeeID { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string DateOfJoining { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string Email { get; set; } = string.Empty;

    public void OnGet()
    {
        // Page load logic (if needed)
    }
}