using sippedes.Cores.Extensions;

namespace sippedes.Commons.Utils;

public static class GeneratorUtils
{
    private static readonly Random _random = new Random();

    public static string GenerateRondomAlphaNumeric()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 7)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
    
    public static int GenerateRandomNumeric()
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, 7)
            .Select(s => s[_random.Next(s.Length)]).ToArray()).Substring(0,5).ParseToInt();
    }
}