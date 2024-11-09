using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class RecruitmentCompany
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyType { get; set; }
        public string? Industry { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Job> Jobs {  get; set; }
    }
}
