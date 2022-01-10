using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Advertisement
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AdvertisementId { get; set; }

        public string AdvertisementType { get; set; }
        public string AdvertisementTitle { get; set; }

        public string AdvertisementDesc { get; set; }

        public string AdvertisementImageName { get; set; }


        [NotMapped]
        public IFormFile AdvertisementImageFile { get; set; }
        public string SubscriptionPlan { get; set; }
        public string AdvertisementSize { get; set; }
        public string Subscriber { get; set; }

        public DateTime AdvRegisteredDate { get; set; }

        public DateTime AdvPublishedDate { get; set; }

        public Boolean Agree { get; set; }

        public Boolean AdminApproved { get; set; }

        public string ClientId { get; set; }

        public virtual Client Client { get; set; }


    }
}