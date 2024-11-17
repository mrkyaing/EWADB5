using CloudHRMS.DAO;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories.Domain;
using CloudHRMS.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CloudHRMS.UnitTest.Domain.Position
{
    public class PositionUnitTest
    {
        //mock the service
        private Mock<IPositionService> positionServiceMock = new Mock<IPositionService>();
        //mock the repository
        private Mock<IPositoryRepository> positionRepositoryMock = new Mock<IPositoryRepository>();
        [Fact]
        public void ShouldCreate()
        {
            // Arrange
            var InputPositionViewModel = new PositionViewModel()
            {
                Code = "HR",
                Description = "HR"
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var applicationDb = new ApplicationDbContext(options))
            {

                //var positionService = new PositionService(positionRepositoryMock.Object);
                var positionRepository = new PositionRepository(applicationDb);

                // Act & Assert
                //positionService.Create(InputPositionViewModel);
                //var outputPositionEntity = positionRepository.Create(InputPositionViewModel);
                //Assert.Equal(InputPositionViewModel.Code, outputPositionEntity.Code);
                //Assert.Equal(InputPositionViewModel.Description, outputPositionEntity.Description);
            }
        }
    }

}
