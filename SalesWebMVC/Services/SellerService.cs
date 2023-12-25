using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
  public class SellerService
  {
    private readonly SalesWebMVCContext _context;
    public SellerService() { }
    public SellerService(SalesWebMVCContext context)
    {
      _context = context;
    }

    public List<Seller> FindAll()
    {
      return _context.Seller.ToList();
    }
    public void Insert (Seller seller)
    {
      seller.Department = _context.Department.First(); // Gambiarra
      _context.Add(seller);
      _context.SaveChanges();
    }
  }
}
