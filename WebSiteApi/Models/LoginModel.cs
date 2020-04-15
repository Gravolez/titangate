using System;
using System.ComponentModel.DataAnnotations;

namespace TitanGate.WebSiteStore.Api.Models
{
    [Serializable]
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
