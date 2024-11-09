using BusinessObjects;
using DataAccessLayers.DTOs.Education;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEducationService
    {
        List<Education> GetEducationsByUserId(int userId);
        Education GetEducationById(int id);
        void AddEducation(Education education);
        void UpdateEducation(Education education);
        void DeleteEducation(Education education);
    }
}
