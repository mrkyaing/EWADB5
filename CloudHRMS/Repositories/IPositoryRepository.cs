using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Repositories
{
    public interface IPositoryRepository
    {
        void Create(PositionViewModel positionView);
        IList<PositionViewModel> ReteriveAll();
        PositionViewModel GetById(string Id);
        void Update(PositionViewModel positionView);
        void Delete(string Id);
    }
}
