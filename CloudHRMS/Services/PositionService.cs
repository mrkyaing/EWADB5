using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CloudHRMS.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositoryRepository _positionRepository;
        public PositionService(IPositoryRepository positoryRepository)
        {
            _positionRepository = positoryRepository;
        }
        public void Create(PositionViewModel positionView)
        {
            _positionRepository.Create(positionView);
        }

        public void Delete(string Id)
        {
            _positionRepository.Delete(Id);
        }

        public PositionViewModel GetById(string Id)
        {
            return _positionRepository.GetById(Id);
        }

        public IList<PositionViewModel> ReteriveAll()
        {
            return _positionRepository.ReteriveAll();
        }

        public void Update(PositionViewModel positionView)
        {
            _positionRepository.Update(positionView);
        }
    }
}
