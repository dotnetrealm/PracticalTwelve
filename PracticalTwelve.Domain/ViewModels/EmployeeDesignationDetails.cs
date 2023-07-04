using PracticalTwelve.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PracticalTwelve.Domain.ViewModels
{
    public class EmployeeWithDesignation
    {
        public string FirstName { get; set; } = null!;

        [DisplayFormat(NullDisplayText = "-")]
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string Designation { get; set; } = null!;
    }

    
}
