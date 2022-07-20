namespace Blog.Infrastructure.Results;

public class AuthenticationResult
{
    public bool Succeeded { get; set; }
    public List<string> Errors { get; set; }
}
