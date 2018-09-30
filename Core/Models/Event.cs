using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemSupportingMSE.Core.Models
{
    [Table("Events")]
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime RegistrationDateStart { get; set; }

        public DateTime RegistrationDateEnd { get; set; }

        public DateTime EventDateStart { get; set; }

        public DateTime EventDateEnd { get; set; }

        public ICollection<UserEvent> Users { get; set; }

        public Event()
        {
            this.Users = new Collection<UserEvent>();
        }
    }
}