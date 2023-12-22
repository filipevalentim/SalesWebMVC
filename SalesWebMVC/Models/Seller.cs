﻿namespace SalesWebMVC.Models
{
  public class Seller
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public double BaseSalary { get; set; }
    public Department Department { get; set; }
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