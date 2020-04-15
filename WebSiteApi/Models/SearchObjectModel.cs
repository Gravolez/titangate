using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class SearchObjectModel
    {
        [Required]
        [Range(5, 100)]
        public int PageSize { get; set; }

        [Required]
        public int PageNumber { get; set; }

        public IEnumerable<SortField> SortFields { get; set; }
    }
}
