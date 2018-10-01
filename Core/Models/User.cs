using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public byte GenderId { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? LastLogin { get; set; }

        public Gender Gender { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<UserTeam> Teams { get; set; }

        public ICollection<UserEvent> Events { get; set; }

        public User()
        {
            this.Roles = new Collection<UserRole>();
            this.Teams = new Collection<UserTeam>();
            this.Events = new Collection<UserEvent>();
        }
    }
}