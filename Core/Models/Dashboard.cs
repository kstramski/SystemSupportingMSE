using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SystemSupportingMSE.Core.Models
{
    public class Dashboard
    {
        public ICollection<KeyValuePairChart> Genders;
        public ICollection<KeyValuePairChart> Emails;
        public ICollection<KeyValuePairChart> UsersAge;
        public ICollection<KeyValuePairChart> UsersRegistered;

        public Dashboard()
        {
            this.Genders = new Collection<KeyValuePairChart>();
            this.Emails = new Collection<KeyValuePairChart>();
            this.UsersAge = new Collection<KeyValuePairChart>();
            this.UsersRegistered = new Collection<KeyValuePairChart>();
        }
    }
}