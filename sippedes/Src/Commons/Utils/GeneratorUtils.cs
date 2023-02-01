using sippedes.Cores.Extensions;

namespace livecode_net_advanced.Commons.Utils;

public static class GeneratorUtils
{
    private static readonly Random _random = new Random();

    public static string GenerateRondomAlphaNumeric()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 7)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
    
    public static int GenerateRondomNumeric()
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[_random.Next(s.Length)]).ToArray()).ParseToInt();
    }
}