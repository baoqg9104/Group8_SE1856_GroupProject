using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ISkillRepository
    {
        List<Skill> GetSkills();
        void AddSkill(Skill skill);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
    }
}
