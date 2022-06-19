using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyUserIdException : BlogException
{
    const string _message = "User Id can't be empty.";
    public EmptyUserIdException() : base(_message) { }
}
