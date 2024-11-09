using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.DTOs.WorkExperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class WorkExperienceRepository : IWorkExperienceRepository
    {
        public void AddWorkExperience(WorkExperience workExperience)
        {
            WorkExperienceDAO.AddWorkExperience(workExperience);
        }

        public void DeleteWorkExperience(WorkExperience workExperience)
        {
            WorkExperienceDAO.DeleteWorkExperience(workExperience);
        }

        public List<WorkExperience> GetWorkExperiencesByUserId(int userId)
        {
            return WorkExperienceDAO.GetWorkExperiencesByUserId(userId);
        }

        public void UpdateWorkExperience(WorkExperience workExperience)
        {
            WorkExperienceDAO.UpdateWorkExperience(workExperience);
        }

    }
}
