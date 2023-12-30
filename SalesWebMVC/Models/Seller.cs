namespace SalesWebMVC.Models
{
  using System.ComponentModel.DataAnnotations;
  using Humanizer;

  public class Seller
  {
    public int Id { get; set; }
    public string Name { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Display(Name = "Birth Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [DataType(DataType.Date)]
    public DateTime Birthdate { get; set; }
    [Display(Name = "Base Salary")]
    [DisplayFormat(DataFormatString = "$ {0:F2}")]
    public double BaseSalary { get; set; }
    public Department? Department { get; set; }
    [Display(Name = "Department Id ")]
    public int DepartmentId { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
    public Seller() { }

    public Seller(int id, string name, string email, DateTime birthdate, double baseSalary, Department department)
    {
      Id = id;
      Name = name;
      Email = email;
      Birthdate = birthdate;
      BaseSalary = baseSalary;
      Department = department;
    }

    public void AddSales(SalesRecord salesRecord) => Sales.Add(salesRecord);
    public void RemoveSales(SalesRecord salesRecord) => Sales.Remove(salesRecord);
    public double TotalSales(DateTime initial, DateTime end)
    {
      return Sales
        .Where(sales => sales.Date >= initial && sales.Date <= end)
        .Sum(sales => sales.Amount);
    }
  }
}
