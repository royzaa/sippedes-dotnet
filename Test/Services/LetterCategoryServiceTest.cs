using Microsoft.AspNetCore.DataProtection.Repositories;
using Moq;
using sippedes.Cores.Entities;
using sippedes.Cores.Exceptions;
using sippedes.Cores.Repositories;
using sippedes.Features.Letters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
{
    public class LetterCategoryServiceTest
    {
        private readonly Mock<IRepository<LetterCategory>> _repoMock;
        private readonly Mock<IPersistence> _peristenceMock;
        private readonly ILetterCategoryService _letterCategoryService;

        public LetterCategoryServiceTest()
        {
            _repoMock = new Mock<IRepository<LetterCategory>>();
            _peristenceMock = new Mock<IPersistence>();
            _letterCategoryService = new LetterCategoryService(_repoMock.Object, _peristenceMock.Object);
        }

        [Fact]
        public async Task Should_ReturnCategoryLetter_When_CreateNewCategoryLetter()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            _repoMock.Setup(repo => repo.Save(categoryObj))
                        .ReturnsAsync(categoryObj);
            _peristenceMock.Setup(persis => persis.SaveChangesAsync());

            var category = await _letterCategoryService.Create(categoryObj);

            _repoMock.Verify(repo => repo.Save(It.IsAny<LetterCategory>()), Times.Once);
            _peristenceMock.Verify(persis => persis.SaveChangesAsync(), Times.Once);
            Assert.Equal(categoryObj.Category, category.Category);
        }

        [Fact]
        public async Task Should_ReturnCategory_When_GetById()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            _repoMock.Setup(repo => repo.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(categoryObj);

            var category = await _letterCategoryService.GetById(categoryObj.Id.ToString());

            _repoMock.Verify(repo => repo.FindById(It.IsAny<Guid>()), Times.Once);
            Assert.Equal(categoryObj.Id, category.Id);
                
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_GetById()
        {
            _repoMock.Setup(repo => repo.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(null as LetterCategory);

            await Assert.ThrowsAsync<NotFoundException>(async () => 
            await _letterCategoryService.GetById(Guid.Empty.ToString()));
        }

        [Fact]
        public async Task Should_ReturnCategory_When_UpdateCategory()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            _repoMock.Setup(repo => repo.Update(It.IsAny<LetterCategory>()))
                .Returns(categoryObj);

            _peristenceMock.Setup(repo => repo.SaveChangesAsync());

            var updateCategory = await _letterCategoryService.Update(categoryObj);

            _repoMock.Verify(repo => 
            repo.Update(It.IsAny<LetterCategory>()), Times.Once );

            Assert.Equal(categoryObj.Category, updateCategory.Category);
        }

        [Fact]
        public async Task Should_DoNothing_When_DeleteCategory()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            await Should_ReturnCategory_When_GetById();

            _repoMock.Setup(repo => repo.Delete(It.IsAny<LetterCategory>()));
            _peristenceMock.Setup(persis => persis.SaveChangesAsync());

            await _letterCategoryService.DeleteById(categoryObj.Id.ToString());

            _repoMock.Verify(repo => repo.Delete(It.IsAny<LetterCategory>()), Times.Once );
            _peristenceMock.Verify(persis => persis.SaveChangesAsync(), Times.Once );
        }

        [Fact]
        public async Task Should_ReturnCategory_When_GetByName()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            _repoMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<LetterCategory, bool>>>()))
                .ReturnsAsync(categoryObj);

            var category = await _letterCategoryService.GetByName(categoryObj.Category);

            _repoMock.Verify(repo => repo.Find(It.IsAny<Expression<Func<LetterCategory, bool>>>()), Times.Once);
            Assert.Equal(categoryObj.Category, category.Category);
        }

        [Fact]
        public async Task Should_ThrowNotFoundException_When_GetByName()
        {
            _repoMock.Setup(repo => repo.Find(It.IsAny<Expression<Func<LetterCategory, bool>>>()))
                .ReturnsAsync(null as LetterCategory);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _letterCategoryService.GetByName("Category Not Found"));
        }

        [Fact]
        public async Task Should_ReturnPageResponse_When_GetAllCategories()
        {
            var categoryObj = new LetterCategory
            {
                Id = Guid.NewGuid(),
                Category = "Pengantar SKCK"
            };

            var categories = new List<LetterCategory>
            {
                categoryObj
            };

            _repoMock.Setup(repo => repo.FindAll(It.IsAny<Expression<Func<LetterCategory, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(categories);
            _repoMock.Setup(repo => repo.Count()).ReturnsAsync(categories.Count);

            var result = await _letterCategoryService.GetAllCategories(1, 10);

            _repoMock.Verify(repo => repo.FindAll(It.IsAny<Expression<Func<LetterCategory, bool>>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _repoMock.Verify(repo => repo.Count(), Times.Once);
            Assert.Equal(categories.Count, result.TotalElement);
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(categories, result.Content);
        }
    }
}
