using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class JobDAO
    {
        public static List<Job> GetJobs()
        {
            using var context = new JobFinderContext();
            var jobs = context.Jobs
                .Include(j => j.Company)
                .Include(j => j.JobSkills)
                    .ThenInclude(jk => jk.Skill)
                .ToList();
            return jobs;
        }

        public static Job GetJobById(int id)
        {
            using var context = new JobFinderContext();
            var job = context.Jobs.FirstOrDefault(j => j.JobId == id);
            return job;
        }

        public static void AddJob(Job job)
        {
            using var context = new JobFinderContext();
            context.Jobs.Add(job);
            context.SaveChanges();
        }

        public static void UpdateJob(Job job)
        {
            using var context = new JobFinderContext();
            context.Entry<Job>(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteJob(Job job)
        {
            using var context = new JobFinderContext();
            job.Status = false;
            context.Entry<Job>(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            var jobApplications = context.JobApplications
                .Where(ja => ja.JobId == job.JobId)
                .ToList();

            foreach (var ja in jobApplications)
            {
                ja.Status = "Job Deleted";
                context.Entry<JobApplication>(ja).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
