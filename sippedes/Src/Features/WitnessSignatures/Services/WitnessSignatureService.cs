using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Src.Features.WitnessSignatures.DTO;

namespace sippedes.Src.Features.WitnessSignatures.Services
{
    public class WitnessSignatureService : IWitnessSignatureService
    {
        private readonly IRepository<WitnessSignature> _witnessRepository;
        private readonly IPersistence _persistence;

        public WitnessSignatureService(IRepository<WitnessSignature> witnessRepository, IPersistence persistence)
        {
            _witnessRepository = witnessRepository;
            _persistence = persistence;
        }

        public async Task<WitnessSignatureResponse> CreateNewWitnessSignature(WitnessSignature payload)
        {
            var validate = await _witnessRepository.Find(witness => witness.WitnessName.Equals(payload.WitnessName));
            if (validate != null) throw new Exception("WitnessName Already Exist");
            try
            {
                var result = await _persistence.ExecuteTransactionAsync(async () =>
                {
                    var save = await _witnessRepository.Save(payload);
                    await _persistence.SaveChangesAsync();
                    return save;
                });
                WitnessSignatureResponse response = new()
                {
                    Id = result.Id,
                    WitnessName = result.WitnessName,
                    Signature = result.Signature,
                    Occupation = result.Occupation,
                    IsActive = result.IsActive,
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task Delete(string id)
        {
            var witnessData = await _witnessRepository.Find(witness => witness.Id.Equals(id));
            if (witnessData is null) throw new NotFoundException("Witness Id Not Found");
            _witnessRepository.Delete(witnessData);
            await _persistence.SaveChangesAsync();
        }

        public async Task<PageResponse<WitnessSignatureResponse>> GetAllWitnessSignature(string? name, int page, int size)
        {
            var witnessData = await _witnessRepository.FindAll(
                criteria: w => EF.Functions.Like(w.WitnessName, $"{name}"),
                page: page,
                size: size
                );

            var witnessSignatureResponse = witnessData.Select(data => new WitnessSignatureResponse
            {
                Id = data.Id,
                WitnessName = data.WitnessName,
                Signature = data.Signature,
                Occupation = data.Occupation,
                IsActive = data.IsActive,
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _witnessRepository.Count() / (decimal)size);
            PageResponse<WitnessSignatureResponse> pageResponse = new()
            {
                Content = witnessSignatureResponse,
                TotalPages = totalPages,
                TotalElement = witnessSignatureResponse.Count()
            };
            return pageResponse;
        }

        public async Task<WitnessSignatureResponse> GetWitnessSignatureById(string id)
        {
            try
            {
                var witnessData = await _witnessRepository.Find(witness => witness.Id.Equals(id));
                if (witnessData is null) throw new NotFoundException("Witness Id Not Found");

                WitnessSignatureResponse response = new()
                {
                    Id = witnessData.Id,
                    WitnessName = witnessData.WitnessName,
                    Signature = witnessData.Signature,
                    Occupation = witnessData.Occupation,
                    IsActive = witnessData.IsActive,
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<WitnessSignatureResponse> UpdateWitnessSignature(WitnessSignature payload)
        {
            var witnessData = await _witnessRepository.Find(criteria: w => w.WitnessName == payload.WitnessName);
            if (witnessData is null) throw new NotFoundException("WitnessName Not Found");

            witnessData.WitnessName = payload.WitnessName;
            witnessData.Signature = payload.Signature;
            witnessData.Occupation = payload.Occupation;
            witnessData.IsActive = payload.IsActive;
            await _persistence.SaveChangesAsync();

            WitnessSignatureResponse response = new()
            {
                Id = witnessData.Id,
                WitnessName = witnessData.WitnessName,
                Signature = witnessData.Signature,
                Occupation = witnessData.Occupation,
                IsActive = witnessData.IsActive,
            };
            return response;
        }
    }
}
