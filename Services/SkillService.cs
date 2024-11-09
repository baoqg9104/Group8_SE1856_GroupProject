using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository skillRepository;

        public SkillService()
        {
            skillRepository = new SkillRepository();
        }

        public void AddSkill(Skill skill)
        {
            skillRepository.AddSkill(skill);
        }

        public void DeleteSkill(Skill skill)
        {
            skillRepository.DeleteSkill(skill);
        }

        public List<Skill> GetSkills()
        {
            return skillRepository.GetSkills();
        }

        public void UpdateSkill(Skill skill)
        {
            skillRepository.UpdateSkill(skill);
        }
    }
}
