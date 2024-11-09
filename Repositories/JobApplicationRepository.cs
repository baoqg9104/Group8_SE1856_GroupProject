using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        public void AddJobApplication(JobApplication jobApplication)
        {
            JobApplicationDAO.AddJobApplication(jobApplication);
        }

        public void DeleteJobApplication(JobApplication jobApplication)
        {
            JobApplicationDAO.DeleteJobApplication(jobApplication);
        }

        public JobApplication GetJobApplication(int id)
        {
            return JobApplicationDAO.GetJobApplication(id);
        }

        public List<JobApplication> GetJobApplications()
        {
            return JobApplicationDAO.GetJobApplications();
        }

        public void UpdateJobApplication(JobApplication jobApplication)
        {
            JobApplicationDAO.UpdateJobApplication(jobApplication);
        }
    }
}
