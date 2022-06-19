using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class InvalidCommentId : BlogException
{
    const string _message = "Comment Id is invalid or not exist.";
    public InvalidCommentId() : base(_message) { }
}
