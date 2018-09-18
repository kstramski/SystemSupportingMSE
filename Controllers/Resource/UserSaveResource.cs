using System;
using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource
{
    public class UserSaveResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }
    }
}