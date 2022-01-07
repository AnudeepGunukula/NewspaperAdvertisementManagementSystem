using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentType { get; set; }
        public string UpiId { get; set; }
        public Boolean PaymentStatus { get; set; }
        public DateTime PaymentTime { get; set; }

        public int AdvertisementId { get; set; }

        [ForeignKey("AdvertisementId")]
        public Advertisement Advertisement { get; set; }


    }
}