using CloudHRMS.DAO;
using CloudHRMS.Repositories.Domain;

namespace CloudHRMS.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly IPositoryRepository _positoryRepository;
        public IPositoryRepository PositionRepository
        {

            get
            {
                return _positoryRepository ?? new PositionRepository(_dbContext);
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void RollBack()
        {
            _dbContext.Dispose();
        }
    }
}
