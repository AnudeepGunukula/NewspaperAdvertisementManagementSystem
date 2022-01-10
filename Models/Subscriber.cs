using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Subscriber
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SubscriberId { get; set; }

        public string SubscriberDesc { get; set; }

        public string CompanyEmail { get; set; }

        public string Popularity { get; set; }

        public string OfficeAddress { get; set; }

        public string Price { get; set; }

    }
}