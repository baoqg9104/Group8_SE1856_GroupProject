using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobApplicationService
    {
        List<JobApplication> GetJobApplications();
        JobApplication GetJobApplication(int id);
        void AddJobApplication(JobApplication jobApplication);
        void UpdateJobApplication(JobApplication jobApplication);
        void DeleteJobApplication(JobApplication jobApplication);
    }
}
