using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(30)]
        public string? FullName { get; set; }
    }
}
