using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }

    }
}