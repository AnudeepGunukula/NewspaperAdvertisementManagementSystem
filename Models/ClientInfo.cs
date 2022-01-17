using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class ClientInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        // public string Email { get; set; }

        public string Address { get; set; }

        public string MobileNumber { get; set; }
        public string ProfileImageName { get; set; }

    }
}