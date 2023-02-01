using livecode_net_advanced.Commons.Utils;
using sib_api_v3_sdk.Model;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.Mail.Services;
using sippedes.Features.Otp.Dto;

namespace sippedes.Features.Otp.Services;

public class OtpService : IOtpService
{
    private readonly IRepository<Cores.Entities.Otp> _repository;
    private readonly IMailService _mailService;
    private readonly IPersistence _persistence;

    public OtpService(IRepository<Cores.Entities.Otp> repository, IPersistence persistence, IMailService mailService)
    {
        _repository = repository;
        _persistence = persistence;
        _mailService = mailService;
    }

    public async Task<CreateSmtpEmail> SendOtp(SendOtpReqDto payload)
    {
        var randomCode = GeneratorUtils.GenerateRondomNumeric();

        var otpResult = await _persistence.ExecuteTransactionAsync<Cores.Entities.Otp>(async () =>
        {
            var otp = await _repository.Save(new Cores.Entities.Otp
            {
                user_id = payload.UserId,
                OtpCode = randomCode,
                IsExpired = 0,
                LastExpiration = DateTime.Now.AddMinutes(10),
                CreatedAt = DateTime.Now
            });
            return otp;
        });

        var result = await SendMailOtp(payload, randomCode);

        return result;
    }

    public async Task<VerifyOtpResDto> VerifyOtp(VerifyOtpReqDto payload)
    {
        var validOtp = await GetValidOtp(payload);

        if (validOtp.OtpCode != payload.OtpCode)
            return new VerifyOtpResDto
            {
                Success = false
            };
        return  new VerifyOtpResDto
        {
            Success = true
        };
    }

    private async Task<Cores.Entities.Otp> GetValidOtp(VerifyOtpReqDto payload)
    {
        var otp = await _repository.Find(otp => otp.IsExpired == 0 && otp.user_id.Equals(payload.UserId));

        if (otp is null)
        {
            throw new NotFoundException("verifikasi otp gagal");
        }
        return otp;
    }

    private async Task<CreateSmtpEmail> SendMailOtp(SendOtpReqDto payload, int randomCode)
    {
        return await _mailService.SendMail(new SendSmtpEmail
        {
            To = new List<SendSmtpEmailTo>
            {
                new SendSmtpEmailTo
                {
                    Email = payload.Email
                }
            },
            Sender = new SendSmtpEmailSender
            {
                Email = "casplatohosin@gmail.com",
                Name = "Sippedes Official"
            },
            TemplateId = 1,
            Params = new
            {
                // FNAME = "",
                SMS = randomCode
            }
        });
    }
}