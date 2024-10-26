using CloudHRMS.DAO;
using CloudHRMS.Models.ReportModels;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public class ReportingService : IReportingService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReportingService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IList<DepartmentViewModel> GetAllDepartments()
        {
            return _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description
            }).ToList();
        }

        public IList<EmployeeViewModel> GetAllEmployees()
        {
            return _applicationDbContext.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel
            {
                No = s.No,
                FullName = s.FullName
            }).ToList();
        }

        public IList<EmployeeDetailReportModel> GetEmployeeDetailReport(string fromNo, string toNo, string departmentId)
        {
            IList<EmployeeDetailReportModel> employees = null;
            if (!departmentId.Equals("x"))
            {
                employees = (from e in _applicationDbContext.Employees
                             join p in _applicationDbContext.Positions
                             on e.PositionId equals p.Id
                             join d in _applicationDbContext.Departments
                             on e.DepartmentId equals d.Id
                             where e.IsActive && d.IsActive && p.IsActive && d.Id == departmentId
                             select new EmployeeDetailReportModel
                             {
                                 No = e.No,
                                 FullName = e.FullName,
                                 Email = e.Email,
                                 Phone = e.Phone,
                                 Address = e.Address,
                                 Salary = e.Salary,
                                 Gender = e.Gender,
                                 DOB = e.DOB,
                                 DOE = e.DOE,
                                 DOR = e.DOR,
                                 DepartmentInfo = d.Description,//to display UI List
                                 PositonInfo = p.Description//to display UI List
                             }).ToList();
            }
            else if (departmentId.Equals("x") && !string.IsNullOrEmpty(fromNo) && !string.IsNullOrEmpty(toNo))
            {
                employees = (from e in _applicationDbContext.Employees
                             join p in _applicationDbContext.Positions
                             on e.PositionId equals p.Id
                             join d in _applicationDbContext.Departments
                             on e.DepartmentId equals d.Id
                             where e.IsActive && d.IsActive && p.IsActive &&
                             (e.No.CompareTo(fromNo) >= 0 && e.No.CompareTo(toNo) <= 0)//s001 to s100
                             select new EmployeeDetailReportModel
                             {
                                 No = e.No,
                                 FullName = e.FullName,
                                 Email = e.Email,
                                 Phone = e.Phone,
                                 Address = e.Address,
                                 Salary = e.Salary,
                                 Gender = e.Gender,
                                 DOB = e.DOB,
                                 DOE = e.DOE,
                                 DOR = e.DOR,
                                 DepartmentInfo = d.Description,//to display UI List
                                 PositonInfo = p.Description//to display UI List
                             }).ToList();
            }
            return employees;
        }
    }
}
