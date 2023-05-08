using TestAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Data;
public class ApplicationDbContext :DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

    {
        
    }
   public DbSet<Employee> Employees { get; set;}
    
}