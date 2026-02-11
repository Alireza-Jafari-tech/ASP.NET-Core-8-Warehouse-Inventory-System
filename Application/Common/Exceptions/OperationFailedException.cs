namespace Application.Common.Exceptions
{
  public class OperationFailedException : Exception
  {
    public OperationFailedException(string error) : base(error)
    {

    }
  }
}