using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewspaperAdvertisementManagementSystem.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentId { get; set; }
        public int PaymentAmount { get; set; }
        public string PaymentType { get; set; }
        public string UpiId { get; set; }
        public Boolean PaymentStatus { get; set; }
        public DateTime PaymentTime { get; set; }


        public int AdvertisementId { get; set; }
        public virtual Advertisement Advertisement { get; set; }




    }
}