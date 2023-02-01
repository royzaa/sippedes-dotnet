using sib_api_v3_sdk.Model;

namespace sippedes.Features.Mail.Services;

public interface IMailService
{
    public Task<CreateSmtpEmail> SendMail(SendSmtpEmail model);
}