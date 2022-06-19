using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyMessageException : BlogException
{
    const string _message = "Message can't be empty.";
    public EmptyMessageException() : base(_message) { }
}
