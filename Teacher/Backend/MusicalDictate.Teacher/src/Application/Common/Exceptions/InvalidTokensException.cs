namespace Application.Common.Exceptions;

public class InvalidTokensException : Exception
{
    public InvalidTokensException() : base("Invalid access token or refresh token") { }
}