using sippedes.Cores.Entities;
using sippedes.Cores.Model;

namespace sippedes.Cores.Security;

public interface IJwtUtils
{
    string GenerateToken(UserCredential credential);
    string GeneratePdfToken(PdfApiConf credential);
}