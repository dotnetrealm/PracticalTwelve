using Microsoft.AspNetCore.Mvc;
using PracticalTwelve.Data.Interfaces;
using PracticalTwelve.Data.Repositories;
using PracticalTwelve.Domain.Entities;
using PracticalTwelve.Domain.ViewModels;

namespace PracticalTwelve.Controllers
{
    public class TestThreeController : Controller
    {
        readonly ITestThreeRepository _testThreeRepository;

        public TestThreeController(ITestThreeRepository testThreeRepository)
        {
            _testThreeRepository = testThreeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetEmployeeCountsByDesignation()
        {
            IEnumerable<CountOfEmployeeByDesginationId> data = await _testThreeRepository.GetEmployeeCountsByDesignationAsync();
            return PartialView("_EmployeeCountByDesignation", data);
        }

        public async Task<IActionResult> GetEmployeeDesignationDetails()
        {
            IEnumerable<EmployeeDesignationDetails> data = await _testThreeRepository.GetEmployeeDesignationDetailsAsync();
            return PartialView("_EmployeeDesignationDetails", data);
        }

        public async Task<IActionResult> GetDesignationThatHaveMoreThanOneEmployee()
        {
            List<string> data = await _testThreeRepository.GetDesignationThatHaveMoreThanOneEmployeeAsync();
            return PartialView("_DesignationTable", data);
        }
        public async Task<IActionResult> GetEmployeeHavingMaxSalary()
        {
            EmployeeInfo data = await _testThreeRepository.GetEmployeeHavingMaxSalaryAsync();
            return PartialView("_EmployeeInfoTable", data);
        }

        [HttpGet]
        public async Task<JsonResult> InsertDesignation()
        {
            string designation = "NewDesignation_" + new Random().Next(1000, 9999);
            int count = await _testThreeRepository.InsertDesignationAsync(designation);
            return Json(new { Result = "OK", Count = count });
        }

        [HttpGet]
        public async Task<JsonResult> InsertEmployeeInfo()
        {
            EmployeeInfo emp = new()
            {
                FirstName = "FN_Employee_" + new Random().Next(1000, 9999),
                MiddleName = "MN_Employee_" + new Random().Next(1000, 9999),
                LastName = "LN_Employee_" + new Random().Next(1000, 9999),
                DOB = new DateTime(2000, 12, 01),
                Address = "Rajkot",
                MobileNumber = "1231231231",
                Salary = 15000,
                DesignationId = 1
            };
            int count = await _testThreeRepository.InsertEmployeeInfoAsync(emp);
            return Json(new { Result = "OK", Count = count });
        }
    }
}
