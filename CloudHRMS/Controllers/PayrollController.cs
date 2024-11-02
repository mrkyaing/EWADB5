using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class PayrollController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PayrollController(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IActionResult List()
        {
            //mapper.map<source,destination>(dataList);
            //_mapper.Map<List<PayrollEntity>,List<PayrollViewModel>>(_applicationDbContext.Payrolls.ToList())
            List<PayrollViewModel> payrollViews = (from p in _applicationDbContext.Payrolls
                                                   join e in _applicationDbContext.Employees
                                                   on p.EmployeeId equals e.Id
                                                   join d in _applicationDbContext.Departments
                                                   on e.DepartmentId equals d.Id
                                                   select new PayrollViewModel
                                                   {
                                                       Id = p.Id,
                                                       FromDate = p.FromDate,
                                                       ToDate = p.ToDate,
                                                       EmployeeInfo = e.No + "/" + e.FullName,
                                                       BasicSalary = e.Salary,
                                                       DepartmentInfo = d.Code + "/" + d.Description,
                                                       GrossPay = p.GrossPay,
                                                       NetPay = p.NetPay,
                                                       AttendanceDays = p.AttendanceDays,
                                                       AttendanceDeduction = p.AttendanceDeduction,
                                                       Allowance = p.Allowance,
                                                       Deduction = p.Deduction,
                                                       IncomeTax = p.IncomeTax,
                                                   }).ToList();
            return View(payrollViews);
        }
        public IActionResult PayrollProcess()
        {
            ViewBag.Employees = (_applicationDbContext.Employees.ToList());
            ViewBag.Departments = (_applicationDbContext.Departments.ToList());
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "HR")]
        public IActionResult PayrollProcess(PayrollProcessViewModel ui)
        {
            try
            {
                List<AttendanceMasterCalculatedData> attendanceMasterCalculatedData = new List<AttendanceMasterCalculatedData>();
                if (ui.DepartmentId != null)
                {
                    //HR,01-03-2024 to 31-03-2024
                    List<AttendanceMasterEntity> attendances = _applicationDbContext.AttendanceMasters.Where(w => w.DepartmentId == ui.DepartmentId && (w.AttendanceDate <= ui.ToDate)).OrderBy(o => o.AttendanceDate).ToList();
                    List<AttendanceMasterEntity> distinctEmployees = attendances.DistinctBy(e => e.EmployeeId).ToList();
                    foreach (AttendanceMasterEntity distinctEmployee in distinctEmployees)
                    {
                        AttendanceMasterCalculatedData calculatedData = new AttendanceMasterCalculatedData();
                        calculatedData.DepartmentId = distinctEmployee.DepartmentId;
                        calculatedData.EmployeeId = distinctEmployee.EmployeeId;
                        calculatedData.FromDate = ui.FromDate;
                        calculatedData.ToDate = ui.ToDate;
                        calculatedData.BasicPay = _applicationDbContext.Employees.Find(distinctEmployee.EmployeeId).Salary;
                        calculatedData.LateCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLate && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.EarlyOutCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsEarlyOut && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.LeaveCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLeave && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.AttendanceDays = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLeave == false && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        attendanceMasterCalculatedData.Add(calculatedData);
                    }
                    List<PayrollEntity> payrolls = CalculatePayroll(attendanceMasterCalculatedData);
                    _applicationDbContext.Payrolls.AddRange(payrolls);
                    _applicationDbContext.SaveChanges();
                    TempData["Info"] = "successfully save a record to the system";
                }
            }
            catch (Exception ex)
            {
                TempData["Info"] = "Error occur when  saving a record  to the system";
            }
            return RedirectToAction("list");
        }

        private List<PayrollEntity> CalculatePayroll(List<AttendanceMasterCalculatedData> attendanceMasterCalculatedData)
        {
            List<PayrollEntity> payrolls = new List<PayrollEntity>();
            decimal incomeTax = 2000, allowance = 30000, deduction = 10000;
            int workingDays = 30;
            foreach (AttendanceMasterCalculatedData calculatedData in attendanceMasterCalculatedData)
            {
                PayrollEntity payroll = new PayrollEntity();
                payroll.Id = Guid.NewGuid().ToString();
                payroll.CreatedAt = DateTime.Now;
                payroll.FromDate = calculatedData.FromDate;
                payroll.ToDate = calculatedData.ToDate;
                payroll.EmployeeId = calculatedData.EmployeeId;
                payroll.DepartmentId = calculatedData.DepartmentId;
                payroll.IncomeTax = incomeTax;
                decimal PayPerDay = (calculatedData.BasicPay / workingDays);//500000/30 >> 16,666.66666666667
                payroll.AttendanceDeduction = CalculateAttendanceDeductionByAttendancePolicy(calculatedData.EmployeeId, PayPerDay, calculatedData.LateCount, calculatedData.EarlyOutCount);
                payroll.GrossPay = ((PayPerDay) * calculatedData.AttendanceDays) + allowance - payroll.AttendanceDeduction - deduction;
                payroll.NetPay = payroll.GrossPay - payroll.IncomeTax;
                payroll.Allowance = allowance;
                payroll.Deduction = deduction;
                payroll.AttendanceDays = calculatedData.AttendanceDays;
                payrolls.Add(payroll);
            }
            return payrolls;
        }

        private decimal CalculateAttendanceDeductionByAttendancePolicy(string EmployeeId, decimal PayPerDay, int lateCount, int earlyOutCount)
        {
            decimal attendanceDeduction = 0;
            var attendancePolicy = (from ap in _applicationDbContext.AttendancePolicies
                                    join s in _applicationDbContext.Shifts
                                    on ap.Id equals s.AttendancePolicyId
                                    join sa in _applicationDbContext.ShiftAssigns
                                    on s.Id equals sa.ShiftId
                                    where sa.EmployeeId == EmployeeId
                                    select ap).FirstOrDefault();
            if (attendancePolicy != null)
            {
                if (lateCount > attendancePolicy.NumberOfLateTime || attendancePolicy?.NumberOfEarlyOutTime < earlyOutCount)
                {
                    attendanceDeduction = attendancePolicy?.DeductionInAmount ?? 0;
                }
                if (attendancePolicy?.DeductionInDay > 0)
                {
                    attendanceDeduction += (PayPerDay * attendancePolicy?.DeductionInDay) ?? 0;
                }
            }
            return attendanceDeduction;
        }
        /*
        public IActionResult PayrollDetailReport()
        {
            ViewBag.Employees = _mapper.Map<List<EmployeeEntity>, List<EmployeeViewModel>>(_applicationDbContext.Employees.ToList());
            ViewBag.Departments = _mapper.Map<List<DepartmentEntity>, List<DepartmentViewModel>>(_applicationDbContext.Departments.ToList());
            return View();
        }
        public IActionResult PayrollDetailReportByPayrollMonth(DateTime fromDate, DateTime toDate, string employeeId, string departmentId)
        {
            List<PayrollDetail> payrollDetails = (from p in _applicationDbContext.Payrolls
                                                  join e in _applicationDbContext.Employees
                                                  on p.EmployeeId equals e.Id
                                                  join d in _applicationDbContext.Departments
                                                  on e.DepartmentId equals d.Id
                                                  where (e.Id == employeeId && (p.FromDate >= fromDate && p.ToDate <= toDate) || (d.Id == departmentId && (p.FromDate >= fromDate && p.ToDate <= toDate)))
                                                  select new PayrollDetail
                                                  {
                                                      FromDate = p.FromDate.ToString("dd-MM-yyyy"),
                                                      ToDate = p.ToDate.ToString("dd-MM-yyyy"),
                                                      EmployeeInfo = e.Code + "/" + e.Name,
                                                      BasicSalary = e.BasicSalary,
                                                      DepartmentInfo = d.Code + "/" + d.Name,
                                                      GrossPay = p.GrossPay,
                                                      NetPay = p.NetPay,
                                                      AttendanceDays = p.AttendanceDays,
                                                      AttendanceDeduction = p.AttendanceDeduction,
                                                      Allowance = p.Allowance,
                                                      Deduction = p.Deduction,
                                                      IncomeTax = p.IncomeTax,
                                                  }).ToList();
            if (payrollDetails.Any())
            {
                string reportname = $"PayrollDetails_{Guid.NewGuid():N}.xlsx";
                var exportData = FilesIOHelper.ExporttoExcel<PayrollDetail>(payrollDetails, "PayrollDetailReport");
                return File(exportData, "application/vnd.openxmlformats-officedocument.spreedsheetml.sheet", reportname);
            }
            else
            {
                TempData["info"] = "There is no data when report to excel";
                return RedirectToAction("PayrollDetailReport");
            }
        }
        */
    }
}