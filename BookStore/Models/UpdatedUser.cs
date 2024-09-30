using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Models
{
    public class UpdatedUser : IdentityUser
    {
        public string? Role { get; set; }
    }
}