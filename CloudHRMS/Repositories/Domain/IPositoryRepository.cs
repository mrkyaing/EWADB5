using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain
{
    public interface IPositoryRepository : IBaseRepository<PositionEntity>
    {
        IEnumerable<PositionViewModel> GetAll();
    }
}
