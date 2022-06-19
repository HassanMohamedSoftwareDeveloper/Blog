using Shared.Abstractions.Exceptions;

namespace Blog.Domain.Exceptions;

public class EmptyCategoryNameException : BlogException
{
    const string _message = "Category Name can't be empty.";
    public EmptyCategoryNameException() : base(_message) { }
}
