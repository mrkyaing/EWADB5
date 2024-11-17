using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IPositionService
    {
        PositionEntity Create(PositionViewModel positionView);
        IList<PositionViewModel> ReteriveAll();
        PositionViewModel GetById(string Id);
        PositionEntity Update(PositionViewModel positionView);
        void Delete(string Id);
        IEnumerable<PositionViewModel> GetAll();
    }
}
