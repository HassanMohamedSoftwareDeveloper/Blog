using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyLikeIdException : BlogException
{
    const string _message = "Like Id can't be empty.";
    public EmptyLikeIdException() : base(_message) { }
}
