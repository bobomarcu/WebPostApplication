using System.ComponentModel.DataAnnotations;

namespace PostApplication.Models
{
    public class Package
    {
        public int ID { get; set; }
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Sender")]
        public User Sender { get; set; }

        [Display(Name = "Receiver")]
        public User Receiver { get; set; }

        [Display(Name = "Courier")]
        public Courier AssignedCourier { get; set; }
    }
}
