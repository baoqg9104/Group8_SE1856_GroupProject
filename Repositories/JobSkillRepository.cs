using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class JobSkillRepository : IJobSkillRepository
    {
        public void AddJobSkill(JobSkill jobSkill)
        {
            JobSkillDAO.AddJobSkill(jobSkill);
        }

        public List<JobSkill> GetJobSkills()
        {
            return JobSkillDAO.GetJobSkills();
        }

        public void RemoveJobSkill(JobSkill jobSkill)
        {
            JobSkillDAO.RemoveJobSkill(jobSkill);
        }
    }
}
