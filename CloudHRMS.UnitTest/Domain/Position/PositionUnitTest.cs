using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories.Domain;
using CloudHRMS.Services;
using CloudHRMS.UnitOfWorks;
using Moq;
using Newtonsoft.Json;

namespace CloudHRMS.UnitTest.Domain.Position
{
    public class PositionUnitTest
    {
        //creating mock object for unit test of position services functions
        //step 1: create mock object of service
        public Mock<IPositionService> positionServiceMock = new Mock<IPositionService>();
        //step 2: create mock object of Unit Of Work
        public Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        //step 3: create mock object of Repository
        public Mock<IPositoryRepository> positionRepositoryMock = new Mock<IPositoryRepository>();

        [Fact]
        public void ShouldCreate()
        {
            // 1) Arrange
            var expectedPositionViewModel = new PositionViewModel()
            {
                Code = "HR",
                Description = "Human Resource MGR",
                Level = 3
            };
            var dbPositonEntity = new PositionEntity()
            {
                Code = expectedPositionViewModel.Code,
                Description = expectedPositionViewModel.Description,
                Level = expectedPositionViewModel.Level
            };
            positionRepositoryMock.Setup(r => r.Create(dbPositonEntity));//mock Position Repository
            unitOfWorkMock.Setup(u => u.PositionRepository).Returns(positionRepositoryMock.Object);//mock the Unit Of Work
            //2) Act
            var positionService = new PositionService(unitOfWorkMock.Object);//create  a service object with Unit Of work mock
            //3) Assert
            var actualResult = positionService.Create(expectedPositionViewModel);

            Assert.Equal(expectedPositionViewModel.Code, actualResult.Code);
            Assert.Equal(expectedPositionViewModel.Description, actualResult.Description);
            Assert.Equal(expectedPositionViewModel.Level, actualResult.Level);
        }

        [Fact]
        public void GetAll()
        {
            //Arrange
            IEnumerable<PositionViewModel> expectedResults = new List<PositionViewModel>()
            {
                new PositionViewModel{Id="xy1",Code="SE",Description="SE" },
                new PositionViewModel{Id="xyx1",Code="SE",Description="SE"}
            };

            IEnumerable<PositionEntity> positionEntities = new List<PositionEntity>()
            {
                new PositionEntity{Id="xy1",Code="SE",Description="SE"},
                new PositionEntity{Id="xyx1",Code="SE",Description="SE"}
            };
            positionRepositoryMock.Setup(r => r.GetAll()).Returns(positionEntities);
            unitOfWorkMock.Setup(u => u.PositionRepository).Returns(() => positionRepositoryMock.Object);
            //Act
            var positionService = new PositionService(unitOfWorkMock.Object);
            var actualResult = positionService.GetAll();
            //Assert
            var input = JsonConvert.SerializeObject(expectedResults);
            var output = JsonConvert.SerializeObject(actualResult);
            Assert.Equal(input, output);
        }
    }

}
