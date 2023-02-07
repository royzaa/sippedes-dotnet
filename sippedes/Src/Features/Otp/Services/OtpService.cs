using Hangfire;
using sib_api_v3_sdk.Model;
using sippedes.Commons.Utils;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Mail.Services;
using sippedes.Features.Otp.Dto;
using sippedes.Features.Users.Services;
using Task = System.Threading.Tasks.Task;

namespace sippedes.Features.Otp.Services;

public class OtpService : IOtpService
{
    private readonly IRepository<Cores.Entities.Otp> _repository;
    private readonly IMailService _mailService;
    private readonly IPersistence _persistence;
    private readonly IUserCredentialService _userCredentialService;

    public OtpService(IRepository<Cores.Entities.Otp> repository, IPersistence persistence, IMailService mailService,
        ICivilDataService civilDataService, IUserCredentialService userCredentialService)
    {
        _repository = repository;
        _persistence = persistence;
        _mailService = mailService;
        _userCredentialService = userCredentialService;
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
        var user = await _userCredentialService.GetById(payload.UserId.ToString());
        payload.Name = user.CivilData?.Fullname;

        Console.WriteLine(user.CivilData?.Fullname);

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
        var user = await _userCredentialService.GetById(payload.UserId);
        await _userCredentialService.VerifyAccount(user);
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
                FIRSTNAME = payload.Name,
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