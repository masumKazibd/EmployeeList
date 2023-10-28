using Class_09.Models;
using Microsoft.AspNetCore.Mvc;

namespace Class_09.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmpDbContext _context;
        public HomeController(EmpDbContext _context)
        {
            this._context = _context;
        }
        public async Task<IActionResult> Index(string usertext, string sortOrder, int page)
        {
            ViewBag.sword = usertext;
            ViewBag.sortParam = string.IsNullOrEmpty(sortOrder) ? "desc_name" : "";
            ViewBag.sortSalary = sortOrder == "sal_asc" ? "sal_desc" : "sal_desc";
            IQueryable<Employee>emp = _context.Employees;
            if (!string.IsNullOrEmpty(usertext))
            {
                usertext = usertext.ToLower();
                emp=emp.Where(e=>e.EmployeeName.ToLower().Contains(usertext) || e.EmployeeStatus.ToLower().Contains(usertext)|| e.PositionTitle.ToLower().Contains(usertext));
            }
            switch (sortOrder)
            {
                case "desc_name":
                    emp = emp.OrderByDescending(e => e.EmployeeName);
                    break;
                case "sal_asc":
                    emp = emp.OrderBy(e=>e.Salary); break;
                case "sal_desc":
                    emp = emp.OrderByDescending(e=>e.Salary); break;
                default:
                    emp= emp.OrderBy(e=> e.EmployeeName);
                    break;
            }
            ViewBag.count = emp.Count();
            if (page <= 0) page = 1;
            int pageSize = 10;
            ViewBag.pSize = pageSize;
            return View(await PaginatedList<Employee>.CreateAsync(emp,page,pageSize));
        }
    }
}
