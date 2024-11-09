using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MemberSkillService : IMemberSkillService
    {
        private readonly IMemberSkillRepository memberSkillRepository;

        public MemberSkillService()
        {
            memberSkillRepository = new MemberSkillRepository();
        }

        public void AddMemberSkill(MemberSkill memberSkill)
        {
            memberSkillRepository.AddMemberSkill(memberSkill);
        }

        public void DeleteMemberSkill(MemberSkill memberSkill)
        {
            memberSkillRepository.DeleteMemberSkill(memberSkill);
        }

        public List<MemberSkill> GetMemberSkillsByUserId(int userId)
        {
            return memberSkillRepository.GetMemberSkillsByUserId(userId);
        }

        public void UpdateMemberSkill(MemberSkill memberSkill)
        {
            memberSkillRepository.UpdateMemberSkill(memberSkill);
        }
    }
}
