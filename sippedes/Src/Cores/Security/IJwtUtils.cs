using sippedes.Cores.Entities;

namespace sippedes.Cores.Security;

public interface IJwtUtils
{
    string GenerateToken(UserCredential credential);
}