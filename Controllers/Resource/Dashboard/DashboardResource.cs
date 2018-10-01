using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SystemSupportingMSE.Controllers.Resource.Dashboard
{
    public class DashboardResource
    {
        public ICollection<KeyValuePairChartResource> Genders;
        public ICollection<KeyValuePairChartResource> Emails;
        public ICollection<KeyValuePairChartResource> UsersAge;
        public ICollection<KeyValuePairChartResource> UsersRegistered;

        public DashboardResource()
        {
            this.Genders = new Collection<KeyValuePairChartResource>();
            this.Emails = new Collection<KeyValuePairChartResource>();
            this.UsersAge = new Collection<KeyValuePairChartResource>();
            this.UsersRegistered = new Collection<KeyValuePairChartResource>();
        }
    }
}