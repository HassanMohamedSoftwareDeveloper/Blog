namespace Shared.Abstractions.Exceptions;

public abstract class BlogException : Exception
{
    public BlogException(string message) : base(message) { }
}
