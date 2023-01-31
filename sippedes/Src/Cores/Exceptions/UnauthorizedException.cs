namespace livecode_net_advanced.Cores.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string? message) : base(message)
    {
    }
}