using BusinessObjects;
using DataAccessLayers.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMemberRepository
    {
        List<Member> GetMembers();
        Member GetMemberById(int id);
        void AddMember(Member member);
        void UpdateMember(UpdateMemberDTO updateMember);
        void DeleteMember(Member member);
    }
}
