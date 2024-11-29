using CloudHRMS.Repositories.Domain;

namespace CloudHRMS.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IPositoryRepository PositionRepository { get; }
        void Commit();
        void RollBack();
    }
}
