using CloudHRMS.Models.Entities;
using CloudHRMS.Repositories.Common;

namespace CloudHRMS.Repositories.Domain
{
    public interface IPositoryRepository : IBaseRepository<PositionEntity>
    {
        IEnumerable<PositionEntity> GetAll();
    }
}
