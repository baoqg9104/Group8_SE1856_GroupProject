using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone {  get; set; }
        public int? Gender { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<Education> Educations {  get; set; }
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; }
        public virtual ICollection<MemberSkill> MemberSkills { get; set; }
    }
}
