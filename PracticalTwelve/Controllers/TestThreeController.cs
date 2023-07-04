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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> GetEmployeeCountsByDesignation()
        {
            IEnumerable<CountOfEmployeeByDesginationId> data = await _testThreeRepository.GetEmployeeCountsByDesignationAsync();
            return PartialView("_EmployeeCountByDesignation", data);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetEmployeeDesignationDetails()
        {
            IEnumerable<EmployeeWithDesignation> data = await _testThreeRepository.GetEmployeeDesignationDetailsAsync();
            return PartialView("_EmployeeWithDesignation", data);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetEmployeeDesignationDetailsUsingView()
        {
            IEnumerable<EmployeeDetails> data = await _testThreeRepository.GetEmployeeDesignationDetailsUsingViewAsync();
            return PartialView("_EmployeeDetails", data);
        }

        [HttpGet]
        public PartialViewResult InsertDesignation()
        {
            return PartialView("_CreateDesignation");
        }

        [HttpPost]
        public async Task<JsonResult> InsertDesignation(string designation)
        {
            int count = await _testThreeRepository.InsertDesignationAsync(designation);
            return Json(new { Result = "OK", Count = count });
        }

        [HttpGet]
        public PartialViewResult InsertEmployeeInfo()
        {
            return PartialView("_CreateEmployeeInfo");
        }

        [HttpPost]
        public async Task<JsonResult> InsertEmployeeInfo(EmployeeInfo employeeInfo)
        {
            int count = await _testThreeRepository.InsertEmployeeInfoAsync(employeeInfo);
            return Json(new { Result = "OK", Count = count });
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignationThatHaveMoreThanOneEmployee()
        {
            List<string> data = await _testThreeRepository.GetDesignationThatHaveMoreThanOneEmployeeAsync();
            return PartialView("_DesignationTable", data);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetEmployeeDetailsUsingSP()
        {
            IEnumerable<EmployeeDetails> data = await _testThreeRepository.GetEmployeeDetailsUsingSPAsync();
            return PartialView("_EmployeeDetails", data);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetEmployeeDetailsByDesignationIdUsingSP()
        {
            IEnumerable<EmployeeInfo> data = await _testThreeRepository.GetEmployeeDetailsByDesignationIdUsingSPAsync();
            return PartialView("_EmployeeDetails", data);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeHavingMaxSalary()
        {
            EmployeeInfo data = await _testThreeRepository.GetEmployeeHavingMaxSalaryAsync();
            return PartialView("_EmployeeInfoTable", data);
        }

        
    }
}
