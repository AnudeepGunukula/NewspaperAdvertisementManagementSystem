using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Advertisement
    {

        [Key]
        public int AdvertisementId { get; set; }

        public string AdvertisementType { get; set; }

        public string AdvertisementTitle { get; set; }

        public string AdvertisementDesc { get; set; }


        [NotMapped]
        public IFormFile AdvertisementImage { get; set; }

        public string SubscriptionPlan { get; set; }

        public string AdvertisementSize { get; set; }

        public string Subscriber { get; set; }

        public DateTime AdvRegisteredDate { get; set; }

        public DateTime AdvPublishedDate { get; set; }

        public Boolean Agree { get; set; }

        public Boolean AdminApproved { get; set; }

        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }


    }
}