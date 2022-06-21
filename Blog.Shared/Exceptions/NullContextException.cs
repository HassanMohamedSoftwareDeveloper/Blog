namespace Shared.Abstractions.Exceptions;

public sealed class NullContextException : BlogException
{
    const string _errorMessage = "Context can't be null.";
    public NullContextException() : base(_errorMessage) { }
}
