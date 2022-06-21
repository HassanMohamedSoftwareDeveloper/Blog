using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Application.Exceptions;

public class InvalidPostIdException : BlogException
{
    const string _message = "Invalid or not exist Post Id ({0})";

    public InvalidPostIdException(PostId id) : base(string.Format(_message, id.Value)) { }
}
