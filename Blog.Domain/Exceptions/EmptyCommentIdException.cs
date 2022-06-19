using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyCommentIdException : BlogException
{
    const string _message = "Comment Id can't be empty.";
    public EmptyCommentIdException() : base(_message) { }
}
