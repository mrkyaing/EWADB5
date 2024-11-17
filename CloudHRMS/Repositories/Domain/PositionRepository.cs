using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Repositories.Common;
using CloudHRMS.Utility;


namespace CloudHRMS.Repositories.Domain
{
    public class PositionRepository : BaseRepository<PositionEntity>, IPositoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PositionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._applicationDbContext = dbContext;
        }
        public IEnumerable<PositionViewModel> GetAll()
        {
            return _applicationDbContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Description = s.Description,
            }).ToList();
        }

    }
}
