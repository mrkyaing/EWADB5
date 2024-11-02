using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
    public interface IPositoryRepository
    {
        PositionEntity Create(PositionViewModel positionView);
        IList<PositionViewModel> ReteriveAll();
        PositionViewModel GetById(string Id);
        PositionEntity Update(PositionViewModel positionView);
        void Delete(string Id);
    }
}
