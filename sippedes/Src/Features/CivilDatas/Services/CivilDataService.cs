using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Src.Features.CivilDatas.DTO;

namespace sippedes.Src.Features.CivilDatas.Services
{
    public class CivilDataService : ICivilDataService
    {
        private readonly IRepository<CivilData> _civilRepository;
        private readonly IPersistence _persistence;

        public CivilDataService(IRepository<CivilData> civilRepository, IPersistence persistence)
        {
            _civilRepository = civilRepository;
            _persistence = persistence;
        }

        public async Task<CivilDataResponse> CreateNewCivil(CivilData payload)
        {
            var civilData = await _civilRepository.Find(
                civil => civil.NIK.Equals(payload.NIK));
            if (civilData != null) throw new Exception("NIK is Already Exist");
            try
            {
                var result = await _persistence.ExecuteTransactionAsync(async () =>
                {
                    var save = await _civilRepository.Save(payload);
                    await _persistence.SaveChangesAsync();
                    return save;
                });

                CivilDataResponse response = new()
                {
                    NIK = result.NIK,
                    NoKK = result.NoKK,
                    Fullname = result.Fullname,
                    Gender = result.Gender,
                    BloodType = result.BloodType,
                    Education = result.Education,
                    BirthDate = result.BirthDate,
                    Address = result.Address,
                    Province = result.Province,
                    City = result.City,
                    District = result.District,
                    Village = result.Village,
                    Religion = result.Religion
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteByNIK(string id)
        {
            var civilData = await _civilRepository.Find(civil => civil.NIK.Equals(id));
            if (civilData is null) throw new NotFoundException("NIK Not Found");
            _civilRepository.Delete(civilData);
            await _persistence.SaveChangesAsync();

        }

        public async Task<PageResponse<CivilDataResponse>> GetAllCivil(string? id, int page, int size)
        {
            var civilData = await _civilRepository.FindAll(
                criteria: c => EF.Functions.Like(c.NIK, $"{id}"),
                page: page,
                size: size
                );


            var civilDataResponse = civilData.Select(data => new CivilDataResponse
            {
                NIK = data.NIK,
                NoKK = data.NoKK,
                Fullname = data.Fullname,
                Gender = data.Gender,
                BloodType = data.BloodType,
                Education = data.Education,
                BirthDate = data.BirthDate,
                Address = data.Address,
                Province = data.Province,
                City = data.City,
                District = data.District,
                Village = data.Village,
                Religion = data.Religion
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _civilRepository.Count() / (decimal)size);
            PageResponse<CivilDataResponse> pageResponse = new()
            {
                Content = civilDataResponse,
                TotalPages = totalPages,
                TotalElement = civilDataResponse.Count()
            };
            return pageResponse;

        }

        public async Task<CivilDataResponse> GetByNIK(string id)
        {
            try
            {
                var civilData = await _civilRepository.Find(civil => civil.NIK.Equals(id));
                if (civilData is null) throw new NotFoundException("NIK Not Found");

                CivilDataResponse response = new()
                {
                    NIK = civilData.NIK,
                    NoKK = civilData.NoKK,
                    Fullname = civilData.Fullname,
                    Gender = civilData.Gender,
                    BloodType = civilData.BloodType,
                    Education = civilData.Education,
                    BirthDate = civilData.BirthDate,
                    Address = civilData.Address,
                    Province = civilData.Province,
                    City = civilData.City,
                    District = civilData.District,
                    Village = civilData.Village,
                    Religion = civilData.Religion
                };
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task<CivilDataResponse> Update(CivilData payload)
        {
            var civilData = await _civilRepository.Find(criteria: c => c.NIK == payload.NIK);
            if (civilData is null) throw new NotFoundException("NIK Not Found");

            civilData.NoKK = payload.NoKK;
            civilData.Fullname = payload.Fullname;
            civilData.Gender = payload.Gender;
            civilData.BloodType = payload.BloodType;
            civilData.Education = payload.Education;
            civilData.BirthDate = payload.BirthDate;
            civilData.Address = payload.Address;
            civilData.Province = payload.Province;
            civilData.City = payload.City;
            civilData.District = payload.District;
            civilData.Village = payload.Village;
            civilData.Religion = payload.Religion;
            await _persistence.SaveChangesAsync();

            CivilDataResponse response = new()
            {
                NIK = civilData.NIK,
                NoKK = civilData.NoKK,
                Fullname = civilData.Fullname,
                Gender = civilData.Gender,
                BloodType = civilData.BloodType,
                Education = civilData.Education,
                BirthDate = civilData.BirthDate,
                Address = civilData.Address,
                Province = civilData.Province,
                City = civilData.City,
                District = civilData.District,
                Village = civilData.Village,
                Religion = civilData.Religion
            };
            return response;
        }
    }
}
