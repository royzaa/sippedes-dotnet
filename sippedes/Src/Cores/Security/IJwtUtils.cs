using livecode_net_advanced.Cores.Entities;

namespace livecode_net_advanced.Cores.Security;

public interface IJwtUtils
{
    string GenerateToken(User user);
}