using System.Text;

namespace Shared.Domain.Extensions;

public static class StringExtentions
{

    public static string GenerateRandomString(this string str,int length) {
            
        string src = "abcdefghijklmnopqrstuvwxyz0123456789QWERTYUIOPASDFGHJKLZXCVBNM";
        var sb = new StringBuilder();
        Random RNG = new Random();
        for (var i = 0; i < length; i++)
        {
            var c = src[RNG.Next(0, src.Length)];
            sb.Append(c);
        }
        return sb.ToString();
        
    }
}