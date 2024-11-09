using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RecruitmentCompanyService : IRecruitmentCompanyService
    {
        private readonly IRecruitmentCompanyRepository recruitmentCompanyRepository;

        public RecruitmentCompanyService()
        {
            recruitmentCompanyRepository = new RecruitmentCompanyRepository();
        }

        public void AddRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            recruitmentCompanyRepository.AddRecruitmentCompany(recruitmentCompany);
        }

        public void DeleteRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            recruitmentCompanyRepository.DeleteRecruitmentCompany(recruitmentCompany);
        }

        public List<RecruitmentCompany> GetRecruitmentCompanies()
        {
            return recruitmentCompanyRepository.GetRecruitmentCompanies();
        }

        public RecruitmentCompany GetRecruitmentCompany(int id)
        {
            return recruitmentCompanyRepository.GetRecruitmentCompany(id);
        }

        public void UpdateRecruitmentCompany(RecruitmentCompany recruitmentCompany)
        {
            recruitmentCompanyRepository.UpdateRecruitmentCompany(recruitmentCompany);
        }
    }
}
