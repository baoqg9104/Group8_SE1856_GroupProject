using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository jobRepository;

        public JobService()
        {
            jobRepository = new JobRepository();
        }

        public void AddJob(Job job)
        {
            jobRepository.AddJob(job);
        }

        public void DeleteJob(Job job)
        {
            jobRepository.DeleteJob(job);
        }

        public Job GetJobById(int id)
        {
            return jobRepository.GetJobById(id);
        }

        public List<Job> GetJobs()
        {
            return jobRepository.GetJobs();
        }

        public void UpdateJob(Job job)
        {
            jobRepository.UpdateJob(job);
        }
    }
}
