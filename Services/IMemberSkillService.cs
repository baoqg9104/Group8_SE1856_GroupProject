using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMemberSkillService
    {
        List<MemberSkill> GetMemberSkillsByUserId(int userId);
        void AddMemberSkill(MemberSkill memberSkill);
        void UpdateMemberSkill(MemberSkill memberSkill);
        void DeleteMemberSkill(MemberSkill memberSkill);
    }
}
