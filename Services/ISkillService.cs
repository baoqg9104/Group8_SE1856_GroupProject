using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISkillService
    {
        List<Skill> GetSkills();
        void AddSkill(Skill skill);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
    }
}
