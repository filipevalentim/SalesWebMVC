namespace SalesWebMVC.Services.Exceptions
{
  public class NotFoundException : ApplicationException
  {
    public NotFoundException(string message) : base(message)
    {

    }
  }
  public class DbConcurrencyException : ApplicationException
  {
    public DbConcurrencyException(string message) : base(message)
    {

    }
  }
}
