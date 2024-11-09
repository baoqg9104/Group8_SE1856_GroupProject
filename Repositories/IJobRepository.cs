using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IJobRepository
    {
        List<Job> GetJobs();
        Job GetJobById(int id);
        void AddJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(Job job);
    }
}
