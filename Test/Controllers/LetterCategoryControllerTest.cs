using Microsoft.AspNetCore.Mvc;
using Moq;
using sippedes.Cores.Dto;
using sippedes.Cores.Entities;
using sippedes.Features.Letters.Controllers;
using sippedes.Features.Letters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class LetterCategoryControllerTest
    {
        private readonly Mock<ILetterCategoryService> _mockLetterCategoryService;
        private readonly LetterCategoryController _letterCategoryController;

        public LetterCategoryControllerTest()
        {
            _mockLetterCategoryService = new Mock<ILetterCategoryService>();
            _letterCategoryController = new LetterCategoryController(_mockLetterCategoryService.Object);
        }

        [Fact]
        public async Task Should_ReturnLetterCategory_When_GetLetterCategoryById()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            _mockLetterCategoryService.Setup(service => service.Create(It.IsAny<LetterCategory>()))
                .ReturnsAsync(categoryObj);

            var resultOk = await _letterCategoryController.GetLettterCategoryById(categoryObj.Id.ToString());

            Assert.IsType<JsonResult>(resultOk);
        }

        [Fact]
        public async Task Should_ReturnCategories_When_GetAllCategories()
        {
            int page = 1;
            int size = 5;

            var result = await _letterCategoryController.GetAllCategories(page, size);

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task Should_ReturnCategories_When_CreateNewCategoryLetter()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            var result = await _letterCategoryController.CreateNewCategoryLetter(categoryObj);

            Assert.IsType<CreatedResult>(result);
        }
    }
}
