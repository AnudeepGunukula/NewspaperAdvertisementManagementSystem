using System.ComponentModel.DataAnnotations;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Subscriber
    {

        [Key]
        public int SubscriberId { get; set; }

        public string SubscriberDesc { get; set; }

        public string CompanyEmail { get; set; }

        public string Popularity { get; set; }

        public string OfficeAddress { get; set; }

        public string Price { get; set; }

    }
}