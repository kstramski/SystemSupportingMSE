using System;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource
{
    public class UserSaveProfileResource
    {
         public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(50)]
        public string City { get; set; }
    }
}