using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SkillRepository : ISkillRepository
    {
        public void AddSkill(Skill skill)
        {
            SkillDAO.AddSkill(skill);
        }

        public void DeleteSkill(Skill skill)
        {
            SkillDAO.DeleteSkill(skill);
        }

        public List<Skill> GetSkills()
        {
            return SkillDAO.GetSkills();
        }

        public void UpdateSkill(Skill skill)
        {
            SkillDAO.UpdateSkill(skill);
        }
    }
}
