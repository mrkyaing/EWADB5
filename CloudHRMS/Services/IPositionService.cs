using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IPositionService
    {
        void Create(PositionViewModel positionView);
        IList<PositionViewModel> ReteriveAll();
        PositionViewModel GetById(string Id);
        void Update(PositionViewModel positionView);
        void Delete(string Id);
    }
}
