using BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class RecruitmentCompanyDAO
    {
        public static List<RecruitmentCompany> GetRecruitmentCompanies()
        {
            var list = new List<RecruitmentCompany>();
            try
            {
                using var context = new JobFinderContext();
                list = context.RecruitmentCompanies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static RecruitmentCompany GetRecruitmentCompany(int id)
        {
            using var context = new JobFinderContext();
            return context.RecruitmentCompanies.FirstOrDefault(rc => rc.CompanyId == id);
        }

        public static void AddRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            using var context = new JobFinderContext();
            context.RecruitmentCompanies.Add(recruitmentCompany);
            context.SaveChanges();
        }

        public static void UpdateRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            using var context = new JobFinderContext();

            var industry = recruitmentCompany.Industry.Trim();
            var country = recruitmentCompany.Country;
            var desc = recruitmentCompany.Description.Trim();
            var name = recruitmentCompany.CompanyName.Trim();
            var type = recruitmentCompany.CompanyType.Trim();

            if (string.IsNullOrEmpty(industry))
            {
                throw new Exception("Industry is required");
            }

            if (string.IsNullOrEmpty(country))
            {
                throw new Exception("Country is required");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name is required");
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new Exception("Type is required");
            }

            context.Entry<RecruitmentCompany>(recruitmentCompany).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            using var context = new JobFinderContext();
            context.RecruitmentCompanies.Remove(recruitmentCompany);
            context.SaveChanges();
        }
    }
}
