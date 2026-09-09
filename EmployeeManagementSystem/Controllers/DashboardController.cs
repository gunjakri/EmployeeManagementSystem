using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalEmployees = _context.Employees.Count();

            var totalDepartments = _context.Employees
                .Select(e => e.Department)
                .Distinct()
                .Count();

            var averageSalary = _context.Employees
                .Select(e => (decimal?)e.Salary)
                .Average() ?? 0;

            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.TotalDepartments = totalDepartments;
            ViewBag.AverageSalary = averageSalary;

            return View();
        }
    }
}