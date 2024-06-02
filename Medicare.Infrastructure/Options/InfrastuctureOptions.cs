using System.ComponentModel.DataAnnotations;

namespace Medicare.Infrastructure.Options
{
    public class InfrastuctureOptions
    {
        [Required]
        public string Database { get; set; }
        public string Redis{ get; set; }
        public string CacheName { get; set; }

    }
}
