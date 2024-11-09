using BusinessObjects;
using DataAccessLayers.DTOs.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEducationRepository
    {
        List<Education> GetEducationsByUserId(int userId);
        Education GetEducationById(int id);
        void AddEducation(Education addEducation);
        void UpdateEducation(Education education);
        void DeleteEducation(Education education);
    }
}
