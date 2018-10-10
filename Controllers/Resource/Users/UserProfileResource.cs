using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using SystemSupportingMSE.Core.Models;

namespace SystemSupportingMSE.Controllers.Resource.Users
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

        public KeyValuePairResource Gender { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public ICollection<KeyValuePairResource> Teams { get; set; }

        public ICollection<KeyValuePairResource> Roles { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? LastLogin { get; set; }

        public UserProfileResource()
        {
            Roles = new Collection<KeyValuePairResource>();
            Teams = new Collection<KeyValuePairResource>();
        }

    }
}