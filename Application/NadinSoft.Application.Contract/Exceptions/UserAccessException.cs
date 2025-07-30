namespace NadinSoft.Application.Contract.Exceptions;

public class UserAccessException: Exception
{
    public UserAccessException(string message) : base(message) { }
}