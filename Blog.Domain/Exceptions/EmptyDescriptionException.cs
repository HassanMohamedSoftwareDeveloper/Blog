using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyDescriptionException : BlogException
{
    const string _message = "Description can't be empty.";
    public EmptyDescriptionException() : base(_message) { }
}
