using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MemberSkillRepository : IMemberSkillRepository
    {
        public void AddMemberSkill(MemberSkill memberSkill)
        {
            MemberSkillDAO.AddMemberSkill(memberSkill);
        }

        public void DeleteMemberSkill(MemberSkill memberSkill)
        {
            MemberSkillDAO.DeleteMemberSkill(memberSkill);
        }

        public List<MemberSkill> GetMemberSkillsByUserId(int userId)
        {
            return MemberSkillDAO.GetMemberSkillsByUserId(userId);
        }

        public void UpdateMemberSkill(MemberSkill memberSkill)
        {
            MemberSkillDAO.UpdateMemberSkill(memberSkill);
        }
    }
}
