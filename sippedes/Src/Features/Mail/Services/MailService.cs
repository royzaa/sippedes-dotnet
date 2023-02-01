using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;

namespace sippedes.Features.Mail.Services;

public class MailService : IMailService
{
    public TransactionalEmailsApi _mailInstance;

    public MailService(TransactionalEmailsApi mailInstance)
    {
        _mailInstance = mailInstance;
    }
    public async Task<CreateSmtpEmail> SendMail(SendSmtpEmail model)
    {
        var result = await _mailInstance.SendTransacEmailAsync(model);
        return result;
    }
}