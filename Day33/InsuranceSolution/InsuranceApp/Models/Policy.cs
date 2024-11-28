using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceApp.Models
{
    public class Policy
    {
        public int PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyType { get; set; }

        [ForeignKey("Username")]
        public string UserName { get; set; }
        public User User { get; set; }
    }
}
