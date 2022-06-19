using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyImageException : BlogException
{
    const string _message = "Image can't be empty.";
    public EmptyImageException() : base(_message) { }
}
