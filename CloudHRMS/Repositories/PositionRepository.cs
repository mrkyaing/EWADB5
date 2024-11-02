using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;

namespace CloudHRMS.Repositories
{
    public class PositionRepository : IPositoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PositionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public PositionEntity Create(PositionViewModel positionViewModel)
        {
            try
            {
                PositionEntity positionEntity = new PositionEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = positionViewModel.Code,
                    Description = positionViewModel.Description,
                    Level = positionViewModel.Level,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true
                };
                _applicationDbContext.Positions.Add(positionEntity);
                _applicationDbContext.SaveChanges();
                return positionEntity;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Delete(string Id)
        {
            try
            {
                var existingPosition = _applicationDbContext.Positions.Where(w => w.Id == Id).SingleOrDefault();
                if (existingPosition != null)
                {
                    existingPosition.IsActive = false;
                    existingPosition.IpAddress = NetworkHelper.GetMachinePublicIP();
                    _applicationDbContext.Update(existingPosition);
                    _applicationDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public PositionViewModel GetById(string Id)
        {
            return _applicationDbContext.Positions
                            .Where(p => p.Id == Id)
                            .Select(s => new PositionViewModel
                            {
                                Id = s.Id,
                                Code = s.Code,
                                Description = s.Description,
                                Level = s.Level
                            }).SingleOrDefault();
        }

        public IList<PositionViewModel> ReteriveAll()
        {
            IList<PositionViewModel> positions = _applicationDbContext.Positions
                                                .Where(w => w.IsActive)
                                                .Select(s => new PositionViewModel
                                                {
                                                    Id = s.Id,
                                                    Code = s.Code,
                                                    Description = s.Description,
                                                    Level = s.Level

                                                }).ToList();
            return positions;
        }

        public PositionEntity Update(PositionViewModel positionViewModel)
        {
            try
            {
                var existingPositionEntity = _applicationDbContext.Positions.Find(positionViewModel.Id);
                existingPositionEntity.Code = positionViewModel.Code;
                existingPositionEntity.Description = positionViewModel.Description;
                existingPositionEntity.Level = positionViewModel.Level;
                existingPositionEntity.CreatedAt = DateTime.Now;
                existingPositionEntity.CreatedBy = "System";
                existingPositionEntity.IsActive = true;
                existingPositionEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
                _applicationDbContext.Positions.Update(existingPositionEntity);
                _applicationDbContext.SaveChanges();
                return existingPositionEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
