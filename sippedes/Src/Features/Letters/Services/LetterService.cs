using Microsoft.EntityFrameworkCore;
using sippedes.Commons.Constants;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.CivilDatas.DTO;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Letters.Dto;
using sippedes.Features.Users.Services;
using System.Linq.Expressions;
using System.Security.Claims;



namespace sippedes.Features.Letters.Services
{
    public class LetterService : ILetterService
    {
        private readonly IRepository<Letter> _repository;
        private readonly IPersistence _persistence;
        private readonly ICivilDataService _civilDataService;
        private readonly ILetterCategoryService _letterCategoryService;
        private readonly ITrackingStatusService _trackingStatusService;
        private readonly IUserCredentialService _userCredentialService;

        public LetterService(IRepository<Letter> repository, IPersistence persistence, ICivilDataService civilDataService, ILetterCategoryService letterCategoryService, ITrackingStatusService trackingStatusService, IUserCredentialService userCredentialService)
        {
            _persistence = persistence;
            _repository = repository;
            _civilDataService = civilDataService;
            _letterCategoryService = letterCategoryService;
            _trackingStatusService = trackingStatusService;
            _userCredentialService = userCredentialService;
        }
        public async Task<BussinessEvidenceLetterResponse> CreateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request, string email)
        {
            var user = await _userCredentialService.GetByEmail(email);

            var civil = await _civilDataService.GetByNIK(user.CivilDataId);
            
            var bussinessEvidenceLetterResponse = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var status = await _trackingStatusService.GetOrSave(EStatus.SENT);
                var category = await _letterCategoryService.GetByName("Keterangan Usaha");

                var letter = new Letter
                {
                    FullName = request.Name,
                    UserCredentialId = user.Id,
                    NIK = civil.NIK,
                    Date = DateTime.Now,
                    TypeOfBusiness = request.TypeOfBusiness,
                    LongBusiness = request.LongBusiness,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    TrackingStatus = status,
                    LetterCategory = category
                };

                var saveLetter = await _repository.Save(letter);
                await _persistence.SaveChangesAsync();

                return new BussinessEvidenceLetterResponse
                {
                    Name = saveLetter.FullName,
                    NIK = saveLetter.NIK,
                    Date = saveLetter.Date,
                    TypeOfBusiness = saveLetter.TypeOfBusiness,
                    LongBusiness = saveLetter.LongBusiness,
                    Address = saveLetter.Address,
                    PhoneNumber = saveLetter.PhoneNumber,
                    Category = saveLetter.LetterCategory.Category,
                    Status = saveLetter.TrackingStatus.Status.ToString()
                };

            });

            return bussinessEvidenceLetterResponse;
        }

        public async Task<PoliceRecordLetterResponse> CreatePoliceRecordLetter(PoliceRecordLetterRequest request, string email)
        {
            var user = await _userCredentialService.GetByEmail(email);

            var civil = await _civilDataService.GetByNIK(user.CivilDataId);

            var policeRecordLetterResponse = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var status = await _trackingStatusService.GetOrSave(EStatus.SENT);
                var category = await _letterCategoryService.GetByName("Pengantar SKCK");

                var letter = new Letter
                {
                    FullName = civil.Fullname,
                    UserCredentialId = user.Id,
                    NIK = civil.NIK,
                    Date = DateTime.Now,
                    Address = civil.Address,
                    Job = request.Job,
                    Nationality = "Negara Kebangsaan Republik Indonesia",
                    Necessity = request.Nescessity,
                    MaritalStatus = request.MaritalStatus,
                    Religion = civil.Religion,
                    TrackingStatus = status,
                    LetterCategory = category
                };

                var saveLetter = await _repository.Save(letter);
                await _persistence.SaveChangesAsync();

                return new PoliceRecordLetterResponse
                {
                    FullName = saveLetter.FullName,
                    NIK = saveLetter.NIK,
                    Date = saveLetter.Date,
                    Address = saveLetter.Address,
                    Job = saveLetter.Job,
                    Nationality = saveLetter.Nationality,
                    Nescessity = saveLetter.Necessity,
                    MaritalStatus = saveLetter.MaritalStatus,
                    Religion = saveLetter.Religion,
                    Status = saveLetter.TrackingStatus.Status.ToString(),
                    Category = saveLetter.LetterCategory.Category
                };
            });

            return policeRecordLetterResponse;
        }

        public async Task<PageResponse<BussinessEvidenceLetterResponse>> GetAllBussinessEvidenceLetter(int page, int size)
        {
            var letterData = await _repository.FindAll(
                x => x.LetterCategory.Category == "Keterangan Usaha" && x.TrackingStatus != null,
                includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus) }
                );

            var letterDataResponse = letterData.Select(data => new BussinessEvidenceLetterResponse
            {
                Name = data.FullName,
                NIK = data.NIK,
                Date = data.Date,
                TypeOfBusiness = data.TypeOfBusiness,
                LongBusiness = data.LongBusiness,
                Address = data.Address,
                PhoneNumber = data.PhoneNumber,
                Category = data.LetterCategory.Category,
                Status = data.TrackingStatus.Status.ToString()
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _repository.Count() / (decimal)size);

            PageResponse<BussinessEvidenceLetterResponse> pageResponse = new()
            {
                Content = letterDataResponse,
                TotalPages = totalPages,
                TotalElement = letterDataResponse.Count()
            };
            return pageResponse;
        }

        public async Task<PageResponse<PoliceRecordLetterResponse>> GetAllPoliceRecordLetter(int page, int size)
        {
            var letterData = await _repository.FindAll(
                x => x.LetterCategory.Category == "Pengantar SKCK" && x.TrackingStatus != null,
                page: page,
                size: size,
                includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus) }
                );

            var letterDataResponse = letterData.Select(data => new PoliceRecordLetterResponse
            {
                FullName = data.FullName,
                NIK = data.NIK,
                Date = data.Date,
                Address = data.Address,
                Job = data.Job,
                Nationality = data.Nationality,
                Nescessity = data.Necessity,
                MaritalStatus = data.MaritalStatus,
                Religion = data.Religion,
                Status = data.TrackingStatus.Status.ToString(),
                Category = data.LetterCategory.Category
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _repository.Count() / (decimal)size);

            PageResponse<PoliceRecordLetterResponse> pageResponse = new()
            {
                Content = letterDataResponse,
                TotalPages = totalPages,
                TotalElement = letterDataResponse.Count()
            };
            return pageResponse;
        }

        public async Task<BussinessEvidenceLetterResponse> GetBussinessEvidenceLetterById(string id, string email)
        {
            try
            {
                var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );

                if (letterData is null) throw new NotFoundException("Id Not Found");
                if (letterData.LetterCategory.Category != "Keterangan Usaha") throw new NotFoundException("Not Found");

                var user = letterData.UserCredential.Email.Equals(email);
                if (user is false) throw new NotFoundException("Not Found");

                BussinessEvidenceLetterResponse response = new()
                {
                    Name = letterData.FullName,
                    NIK = letterData.NIK,
                    Date = letterData.Date,
                    TypeOfBusiness = letterData.TypeOfBusiness,
                    LongBusiness = letterData.LongBusiness,
                    Address = letterData.Address,
                    PhoneNumber = letterData.PhoneNumber,
                    Category = letterData.LetterCategory.Category,
                    Status = letterData.TrackingStatus.Status.ToString()
                };

                return response;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<PoliceRecordLetterResponse> GetPoliceRecordLetterById(string id, string email)
        {
            try
            {
                var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );

                if (letterData is null) throw new NotFoundException("Id Not Found");
                if (letterData.LetterCategory.Category != "Pengantar SKCK") throw new NotFoundException("Not Found");

                var user = letterData.UserCredential.Email.Equals(email);
                if (user is false) throw new NotFoundException("Not Found");

                PoliceRecordLetterResponse response = new()
                {
                    FullName = letterData.FullName,
                    NIK = letterData.NIK,
                    Date = letterData.Date,
                    Address = letterData.Address,
                    Job = letterData.Job,
                    Nationality = letterData.Nationality,
                    Nescessity = letterData.Necessity,
                    MaritalStatus = letterData.MaritalStatus,
                    Religion = letterData.Religion,
                    Status = letterData.TrackingStatus.Status.ToString(),
                    Category = letterData.LetterCategory.Category
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task<BussinessEvidenceLetterResponse> UpdateBussinessEvidenceLetter(BussinessEvidenceLetterRequest request, string id, string email)
        {
            var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );

            if (letterData is null) throw new NotFoundException("Id Not Found");

            var category = letterData.LetterCategory.Category.Equals("Keterangan Usaha");
            if (category is false) throw new NotFoundException("Not Found 1");

            var user = letterData.UserCredential.Email.Equals(email);
            if (user is false) throw new NotFoundException("Not Found 2");

            var status = letterData.TrackingStatus.Status.Equals(EStatus.SENT);
            if (status is false) throw new NotFoundException($"Data cannot be changed. Your letter has been {letterData.TrackingStatus.Status.ToString()}");

            letterData.FullName = request.Name;
            letterData.TypeOfBusiness = request.TypeOfBusiness;
            letterData.LongBusiness = request.LongBusiness;
            letterData.Address = request.Address;
            letterData.PhoneNumber = request.PhoneNumber;
            await _persistence.SaveChangesAsync();

            BussinessEvidenceLetterResponse response = new()
            {
                Name = letterData.FullName,
                NIK = letterData.NIK,
                Date = letterData.Date,
                TypeOfBusiness = letterData.TypeOfBusiness,
                LongBusiness = letterData.LongBusiness,
                Address = letterData.Address,
                PhoneNumber = letterData.PhoneNumber,
                Category = letterData.LetterCategory.Category,
                Status = letterData.TrackingStatus.Status.ToString()
            };

            return response;
        }

        public async Task UpdateLetterTrackingToOnProcess(string id)
        {
            var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );

            if (letterData is null) throw new NotFoundException("Id Not Found");

            var status = await _trackingStatusService.GetOrSave(EStatus.ONPROCESS);

            letterData.TrackingStatus = status;
            await _persistence.SaveChangesAsync();
        }

        public async Task UpdateLetterTrackingToComplete(string id)
        {
            var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );

            if (letterData is null) throw new NotFoundException("Id Not Found");

            var status = await _trackingStatusService.GetOrSave(EStatus.COMPLETE);

            letterData.TrackingStatus = status;
            await _persistence.SaveChangesAsync();
        }

        public async Task<PoliceRecordLetterResponse> UpdatePoliceRecordLetter(PoliceRecordLetterRequest request, string id, string email)
        {
            var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] { nameof(Letter.LetterCategory), nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );

            if (letterData is null) throw new NotFoundException("Id Not Found");

            var category = letterData.LetterCategory.Category.Equals("Pengantar SKCK");
            if (category is false) throw new NotFoundException("Not Found 1");

            var user = letterData.UserCredential.Email.Equals(email);
            if (user is false) throw new NotFoundException("Not Found 2");

            var status = letterData.TrackingStatus.Status.Equals(EStatus.SENT);
            if (status is false) throw new NotFoundException($"Data cannot be changed. Your letter has been {letterData.TrackingStatus.Status.ToString()}");

            letterData.Job = request.Job;
            letterData.Necessity = request.Nescessity;
            letterData.MaritalStatus = request.MaritalStatus;
            await _persistence.SaveChangesAsync();

            PoliceRecordLetterResponse response = new()
            {
                FullName = letterData.FullName,
                NIK = letterData.NIK,
                Date = letterData.Date,
                Address = letterData.Address,
                Job = letterData.Job,
                Nationality = letterData.Nationality,
                Nescessity = letterData.Necessity,
                MaritalStatus = letterData.MaritalStatus,
                Religion = letterData.Religion,
                Status = letterData.TrackingStatus.Status.ToString(),
                Category = letterData.LetterCategory.Category
            };

            return response;
        }

        public async Task Delete(string id, string email)
        {
            var letterData = await _repository.Find(
                    let => let.Id.Equals(Guid.Parse(id)),
                    includes: new string[] {nameof(Letter.TrackingStatus), nameof(Letter.UserCredential) }
                    );
            if (letterData is null) throw new NotFoundException("Id Not Found");

            var user = letterData.UserCredential.Email.Equals(email);
            if (user is false) throw new NotFoundException("Not Found");

            var status = letterData.TrackingStatus.Status.Equals(EStatus.SENT);
            if (status is false) throw new NotFoundException($"Data cannot be deleted. Your letter has been {letterData.TrackingStatus.Status.ToString()}");

            _repository.Delete(letterData);
            await _persistence.SaveChangesAsync();
        }
    }
}

