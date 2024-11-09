using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.DTOs.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EducationRepository : IEducationRepository
    {
        public void AddEducation(Education education)
        {
            EducationDAO.AddEducation(education);
        }

        public void DeleteEducation(Education education)
        {
            EducationDAO.DeleteEducation(education);
        }

        public Education GetEducationById(int id)
        {
            return EducationDAO.GetEducationById(id);
        }

        public List<Education> GetEducationsByUserId(int userId)
        {
            return EducationDAO.GetEducationsByUserId(userId);
        }

        public void UpdateEducation(Education education)
        {
            EducationDAO.UpdateEducation(education);
        }
    }
}
