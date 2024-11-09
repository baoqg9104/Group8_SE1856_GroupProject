using BusinessObjects;
using DataAccessLayers.DTOs.Member;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;

        public MemberService()
        {
            memberRepository = new MemberRepository();
        }

        public void AddMember(Member member)
        {
            memberRepository.AddMember(member);
        }

        public void DeleteMember(Member member)
        {
            memberRepository.DeleteMember(member);
        }

        public Member GetMemberById(int id)
        {
            return memberRepository.GetMemberById(id);
        }

        public List<Member> GetMembers()
        {
            return memberRepository.GetMembers();
        }

        public void UpdateMember(UpdateMemberDTO updateMember)
        {
            memberRepository.UpdateMember(updateMember);
        }
    }
}
