using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class JobRepository : IJobRepository
    {
        public void AddJob(Job job)
        {
            JobDAO.AddJob(job);
        }

        public void DeleteJob(Job job)
        {
            JobDAO.DeleteJob(job);
        }

        public Job GetJobById(int id)
        {
            return JobDAO.GetJobById(id);
        }

        public List<Job> GetJobs()
        {
            return JobDAO.GetJobs();
        }

        public void UpdateJob(Job job)
        {
            JobDAO.UpdateJob(job);
        }
    }
}
