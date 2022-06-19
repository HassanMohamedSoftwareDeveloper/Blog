using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyTagException : BlogException
{
    private const string _message = "Tag can't be empty.";
    public EmptyTagException() : base(_message) { }
}
