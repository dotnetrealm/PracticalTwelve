using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PracticalTwelve.Domain.Entities
{
    public class EmployeeInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [MaxLength(50)]
        [DisplayName("Middle Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Birth Date")]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(maximumLength: 10, MinimumLength = 10)]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public string MobileNumber { get; set; } = null!;

        [MaxLength(100)]
        [DisplayFormat(NullDisplayText = "-")]
        public string? Address { get; set; }

        [MaxLength(100)]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DisplayFormat(NullDisplayText = "0")]
        public int? DesignationId { get; set; }
    }
}
