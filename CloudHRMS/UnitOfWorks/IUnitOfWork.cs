using CloudHRMS.Repositories.Domain;

namespace CloudHRMS.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IPositoryRepository PositoryRepository { get; }
        void Commit();
        void RollBack();
    }
}
