using System.ComponentModel.DataAnnotations;

namespace SystemSupportingMSE.Core.Models
{
    public class KeyValuePairChart
    {
        [Required]
        public string Name { get; set; }
        public int Items { get; set; }
    }
}