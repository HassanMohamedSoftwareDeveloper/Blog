using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Application.Exceptions
{
    public class CategoryAlreadyExistException : BlogException
    {
        const string _message = "Category ({0}) already exist.";

        public CategoryAlreadyExistException(CategoryName name) : base(string.Format(_message, name.Value)) { }
    }
}
