using Microsoft.AspNetCore.Mvc;
using PracticalTwelve.Data.Interfaces;

namespace PracticalTwelve.Controllers
{
    public class TestOneController : Controller
    {
        readonly IEmployeeRepository _empolyeeRepository;

        public TestOneController(IEmployeeRepository empolyeeRepository)
        {
            _empolyeeRepository = empolyeeRepository;
        }

        public IActionResult Index()
        {
            throw new DivideByZeroException();
            return View();
        }
    }
}