using Castle.Core.Resource;
using Moq;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.CivilDatas.DTO;
using sippedes.Features.CivilDatas.Services;
using System.Linq.Expressions;

namespace Test.Services
{
    public class CivilDataServiceTest
    {
        private readonly Mock<IRepository<CivilData>> _mockRepo;
        private readonly Mock<IPersistence> _mockPersistence;
        private readonly ICivilDataService _civilDataService;

        public CivilDataServiceTest()
        {
            _mockRepo = new Mock<IRepository<CivilData>>();
            _mockPersistence = new Mock<IPersistence>();
            _civilDataService = new CivilDataService(_mockRepo.Object, _mockPersistence.Object);
        }

        [Fact]
        public async Task Should_ReturnCivil_When_CreateNewCivil()
        {
            var civilData = new CivilData
            {
                NIK = "12345678901234567",
                NoKK = "98765432109876543",
                Fullname = "John Doe",
                Gender = "L",
                BloodType = "A",
                Education = "S1",
                BirthDate = "01 January 2001",
                Address = "Jl. Example",
                Province = "Example Province",
                City = "Example City",
                District = "Example District",
                Village = "Example Village",
                Religion = "Example Religion"
            };
            _mockRepo.Setup(repository => repository.Find(It.IsAny<Expression<Func<CivilData, bool>>>())).ReturnsAsync(null as CivilData);
            _mockPersistence.Setup(persistence => persistence.ExecuteTransactionAsync(It.IsAny<Func<Task<CivilData>>>()))
                .Callback<Func<Task<CivilData>>>((f) => { f(); })
                .ReturnsAsync(civilData);

            var result = await _civilDataService.CreateNewCivil(civilData);

            _mockPersistence.Verify(persistence => persistence.ExecuteTransactionAsync(It.IsAny<Func<Task<CivilData>>>()), Times.Once);
            Assert.Equal(civilData.Fullname, result.Fullname);
        }

        [Fact]
        public async Task Should_ReturnThrowNotFoundException_When_GetByNIK()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Find(It.IsAny<Expression<Func<CivilData, bool>>>()))
                .ReturnsAsync((CivilData)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _civilDataService.GetByNIK("invalidNIK"));
        }

        [Fact]
        public async Task Should_ReturnAllCivil_When_GetAllCivil()
        {
            var civilData = new List<CivilData>
            {
                new()
                {
                    NIK = "12345678901234567",
                    NoKK = "98765432109876543",
                    Fullname = "John Doe",
                    Gender = "L",
                    BloodType = "A",
                    Education = "S1",
                    BirthDate = "01 January 2001",
                    Address = "Jl. Example",
                    Province = "Example Province",
                    City = "Example City",
                    District = "Example District",
                    Village = "Example Village",
                    Religion = "Example Religion"
                },
                new()
                {
                    NIK = "1263425673819254",
                    NoKK = "98765432109876543",
                    Fullname = "Alexa",
                    Gender = "P",
                    BloodType = "O",
                    Education = "S1",
                    BirthDate = "01 February 2004",
                    Address = "Jl. Example",
                    Province = "Example Province",
                    City = "Example City",
                    District = "Example District",
                    Village = "Example Village",
                    Religion = "Example Religion"
                }
            };
            _mockRepo.Setup(repository => repository.FindAll(
                It.IsAny<Expression<Func<CivilData, bool>>>(),
                It.IsAny<int>(),
                It.IsAny<int>()
                )).ReturnsAsync(civilData);

            var totalPage = 0;

            _mockRepo.Setup(repository => repository.Count())
            .ReturnsAsync(totalPage);

            totalPage = (int)Math.Ceiling((decimal)totalPage / 5);

            var pageResponse = new PageResponse<CivilDataResponse>
            {
                Content = civilData.Select(data => new CivilDataResponse 
                {
                    NIK = data.NIK,
                    NoKK = data.NoKK,
                    Fullname = data.Fullname,
                    Gender = data.Gender,
                    BirthDate = data.BirthDate,
                    Address = data.Address,
                    Province = data.Province,
                    City = data.City,
                    District = data.District,
                    Village = data.Village,
                    Religion = data.Religion,
                }).ToList(),
                TotalPages = totalPage,
                TotalElement = civilData.Count
            };

            var civilDataResponse = await _civilDataService.GetAllCivil(1, 5);

            _mockRepo.Verify(repository => repository.FindAll(
                It.IsAny<Expression<Func<CivilData, bool>>>(),
                It.IsAny<int>(),
                It.IsAny<int>()
                ), Times.Once);
            _mockRepo.Verify(repository => repository.Count(), Times.Once());

            Assert.Equal(pageResponse.Content.Count, civilDataResponse.Content.Count);
        }

        [Fact]
        public async Task Should_ReturnNothing_When_DeleteById()
        {
            /* Arrange */
            var civilData = new CivilData()
            {
                NIK = "12345678901234567",
                NoKK = "98765432109876543",
                Fullname = "John Doe",
                Gender = "L",
                BloodType = "A",
                Education = "S1",
                BirthDate = "01 January 2001",
                Address = "Jl. Example",
                Province = "Example Province",
                City = "Example City",
                District = "Example District",
                Village = "Example Village",
                Religion = "Example Religion"
            };
            _mockRepo.Setup(repo => repo.Find(It.IsAny<Expression<Func<CivilData, bool>>>()))
                .ReturnsAsync(civilData);

            /* Act */
            await _civilDataService.DeleteByNIK("12345678901234567");

            /* Assert */
            _mockRepo.Verify(repository => repository.Delete(civilData), Times.Once);
            _mockPersistence.Verify(c => c.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_ReturnThrowNotFoundException_When_DeleteByNIK()
        {
            _mockRepo.Setup(repository => repository.Find(It.IsAny<Expression<Func<CivilData, bool>>>()))
                .ReturnsAsync((CivilData)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _civilDataService.DeleteByNIK("Invalid NIK"));
        }
    }
}
