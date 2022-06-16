namespace Blog.Helper;

public interface IPasswordGenerator
{
    string GeneratePassword(bool useLowerCase, bool useUpperCase, bool useNumbers, bool useSpecial, int passwordSize);
}
