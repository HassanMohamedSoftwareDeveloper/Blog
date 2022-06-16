namespace Blog.Helper;

public class PasswordGenerator : IPasswordGenerator
{
    const string LOWER_CASE = "abcdefghijklmnopqrstuvwxyz";
    const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string NUMBERS = "0123456789";
    const string SPECIALS = @"!@£$%^&*()#€.-{},";

    public string GeneratePassword(bool useLowerCase, bool useUpperCase, bool useNumbers, bool useSpecial, int passwordSize)
    {
        char[] password = new char[passwordSize];
        string charSet = "";
        int counter;

        if (useLowerCase) charSet += LOWER_CASE;
        if (useUpperCase) charSet += UPPER_CAES;
        if (useNumbers) charSet += NUMBERS;
        if (useSpecial) charSet += SPECIALS;

        Random random = new();
        for (counter = 0; counter < passwordSize; counter++)
            password[counter] = charSet[random.Next(charSet.Length - 1)];

        return String.Join(null, password);
    }
}
