using Microsoft.AspNetCore.Mvc;
using PracticalTwelve.Data.Interfaces;
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
    }
}
