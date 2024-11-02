using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;
using CloudHRMS.Services;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace CloudHRMS.UnitTest.Domain.Position
{
    public class PositionUnitTest
    {
        //mock the service
        private Mock<IPositionService> positionServiceMock = new Mock<IPositionService>();
        //mock the repository
        private Mock<IPositoryRepository> positionRepositoryMock = new Mock<IPositoryRepository>();

        private Mock<ApplicationDbContext> applicationDbContext = new Mock<ApplicationDbContext>();

        [Fact]
        public void ShouldCreate()
        {
            //1) Arrange
            string Id = "12345";
            var InputPositionViewModel = new PositionViewModel()
            {
                Id = Id,
                Code = "HR",
                Description = "HR"
            };
            var positionEntity = new PositionEntity()
            {
                Id = Id,
                Code = "HR",
                Description = "HR"
            };
            positionServiceMock.Setup(s => s.Create(InputPositionViewModel));
            positionRepositoryMock.Setup(r => r.Create(InputPositionViewModel)).Returns(positionEntity);
            applicationDbContext.Setup(a => a.Positions);
            //2) Act
            var positionService = positionServiceMock.Object;
            var positionRepository = new PositionRepository(applicationDbContext.Object);
            //3) Assert
            positionService.Create(InputPositionViewModel);
            var expectedPositionEntity = positionRepository.Create(InputPositionViewModel);
            Assert.Equal(InputPositionViewModel.Id, expectedPositionEntity.Id);

        }
    }
}
