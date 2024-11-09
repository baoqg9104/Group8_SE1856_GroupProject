using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobSkillService : IJobSkillService
    {
        private readonly IJobSkillRepository jobSkillRepository;

        public JobSkillService()
        {
            jobSkillRepository = new JobSkillRepository();
        }

        public void AddJobSkill(JobSkill jobSkill)
        {
            jobSkillRepository.AddJobSkill(jobSkill);
        }

        public List<JobSkill> GetJobSkills()
        {
            return jobSkillRepository.GetJobSkills();   
        }

        public void RemoveJobSkill(JobSkill jobSkill)
        {
            jobSkillRepository.RemoveJobSkill(jobSkill);
        }
    }
}
