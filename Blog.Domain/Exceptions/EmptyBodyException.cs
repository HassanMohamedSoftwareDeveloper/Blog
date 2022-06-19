using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyBodyException : BlogException
{
    const string _message = "Body can't be empty.";
    public EmptyBodyException() : base(_message) { }
}
