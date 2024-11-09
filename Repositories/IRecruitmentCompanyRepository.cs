using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRecruitmentCompanyRepository
    {
        List<RecruitmentCompany> GetRecruitmentCompanies();
        RecruitmentCompany GetRecruitmentCompany(int id);
        void AddRecruitmentCompany(RecruitmentCompany recruitmentCompany);
        void UpdateRecruitmentCompany(RecruitmentCompany recruitmentCompany);
        void DeleteRecruitmentCompany(RecruitmentCompany recruitmentCompany);

    }
}
