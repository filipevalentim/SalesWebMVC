namespace SalesWebMVC.Models
{
  using System.ComponentModel.DataAnnotations;

  public class Seller
  {
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome é obrigatório!")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do nome deve ser entre {2} e {1} caracteres")]
    public string Name { get; set; }
    [EmailAddress (ErrorMessage = "Coloque um email válido!")] 
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Display(Name = "Birth Date")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    //[Range(16, 100, ErrorMessage = "Sua idade tem que ser entre {1} e {2}")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Seu aniversário é obrigatório!")]
    public DateTime Birthdate { get; set; }
    [Display(Name = "Base Salary")]
    [Required(ErrorMessage = "{0} é obrigatório!")]
    [Range (100.0, 50000.0, ErrorMessage = "O salário tem que ser entre {1} e {2}")]
    [DisplayFormat(DataFormatString = "$ {0:F2}")]
    public double BaseSalary { get; set; }
    public Department? Department { get; set; }
    [Display(Name = "Department Id")]
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
