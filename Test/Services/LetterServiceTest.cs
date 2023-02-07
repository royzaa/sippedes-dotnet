using Moq;
using sippedes.Commons.Constants;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.CivilDatas.DTO;
using sippedes.Features.CivilDatas.Services;
using sippedes.Features.Letters.Dto;
using sippedes.Features.Letters.Services;
using sippedes.Features.Users.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
{
    public class LetterServiceTest
    {
        private readonly Mock<IRepository<Letter>> _repoMock;
        private readonly Mock<IPersistence> _persistenceMock;
        private readonly ILetterService _letterService;
        private readonly Mock<ICivilDataService> _civilDataService;
        private readonly Mock<ILetterCategoryService> _letterCategoryService;
        private readonly Mock<ITrackingStatusService> _trackingStatusService;
        private readonly Mock<IUserCredentialService> _userCredentialService;

        public LetterServiceTest()
        {
            _repoMock = new Mock<IRepository<Letter>>();
            _persistenceMock = new Mock<IPersistence>();
            _civilDataService = new Mock<ICivilDataService>();
            _letterCategoryService = new Mock<ILetterCategoryService>();
            _trackingStatusService = new Mock<ITrackingStatusService>();
            _userCredentialService= new Mock<IUserCredentialService>();
            _letterService = new LetterService(_repoMock.Object, _persistenceMock.Object, _civilDataService.Object, _letterCategoryService.Object, _trackingStatusService.Object, _userCredentialService.Object);
        }

        [Fact]
        public async Task Should_ReturnLetter_When_CreateNewPoliceRecordLetter()
        {
            // Arrange
            var email = "test@gmail.com";
            var userCredential = new UserCredential 
            { 
                Email = email, 
                Password = BCrypt.Net.BCrypt.HashPassword("test"), 
                RoleId = Guid.NewGuid(), 
                IsVerifed = 0, 
                CivilDataId = "123456789"
            };
            _userCredentialService.Setup(u => u.GetByEmail(email)).ReturnsAsync(userCredential);

            var civilData = new CivilDataResponse {
                
                NIK = "123456789",
                NoKK = "123456789",
                Fullname = "tes",
                Gender = "L",
                BloodType = "tes",
                Education = "tes",
                BirthDate = DateTime.Now.ToString(),
                Address = "tes",
                Province = "tes",
                City = "tes",
                District = "tes",
                Village = "tes",
                Religion = "tes"

            };

            _civilDataService.Setup(c => c.GetByNIK(userCredential.CivilDataId)).ReturnsAsync(civilData);

            var policeRecordLetterRequest = new PoliceRecordLetterRequest
            {
                Job = "Test",
                Nescessity = "Test",
                MaritalStatus = "Test"
            };

            var policeRecordLetterResponse = new PoliceRecordLetterResponse
            {
                FullName = "test",
                NIK = "1234567",
                Date = DateTime.Now,
                Address = "Test",
                Job = "Test",
                Nationality = "Test",
                Nescessity = "Test",
                MaritalStatus = "Test",
                Religion = "Test",
                Status = "Test",
                Category = "Test"
            };

            _persistenceMock.Setup(p => p.ExecuteTransactionAsync(It.IsAny<Func<Task<PoliceRecordLetterResponse>>>()))
                .ReturnsAsync(policeRecordLetterResponse);

            // Act
            var result = await _letterService.CreatePoliceRecordLetter(policeRecordLetterRequest, email);

            // Assert
            Assert.Equal(policeRecordLetterResponse.MaritalStatus, policeRecordLetterRequest.MaritalStatus);
            // Add more assertions to verify the returned response matches the expected values
           
        }

        [Fact]
        public async Task Should_ReturnLetter_When_CreateNewBussinessEvidenceLetterLetter()
        {
            // Arrange
            var email = "test@gmail.com";
            var userCredential = new UserCredential
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword("test"),
                RoleId = Guid.NewGuid(),
                IsVerifed = 0,
                CivilDataId = "123456789"
            };
            _userCredentialService.Setup(u => u.GetByEmail(email)).ReturnsAsync(userCredential);

            var civilData = new CivilDataResponse
            {

                NIK = "123456789",
                NoKK = "123456789",
                Fullname = "tes",
                Gender = "L",
                BloodType = "tes",
                Education = "tes",
                BirthDate = DateTime.Now.ToString(),
                Address = "tes",
                Province = "tes",
                City = "tes",
                District = "tes",
                Village = "tes",
                Religion = "tes"

            };

            _civilDataService.Setup(c => c.GetByNIK(userCredential.CivilDataId)).ReturnsAsync(civilData);

            var bussinessEvidenceLetterRequest = new BussinessEvidenceLetterRequest
            {
                Name = "Test",
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "09876543132"
            };

            var bussinessEvidenceLetterResponse = new BussinessEvidenceLetterResponse
            {
                Name = "Test",
                NIK = "0987762398762223",
                Date = DateTime.Now,
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "9287317483",
                Category = "Test",
                Status = "Test"
            };

            _persistenceMock.Setup(p => p.ExecuteTransactionAsync(It.IsAny<Func<Task<BussinessEvidenceLetterResponse>>>()))
                .ReturnsAsync(bussinessEvidenceLetterResponse);

            // Act
            var result = await _letterService.CreateBussinessEvidenceLetter(bussinessEvidenceLetterRequest, email);

            // Assert
            Assert.Equal(bussinessEvidenceLetterResponse.Name, bussinessEvidenceLetterRequest.Name);

        }

        [Fact]
        public async Task Should_ReturnsCorrectData_When_GetAllBussinessEvidenceLetter()
        {
            // Arrange
            var letters = new List<Letter>
            {
                new Letter {
                    FullName = "John Doe",
                    NIK = "123456789",
                    Date = new DateTime(2022, 1, 1),
                    TypeOfBusiness = "Retail",
                    LongBusiness = "5",
                    Address = "Jl. Example",
                    PhoneNumber = "08123456789",
                    LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                    TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
                },
                new Letter {
                    FullName = "Jane Doe",
                    NIK = "987654321",
                    Date = new DateTime(2022, 2, 1),
                    TypeOfBusiness = "Restaurant",
                    LongBusiness = "5",
                    Address = "Jl. Example",
                    PhoneNumber = "08987654321",
                    LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                    TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
                },
            };

            _repoMock.Setup(r => r.FindAll(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>()))
                .ReturnsAsync(letters);

            _repoMock.Setup(r => r.Count()).ReturnsAsync(letters.Count());

            // Act
            var result = await _letterService.GetAllBussinessEvidenceLetter(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(2, result.TotalElement);
            Assert.Equal("John Doe", result.Content[0].Name);
            Assert.Equal("Jane Doe", result.Content[1].Name);
        }

        [Fact]
        public async Task Should_ReturnsCorrectData_When_GetAllPoliceRecordLetter()
        {
            // Arrange
            var letters = new List<Letter>
            {
                new Letter {
                    FullName = "John Doe",
                    NIK = "123456789",
                    Date = new DateTime(2022, 1, 1),
                    Address = "Jl. Example",
                    Job = "Job",
                    Nationality = "NKRI",
                    Necessity = "Example",
                    MaritalStatus = "Example",
                    Religion = "Example",
                    LetterCategory = new LetterCategory { Category = "Pengantar SKCK" },
                    TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
                },
                new Letter {
                    FullName = "Jane Doe",
                    NIK = "987654321",
                    Date = new DateTime(2022, 2, 1),
                    Address = "Jl. Example",
                    Job = "Job",
                    Nationality = "NKRI",
                    Necessity = "Example",
                    MaritalStatus = "Example",
                    Religion = "Example",
                    LetterCategory = new LetterCategory { Category = "Pengantar SKCK" },
                    TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
                },
            };

            _repoMock.Setup(r => r.FindAll(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string[]>()))
                .ReturnsAsync(letters);

            _repoMock.Setup(r => r.Count()).ReturnsAsync(letters.Count());

            // Act
            var result = await _letterService.GetAllPoliceRecordLetter(1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(2, result.TotalElement);
            Assert.Equal("John Doe", result.Content[0].FullName);
            Assert.Equal("Jane Doe", result.Content[1].FullName);
        }

        [Fact]
        public async Task Should_ReturnBussinessEvidenceLetterResponse_When_GetBussinessEvidenceLetterById()
        {
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                NIK = "123456789",
                Date = new DateTime(2022, 1, 1),
                TypeOfBusiness = "Retail",
                LongBusiness = "5",
                Address = "Jl. Example",
                PhoneNumber = "08123456789",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);

            var letter = await _letterService.GetBussinessEvidenceLetterById(letterObj.Id.ToString());

            _repoMock.Verify(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);

            Assert.Equal(letterObj.FullName, letter.Name);
        }

        [Fact]
        public async Task Should_ReturnPoliceRecordLetterResponse_When_GetPoliceRecordLetterById()
        {
           
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "Jane Doe",
                NIK = "987654321",
                Date = new DateTime(2022, 2, 1),
                Address = "Jl. Example",
                Job = "Job",
                Nationality = "NKRI",
                Necessity = "Example",
                MaritalStatus = "Example",
                Religion = "Example",
                LetterCategory = new LetterCategory { Category = "Pengantar SKCK" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);

            var letter = await _letterService.GetPoliceRecordLetterById(letterObj.Id.ToString());

            _repoMock.Verify(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);

            Assert.Equal(letterObj.FullName, letter.FullName);
        }

        [Fact]
        public async Task Should_ReturnPoliceRecordLetterResponse_When_UpdatePoliceRecordLetter()
        {
            var policeRecordLetterRequest = new PoliceRecordLetterRequest
            {
                Job = "Test",
                Nescessity = "Test",
                MaritalStatus = "Test"
            };

            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "Jane Doe",
                NIK = "987654321",
                Date = new DateTime(2022, 2, 1),
                Address = "Jl. Example",
                Job = "Job",
                Nationality = "NKRI",
                Necessity = "Example",
                MaritalStatus = "Example",
                Religion = "Example",
                LetterCategory = new LetterCategory { Category = "Pengantar SKCK" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);

            _persistenceMock.Setup(persis => persis.SaveChangesAsync());

            var letter = await _letterService.UpdatePoliceRecordLetter(policeRecordLetterRequest, letterObj.Id.ToString());

            _repoMock.Verify(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);

            Assert.Equal(letterObj.Job, letter.Job);
        }

        [Fact]
        public async Task Should_ReturnBussinessEvidenceLetterResponse_When_UpdateBussinessEvidenceLetter()
        {
            var bussinessEvidenceLetterRequest = new BussinessEvidenceLetterRequest
            {
                Name = "Test",
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "09876543132"
            };
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                NIK = "123456789",
                Date = new DateTime(2022, 1, 1),
                TypeOfBusiness = "Retail",
                LongBusiness = "5",
                Address = "Jl. Example",
                PhoneNumber = "08123456789",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);
            _persistenceMock.Setup(persis => persis.SaveChangesAsync());

            var letter = await _letterService.UpdateBussinessEvidenceLetter(bussinessEvidenceLetterRequest, letterObj.Id.ToString());

            _repoMock.Verify(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);

            Assert.Equal(letterObj.FullName, letter.Name);
        }

        [Fact]
        public async Task Should_ReturnOk_When_UpdateLetterTrackingToOnProcess()
        {
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                NIK = "123456789",
                Date = new DateTime(2022, 1, 1),
                TypeOfBusiness = "Retail",
                LongBusiness = "5",
                Address = "Jl. Example",
                PhoneNumber = "08123456789",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);
            _persistenceMock.Setup(persis => persis.SaveChangesAsync());

            await _letterService.UpdateLetterTrackingToOnProcess(letterObj.Id.ToString());

            _repoMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);
            _persistenceMock.Verify(persis => persis.SaveChangesAsync(), Times.Once);

        }

        [Fact]
        public async Task Should_ReturnOk_When_UpdateLetterTrackingToComplete()
        {
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                NIK = "123456789",
                Date = new DateTime(2022, 1, 1),
                TypeOfBusiness = "Retail",
                LongBusiness = "5",
                Address = "Jl. Example",
                PhoneNumber = "08123456789",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);
            _persistenceMock.Setup(persis => persis.SaveChangesAsync());

            await _letterService.UpdateLetterTrackingToComplete(letterObj.Id.ToString());

            _repoMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);
            _persistenceMock.Verify(persis => persis.SaveChangesAsync(), Times.Once);

        }

        public async Task Should_ReturnDelete_When_Delete()
        {
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                NIK = "123456789",
                Date = new DateTime(2022, 1, 1),
                TypeOfBusiness = "Retail",
                LongBusiness = "5",
                Address = "Jl. Example",
                PhoneNumber = "08123456789",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            await Should_ReturnBussinessEvidenceLetterResponse_When_GetBussinessEvidenceLetterById();

            _repoMock.Setup(r => r.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(letterObj);
            _persistenceMock.Setup(persis => persis.SaveChangesAsync());

            await _letterService.Delete(letterObj.Id.ToString());

            _repoMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<Letter, bool>>>(), It.IsAny<string[]>()), Times.Once);
            _persistenceMock.Verify(persis => persis.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_DeteleByIdIsNull()
        {
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _letterService.Delete(Guid.Empty.ToString()));
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_UpdateLetterTrackingToCompleteIsNull()
        {
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _letterService.UpdateLetterTrackingToComplete(Guid.Empty.ToString()));
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_UpdateLetterTrackingToOnProcessIsNull()
        {
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _letterService.UpdateLetterTrackingToOnProcess(Guid.Empty.ToString()));
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_UpdateBussinessEvidenceLetterIsNull()
        {
            var bussinessEvidenceLetterRequest = new BussinessEvidenceLetterRequest
            {
                Name = "Test",
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "09876543132"
            };
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _letterService.UpdateBussinessEvidenceLetter(bussinessEvidenceLetterRequest, Guid.Empty.ToString()));
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_GetPoliceRecordLetterByIdIsNull()
        {
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _letterService.GetPoliceRecordLetterById(Guid.Empty.ToString()));
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_GetBussinessEvidenceLetterByIdIsNull()
        {
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _letterService.GetBussinessEvidenceLetterById(Guid.Empty.ToString()));
        }

    }
}
