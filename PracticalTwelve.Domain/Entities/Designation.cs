using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticalTwelve.Domain.Entities
{
    public class DesignationModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Designation { get; set; } = null!;
    }
}
