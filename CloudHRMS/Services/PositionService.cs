using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.UnitOfWorks;
using CloudHRMS.Utility;

namespace CloudHRMS.Services
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsActive = true
                };
                _unitOfWork.PositionRepository.Create(positionEntity);
                _unitOfWork.Commit();
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
                var existingPosition = _unitOfWork.PositionRepository.Getby(w => w.Id == Id).SingleOrDefault();
                if (existingPosition != null)
                {
                    existingPosition.IsActive = false;
                    existingPosition.IpAddress = NetworkHelper.GetMachinePublicIP();
                    _unitOfWork.PositionRepository.Update(existingPosition);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {
            }
        }

        public PositionViewModel GetById(string Id)
        {
            return _unitOfWork.PositionRepository.Getby(p => p.Id == Id)
                            .Select(s => new PositionViewModel
                            {
                                Id = s.Id,
                                Code = s.Code,
                                Description = s.Description,
                                Level = s.Level
                            }).SingleOrDefault();
        }

        public IEnumerable<PositionViewModel> GetAll()
        {
            return _unitOfWork.PositionRepository.GetAll().Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description
            });
        }

        public IList<PositionViewModel> ReteriveAll()
        {
            IList<PositionViewModel> positions = _unitOfWork.PositionRepository.Getby(w => w.IsActive)
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
                var existingPositionEntity = _unitOfWork.PositionRepository.Getby(w => w.Id == positionViewModel.Id).SingleOrDefault();
                existingPositionEntity.Code = positionViewModel.Code;
                existingPositionEntity.Description = positionViewModel.Description;
                existingPositionEntity.Level = positionViewModel.Level;
                existingPositionEntity.UpdatedAt = DateTime.Now;
                existingPositionEntity.UpdatedBy = "System";
                existingPositionEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
                _unitOfWork.PositionRepository.Update(existingPositionEntity);
                _unitOfWork.Commit();
                return existingPositionEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
