using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }
        public int CompanyId { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public double Salary { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public bool Status { get; set; }

        [ForeignKey("CompanyId")]
        public virtual RecruitmentCompany Company { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<JobSkill> JobSkills { get; set; }

    }
}
