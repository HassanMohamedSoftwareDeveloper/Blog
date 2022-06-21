using Blog.Domain.ValueObjects;
using Shared.Abstractions.Exceptions;

namespace Blog.Application.Exceptions
{
    public class InvalidCategoryIdException : BlogException
    {
        const string _message = "Invalid or not exist Category Id ({0})";

        public InvalidCategoryIdException(CategoryId id) : base(string.Format(_message, id.Value)) { }
    }
}
