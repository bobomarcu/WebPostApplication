using System.ComponentModel.DataAnnotations;

namespace PostApplication.Models
{
    public class Package
    {
        public int ID { get; set; }

        [Display(Name = "Weight (in KG)")]
        public double Weight { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Sender")]
        public int SenderId { get; set; }
        public User Sender { get; set; }

        [Display(Name = "Receiver")]
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        [Display(Name = "Courier")]
        public int AssignedCourierId { get; set; }
        public Courier AssignedCourier { get; set; }
    }
}
