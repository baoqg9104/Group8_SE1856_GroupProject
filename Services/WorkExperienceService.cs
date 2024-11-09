using BusinessObjects;
using DataAccessLayers.DTOs.WorkExperience;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IWorkExperienceRepository workExperienceRepository; 

        public WorkExperienceService()
        {
            workExperienceRepository = new WorkExperienceRepository();
        }

        public void AddWorkExperience(WorkExperience workExperience)
        {
            workExperienceRepository.AddWorkExperience(workExperience);
        }

        public void DeleteWorkExperience(WorkExperience workExperience)
        {
            workExperienceRepository.DeleteWorkExperience(workExperience);
        }

        public List<WorkExperience> GetWorkExperiencesByUserId(int userId)
        {
            return workExperienceRepository.GetWorkExperiencesByUserId(userId);
        }

        public void UpdateWorkExperience(WorkExperience workExperience)
        {
            workExperienceRepository.UpdateWorkExperience(workExperience);
        }
    }
}
