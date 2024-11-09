using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository jobApplicationRepository;

        public JobApplicationService()
        {
            jobApplicationRepository = new JobApplicationRepository();
        }

        public void AddJobApplication(JobApplication jobApplication)
        {
            jobApplicationRepository.AddJobApplication(jobApplication);
        }

        public void DeleteJobApplication(JobApplication jobApplication)
        {
            jobApplicationRepository.DeleteJobApplication(jobApplication);
        }

        public JobApplication GetJobApplication(int id)
        {
            return jobApplicationRepository.GetJobApplication(id);
        }

        public List<JobApplication> GetJobApplications()
        {
            return jobApplicationRepository.GetJobApplications();
        }

        public void UpdateJobApplication(JobApplication jobApplication)
        {
            jobApplicationRepository.UpdateJobApplication(jobApplication);
        }
    }
}
