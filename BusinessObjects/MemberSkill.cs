using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class MemberSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberSkillId { get; set; }
        public int MemberId { get; set; }
        public int SkillId { get; set; }
        public int Level {  get; set; }

        public virtual Member Member { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
