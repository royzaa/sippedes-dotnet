using Microsoft.AspNetCore.Mvc;
using Moq;
using sippedes.Commons.Constants;
using sippedes.Cores.Entities;
using sippedes.Features.Letters.Controllers;
using sippedes.Features.Letters.Dto;
using sippedes.Features.Letters.Services;
using sippedes.Features.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class LetterControllerTest
    {
        private readonly Mock<ILetterService> _letterService;
        private readonly Mock<IUserCredentialService> _userCredentialService;
        private readonly LetterController _letterController;

        public LetterControllerTest()
        {
            _letterService = new Mock<ILetterService>();
            _userCredentialService = new Mock<IUserCredentialService>();
            _letterController = new LetterController(_letterService.Object, _userCredentialService.Object);
        }



        [Fact]
        public async Task Should_ReturnCreated_When_GetAllBussinessEvidenceLetter()
        {
            int page = 1;
            int size = 5;

            var result = await _letterController.GetAllBussinessEvidenceLetter(page, size);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task Should_ReturnCreated_When_GetAllPoliceRecord()
        {
            int page = 1;
            int size = 5;

            var result = await _letterController.GetAllPoliceRecord(page, size);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task Should_ReturnOk_When_GetBussinessEvidenceById()
        {
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "Test",
                NIK = "0987762398762223",
                Date = DateTime.Now,
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "9287317483",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            var letterRespon = new BussinessEvidenceLetterResponse
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

            _letterService.Setup(service => service.GetBussinessEvidenceLetterById(It.IsAny<string>()))
                .ReturnsAsync(letterRespon);

            var resultOk = await _letterController.GetBussinessEvidenceById(letterObj.Id.ToString());

            Assert.IsType<JsonResult>(resultOk);
        }

        [Fact]
        public async Task Should_ReturnOk_When_GetPoliceRecordById()
        {
            var letterObj = new Letter
            {
                Id = Guid.NewGuid(),
                FullName = "Test",
                NIK = "0987762398762223",
                Date = DateTime.Now,
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "9287317483",
                LetterCategory = new LetterCategory { Category = "Keterangan Usaha" },
                TrackingStatus = new TrackingStatus { Status = EStatus.SENT }
            };

            var letterRespon = new PoliceRecordLetterResponse
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

            _letterService.Setup(service => service.GetPoliceRecordLetterById(It.IsAny<string>()))
                .ReturnsAsync(letterRespon);

            var resultOk = await _letterController.GetPoliceRecordById(letterObj.Id.ToString());

            Assert.IsType<JsonResult>(resultOk);
        }

        [Fact]
        public async Task Should_ReturnSuccess_When_UpdateBussinessEvidenceLetter()
        {
            var bussinessEvidenceLetterRequest = new BussinessEvidenceLetterRequest
            {
                Name = "Test",
                TypeOfBusiness = "Test",
                LongBusiness = "Test",
                Address = "Test",
                PhoneNumber = "09876543132"
            };
            var letterRespon = new BussinessEvidenceLetterResponse
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

            _letterService.Setup(service => service.CreateBussinessEvidenceLetter(bussinessEvidenceLetterRequest, It.IsAny<string>()))
                .ReturnsAsync(letterRespon);

            var resultOk = await _letterController.UpdateBussinessEvidenceLetter(bussinessEvidenceLetterRequest, It.IsAny<string>());

            Assert.IsType<JsonResult>(resultOk);
        }

        [Fact]
        public async Task Should_ReturnSuccess_When_UpdatePoliceRecordLetter()
        {
            var policeRecordLetterRequest = new PoliceRecordLetterRequest
            {
                Job = "Test",
                Nescessity = "Test",
                MaritalStatus = "Test"
            };
            var letterRespon = new PoliceRecordLetterResponse
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

            _letterService.Setup(service => service.CreatePoliceRecordLetter(policeRecordLetterRequest, It.IsAny<string>()))
                .ReturnsAsync(letterRespon);

            var resultOk = await _letterController.UpdatePoliceRecordLetter(policeRecordLetterRequest, It.IsAny<string>());

            Assert.IsType<JsonResult>(resultOk);
        }

        [Fact]
        public async Task Should_ReturnOk_When_Delete()
        {
            var policeRecordLetterRequest = new PoliceRecordLetterRequest
            {
                Job = "Test",
                Nescessity = "Test",
                MaritalStatus = "Test"
            };
            var letterRespon = new PoliceRecordLetterResponse
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

            _letterService.Setup(service => service.CreatePoliceRecordLetter(policeRecordLetterRequest, It.IsAny<string>()))
                .ReturnsAsync(letterRespon);

            var result = await _letterController.Delete(It.IsAny<string>());

            _letterService.Verify(service => service.Delete(It.IsAny<string>()), Times.Once());
            //Assert.Equals(result.GetHashCode, 200);
        }


    }
}
