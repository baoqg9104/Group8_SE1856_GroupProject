using BusinessObjects;
using DataAccessLayers.DTOs.Education;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository educationRepository;
        public EducationService()
        {
            educationRepository = new EducationRepository();
        }

        public void AddEducation(Education education)
        {
            educationRepository.AddEducation(education);
        }

        public void DeleteEducation(Education education)
        {
            educationRepository.DeleteEducation(education);
        }

        public Education GetEducationById(int id)
        {
            return educationRepository.GetEducationById(id);
        }

        public List<Education> GetEducationsByUserId(int userId)
        {
            return educationRepository.GetEducationsByUserId(userId);
        }

        public void UpdateEducation(Education education)
        {
            educationRepository.UpdateEducation(education);
        }
    }
}
