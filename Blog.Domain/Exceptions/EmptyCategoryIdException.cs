using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyCategoryIdException : BlogException
{
    const string _message = "Category Id can't be empty.";
    public EmptyCategoryIdException() : base(_message) { }
}
