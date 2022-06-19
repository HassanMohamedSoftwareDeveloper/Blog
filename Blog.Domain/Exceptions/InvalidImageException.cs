using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class InvalidImageException : BlogException
{
    const string _message = "Invalid Image, Image must be like  *.jpg";
    public InvalidImageException() : base(_message)
    {
    }
}
