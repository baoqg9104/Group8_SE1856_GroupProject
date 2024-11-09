using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class JobApplicationDAO
    {
        public static List<JobApplication> GetJobApplications()
        {
            var list = new List<JobApplication>();
            try
            {
                using var context = new JobFinderContext();
                list = context.JobApplications
                    .Include(ja => ja.Job)
                        .ThenInclude(j => j.Company)
                    .Include(ja => ja.Member)
                        .ThenInclude(m => m.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static JobApplication GetJobApplication(int id)
        {
            using var context = new JobFinderContext();
            return context.JobApplications.FirstOrDefault(ja => ja.ApplicationId == id);
        }

        public static void AddJobApplication(JobApplication jobApplication)
        {
            using var context = new JobFinderContext();

            var existJobApplication = context.JobApplications
                .Any(ja => ja.JobId == jobApplication.JobId
                && ja.MemberId == jobApplication.MemberId
                && (ja.Status != "Withdrawn" && ja.Status != "Rejected")
                );

            if (existJobApplication)
            {
                throw new Exception("You have applied for this job before.");
            }

            context.JobApplications.Add(jobApplication);
            context.SaveChanges();
        }

        public static void UpdateJobApplication(JobApplication jobApplication)
        {
            using var context = new JobFinderContext();
            context.Entry<JobApplication>(jobApplication).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteJobApplication(JobApplication jobApplication)
        {
            using var context = new JobFinderContext();
            context.JobApplications.Remove(jobApplication);
            context.SaveChanges();
        }
    }
}
