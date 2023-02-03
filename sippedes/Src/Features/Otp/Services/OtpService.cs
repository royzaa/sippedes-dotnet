using Hangfire;
using sib_api_v3_sdk.Model;
using sippedes.Commons.Utils;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Mail.Services;
using sippedes.Features.Otp.Dto;
using Task = System.Threading.Tasks.Task;

namespace sippedes.Features.Otp.Services;

public class OtpService : IOtpService
{
    private readonly IRepository<Cores.Entities.Otp> _repository;
    private readonly IMailService _mailService;
    private readonly ICivilDataService _civilDataService;
    private readonly IPersistence _persistence;

    public OtpService(IRepository<Cores.Entities.Otp> repository, IPersistence persistence, IMailService mailService,
        ICivilDataService civilDataService)
    {
        _repository = repository;
        _persistence = persistence;
        _mailService = mailService;
        _civilDataService = civilDataService;
    }

    public async Task<CreateSmtpEmail> SendOtp(SendOtpReqDto payload)
    {
        var randomCode = GeneratorUtils.GenerateRandomNumeric();

        var otpResult = await _persistence.ExecuteTransactionAsync<Cores.Entities.Otp>(async () =>
        {
            var otp = await _repository.Save(new Cores.Entities.Otp
            {
                user_id = payload.UserId,
                OtpCode = randomCode,
                IsExpired = 0,
                LastExpiration = DateTime.Now.AddMinutes(5),
                CreatedAt = DateTime.Now
            });
            await _persistence.SaveChangesAsync();
            return otp;
        });

        // TODO: 
        // Get Civil Data by UserId

        BackgroundJob.Schedule(() => UpdateExpiredOtp(otpResult), DateTime.Now.AddMinutes(5));

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
        return new VerifyOtpResDto
        {
            Success = true
        };
    }

    private async Task<Cores.Entities.Otp> GetValidOtp(VerifyOtpReqDto payload)
    {
        var otp = await _repository.Find(otp => otp.IsExpired == 0 && otp.user_id.Equals(payload.UserId));

        if (otp is null)
        {
            throw new NotFoundException("otp expired/not match");
        }

        return otp;
    }

    private async Task<CreateSmtpEmail> SendMailOtp(SendOtpReqDto payload, int randomCode)
    {
        var sendSmtpEmailTo = new SendSmtpEmailTo(email: payload.Email);

        return await _mailService.SendMail(new SendSmtpEmail
        {
            To = new List<SendSmtpEmailTo>
            {
                sendSmtpEmailTo
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

    public async Task UpdateExpiredOtp(Cores.Entities.Otp otp)
    {
        otp.IsExpired = 1;
        await _persistence.ExecuteTransactionAsync(async () =>
        {
            var expiredOtp = _repository.Update(otp);
            await _persistence.SaveChangesAsync();

            return expiredOtp;
        });
    }
}