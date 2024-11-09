using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RecruitmentCompanyRepository : IRecruitmentCompanyRepository
    {
        public void AddRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            RecruitmentCompanyDAO.AddRecruitmentCompany(recruitmentCompany);
        }

        public void DeleteRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            RecruitmentCompanyDAO.DeleteRecruitmentCompany(recruitmentCompany);
        }

        public List<RecruitmentCompany> GetRecruitmentCompanies()
        {
            return RecruitmentCompanyDAO.GetRecruitmentCompanies();
        }

        public RecruitmentCompany GetRecruitmentCompany(int id)
        {
            return RecruitmentCompanyDAO.GetRecruitmentCompany(id);
        }

        public void UpdateRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            RecruitmentCompanyDAO.UpdateRecruitmentCompany(recruitmentCompany);
        }
    }
}
