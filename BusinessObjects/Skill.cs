using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public virtual ICollection<JobSkill> JobSkills { get; set; }
        public virtual ICollection<MemberSkill> MemberSkills { get; set; }
    }
}
