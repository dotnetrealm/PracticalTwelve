using Microsoft.AspNetCore.Mvc;
using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Domain.Entities;

namespace PracticalTwelve.Controllers
{
    public class TestOneController : Controller
    {
        readonly ITestOneRepository _testOneRepository;

        public TestOneController(ITestOneRepository testOneRepository)
        {
            _testOneRepository = testOneRepository;
        }

        /// <summary>
        /// return Index view
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// Return all employees view
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> LoadUsers()
        {
            IEnumerable<Employee> employees = await _testOneRepository.GetAllEmployeeAsync();
            return PartialView("_EmployeesTable", employees);
        }

        /// <summary>
        /// Return partial view for add new designation
        /// </summary>
        /// <returns>Partial View</returns>
        public async Task<IActionResult> CreateDesignation()
        {
            IEnumerable<Employee> employees = await _testOneRepository.GetAllEmployeeAsync();
            return PartialView("_CreateDesignation");
        }

        /// <summary>
        /// Insert single employee to DB
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<JsonResult> InsertSingleRecord()
        {
            int count = await _testOneRepository.InsertSingleRecordAsync();
            return Json(new { Result = "OK", Count = count });
        }

        /// <summary>
        /// Insert multiple records
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<JsonResult> InsertMultipleRecord()
        {
            List<Employee> empList = new() {
                new Employee() { FirstName="Vipul", LastName="Kumar" , DOB=Convert.ToDateTime("07-07-1999").Date, Address="Kutch",  MobileNumber="1231231231"},
                new Employee() { FirstName="Jil", LastName="Patel" , DOB=Convert.ToDateTime("12-04-2001").Date, Address="Anand",  MobileNumber="1231231231"},
                new Employee() { FirstName="Vipul", LastName="Kumar" , DOB=Convert.ToDateTime("05-02-2001").Date, Address="Morbi",  MobileNumber="1231231231"},
                new Employee() { FirstName="Jay", LastName="Gohel" , DOB=Convert.ToDateTime("06-07-2002").Date, Address="Rajkot",  MobileNumber="1231231231"},
            };
            int count = await _testOneRepository.InsertMultipleRecord(empList);
            return Json(new { Result = "OK", Count = count });
        }

        /// <summary>
        /// Update FirstName of first record in DB
        /// </summary>
        /// <returns>json with numbers of affected row</returns>
        [HttpGet]
        public async Task<IActionResult> UpdateFirstNameOfFirstRecord()
        {
            int count = await _testOneRepository.UpdateFirstNameOfFirstRecordAsync("Vipulkumar");
            return Json(new { Result = "OK", Count = count });
        }

        /// <summary>
        /// Update MiddleName of all records in DB
        /// </summary>
        /// <param></param>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<IActionResult> UpdateMiddleNameOfAllRecordsAsync()
        {
            int count = await _testOneRepository.UpdateMiddleNameOfAllRecordsAsync();
            return Json(new { Result = "OK", Count = count });
        }

        /// <summary>
        /// Delete employees whose id is less than given id
        /// </summary>
        /// <param></param>
        /// <returns>View</returns>
        [HttpGet]
        public async Task<IActionResult> DeleteHavingLessValueThanId()
        {
            int count = await _testOneRepository.DeleteHavingLessValueThanId(2);
            return Json(new { Result = "OK", Count = count });
        }

        /// <summary>
        /// Delete all employee data from DB
        /// </summary>
        /// <returns>View</returns>
        [HttpGet]
        public async Task DeleteAllData()
        {
            await _testOneRepository.DeleteAllData();
        }
    }
}