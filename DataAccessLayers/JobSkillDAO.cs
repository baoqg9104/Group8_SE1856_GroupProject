using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class JobSkillDAO
    {
        public static List<JobSkill> GetJobSkills()
        {
            using var context = new JobFinderContext();
            return context.JobSkills.ToList();
        }

        public static void RemoveJobSkill(JobSkill jobSkill)
        {
            using var context = new JobFinderContext();
            context.JobSkills.Remove(jobSkill);
            context.SaveChanges();
        }

        public static void AddJobSkill(JobSkill jobSkill)
        {
            using var context = new JobFinderContext();
            context.JobSkills.Add(jobSkill);
            context.SaveChanges();
        }
    }
}
