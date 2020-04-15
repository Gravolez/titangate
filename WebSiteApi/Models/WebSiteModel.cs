using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class WebSiteModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Url]
        [Required]
        public string Url { get; set; }

        [Required]
        public CategoryModel Category { get; set; }

        [Required]
        public LoginModel Login { get; set; }
        public string ScreenshotUrl { get; set; }
    }
}
