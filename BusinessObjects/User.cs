using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int? CompanyId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public UserStatus Status { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("CompanyId")]
        public virtual RecruitmentCompany RecruitmentCompany { get; set; }

    }

    public enum UserStatus
    {
        Active = 1,
        Banned = 2,
        Deleted = 3
    }
}
