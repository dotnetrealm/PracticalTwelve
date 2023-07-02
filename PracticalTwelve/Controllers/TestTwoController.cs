using Microsoft.AspNetCore.Mvc;
using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Domain.Entities;

namespace PracticalTwelve.Controllers
{
    public class TestTwoController : Controller
    {
        private readonly ITestTwoRepository _testTwoRepository;
        public TestTwoController(ITestTwoRepository testTwoRepository)
        {
            _testTwoRepository = testTwoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Return all employees view
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> LoadUsers()
        {
            IEnumerable<EmployeeSalary> employees = await _testTwoRepository.GetAllEmployeeAsync();
            return PartialView("_EmployeeSalaryTable", employees);
        }

        /// <summary>
        /// Insert multiple records
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<JsonResult> InsertMultipleRecord()
        {
            List<EmployeeSalary> empList = new() {
                new EmployeeSalary() { FirstName="Bhavin", LastName="Kareliya" , DOB=Convert.ToDateTime("09-02-2002").Date, Address="Rajkot",  MobileNumber="1231231231", Salary=100000},
                new EmployeeSalary() { FirstName="Vipul", LastName="Kumar" , DOB=Convert.ToDateTime("07-07-1999").Date, Address="Kutch",  MobileNumber="1231231231", Salary=80000},
                new EmployeeSalary() { FirstName="Jil", LastName="Patel" , DOB=Convert.ToDateTime("12-04-2001").Date, Address="Anand",  MobileNumber="1231231231", Salary=50000},
                new EmployeeSalary() { FirstName="Abhi", LastName="Dasadiya" , DOB=Convert.ToDateTime("05-02-1980").Date, Address="Morbi",  MobileNumber="1231231231", Salary=25000},
                new EmployeeSalary() { FirstName="Jay", LastName="Gohel" , DOB=Convert.ToDateTime("06-07-2002").Date, Address="Rajkot",  MobileNumber="1231231231", Salary=30000},
            };
            int count = await _testTwoRepository.InsertMultipleRecord(empList);
            return Json(new { Result = "OK", Count = count });
        }

        /// <summary>
        /// Return total salary
        /// </summary>
        /// <returns>total salary</returns>
        [HttpGet]
        public async Task<JsonResult> GetTotalEmployeesSalary()
        {
            decimal total = await _testTwoRepository.GetTotalSalaryAsync();
            return Json(new { Result = "OK", TotalSalary = total });
        }

        /// <summary>
        /// Return Employee salary table
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetEmployeesOlderThanGivenDate()
        {
            DateTime date = new DateTime(2000, 01, 01);
            IEnumerable<EmployeeSalary> employeeSalaries = await _testTwoRepository.GetEmployeesOlderThanGivenDate(date);
            return PartialView("_EmployeeSalaryTable", employeeSalaries);
        }

        /// <summary>
        /// Return employee salary table whose middle name is null
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetNullMiddleNameEmployees()
        {
            IEnumerable<EmployeeSalary> employeeSalaries = await _testTwoRepository.GetNullMiddleNameEmployees();
            return PartialView("_EmployeeSalaryTable", employeeSalaries);
        }
    }
}
