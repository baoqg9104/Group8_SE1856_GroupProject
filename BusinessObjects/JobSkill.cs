using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class JobSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobSkillId { get; set; }
        public int SkillId { get; set; }
        public int JobId { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Job Job { get; set; }
    }
}
