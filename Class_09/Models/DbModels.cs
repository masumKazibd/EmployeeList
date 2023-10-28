using Microsoft.EntityFrameworkCore;

namespace Class_09.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = default!;
        public string EmployeeStatus { get; set; } = default!;
        public decimal Salary { get; set; } = default!;
        public string PayBasis { get; set; } = default!;
        public string PositionTitle { get; set; } = default!;
    }
    public class EmpDbContext : DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set;}

    }
}
