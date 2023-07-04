using PracticalTwelve.Domain.Entities;

namespace PracticalTwelve.Domain.ViewModels
{
    public class EmployeeDetails
    {
        public EmployeeInfo EmployeeInfo { get; set; } = null!;
        public string Designation { get; set; } = null!;
    }
}
