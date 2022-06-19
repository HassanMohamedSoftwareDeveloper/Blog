using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyTitleException : BlogException
{
    const string _message = "Title can't be empty.";
    public EmptyTitleException() : base(_message) { }
}
