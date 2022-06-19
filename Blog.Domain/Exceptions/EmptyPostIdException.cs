using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyPostIdException : BlogException
{
    const string _message = "Post Id can't be empty.";
    public EmptyPostIdException() : base(_message) { }
}
