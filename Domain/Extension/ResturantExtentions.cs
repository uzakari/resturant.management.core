using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Extension;

public static class ResturantExtentions
{
    public static string HashedPassword(this string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedPassword= sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedPassword);

        }
    }

    public static bool ValidateEmailAddress(this string emailAddress)
    {
        var TextToValidate = emailAddress.Replace("-", "").Replace("#", "");
        var expression = new Regex("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");

        // test email address with expression 
        if (expression.IsMatch(TextToValidate))
            return true;
        return false;
    }
}
