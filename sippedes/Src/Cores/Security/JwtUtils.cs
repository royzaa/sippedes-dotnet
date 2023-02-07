using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using sippedes.Cores.Entities;
using sippedes.Cores.Model;

namespace sippedes.Cores.Security;

public class JwtUtils : IJwtUtils
{
    private readonly IConfiguration _configuration;

    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserCredential credential)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

        // didalam payload
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtSettings:Audience"],
            Issuer = _configuration["JwtSettings:Issuer"],
            Expires = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
            IssuedAt = DateTime.Now,
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new (ClaimTypes.PrimarySid, credential.Id.ToString()),
                new (ClaimTypes.Email, credential.Email),
                new (ClaimTypes.Role, credential.Role.ERole.ToString())
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GeneratePdfToken(PdfApiConf credential)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(credential.SecretKey);

        tokenHandler.SetDefaultTimesOnTokenCreation = false;
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = credential.ApiKey,
            Expires = DateTime.Now.AddMinutes(credential.ExpireInMinutes),
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new ("sub", credential.Subject),
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}