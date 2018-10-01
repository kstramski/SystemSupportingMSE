using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Controllers.Resource.Dashboard
{
    public class KeyValuePairChartResource
    {
        [Required]
        public string Name { get; set; }
        public int Items { get; set; }
    }
}