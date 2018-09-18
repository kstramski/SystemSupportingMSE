using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource
{
    public class UserProfileResource
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

        public KeyValuePairResource Team { get; set; }

        public ICollection<KeyValuePairResource> Roles { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime LastLogin { get; set; }

    }
}