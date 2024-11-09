using BusinessObjects;
using DataAccessLayers.DTOs.WorkExperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IWorkExperienceRepository
    {
        List<WorkExperience> GetWorkExperiencesByUserId(int userId);
        void AddWorkExperience(WorkExperience workExperience);
        void UpdateWorkExperience(WorkExperience workExperience);
        void DeleteWorkExperience(WorkExperience workExperience);
    }
}
