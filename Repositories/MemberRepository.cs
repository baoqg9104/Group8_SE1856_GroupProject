using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public void AddMember(Member member)
        {
            MemberDAO.AddMember(member);
        }

        public void DeleteMember(Member member)
        {
            MemberDAO.DeleteMember(member);
        }

        public Member GetMemberById(int id)
        {
            return MemberDAO.GetMemberById(id);
        }

        public List<Member> GetMembers()
        {
            return MemberDAO.GetMembers();
        }

        public void UpdateMember(UpdateMemberDTO updateMember)
        {
            MemberDAO.UpdateMember(updateMember);
        }
    }
}
