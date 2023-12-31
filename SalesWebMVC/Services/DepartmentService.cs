using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
  using Microsoft.EntityFrameworkCore;

  public class DepartmentService
  {
    private readonly SalesWebMVCContext _context;
    public DepartmentService(SalesWebMVCContext context)
    {
      _context = context;
    }
    public async Task<List<Department>> FindAllAsync()
    {
      return await _context.Department.OrderBy(x => x.Name).AsNoTracking().ToListAsync();
    }
  }
}
