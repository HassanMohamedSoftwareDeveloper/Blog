using Shared.Abstractions.Exceptions;

namespace Blog.Application.Exceptions;

public class CategoryHasPostsException : BlogException
{
    const string _message = "Category can't deleted because It has Posts.";
    public CategoryHasPostsException() : base(_message) { }
}
