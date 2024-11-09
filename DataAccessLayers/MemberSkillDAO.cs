using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class MemberSkillDAO
    {
        public static List<MemberSkill> GetMemberSkillsByUserId(int userId)
        {
            using var context = new JobFinderContext();

            var member = context.Members.FirstOrDefault(m => m.UserId == userId);

            var memberSkills = context.MemberSkills
                .Include(ms => ms.Skill)
                .Where(e => e.MemberId == member.MemberId)
                .ToList();

            return memberSkills;
        }

        public static void AddMemberSkill(MemberSkill memberSkill)
        {
            if (memberSkill == null)
            {
                throw new ArgumentNullException("MemberSkill data cannot be null.");
            }

            ValidateMemberSkill(memberSkill);

            using var context = new JobFinderContext();

            context.MemberSkills.Add(memberSkill);
            context.SaveChanges();

        }

        public static void UpdateMemberSkill(MemberSkill memberSkill)
        {
            if (memberSkill == null)
            {
                throw new ArgumentNullException("MemberSkill data cannot be null.");
            }

            ValidateMemberSkill(memberSkill, memberSkill.MemberSkillId);

            using var context = new JobFinderContext();
            context.Entry<MemberSkill>(memberSkill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteMemberSkill(MemberSkill memberSkill)
        {
            using var context = new JobFinderContext();
            context.MemberSkills.Remove(memberSkill);
            context.SaveChanges();
        }

        private static void ValidateMemberSkill(MemberSkill memberSkill, int? memberSkillId = null)
        {
            // Check duplicate
            using var context = new JobFinderContext();

            var otherMemberSkills = context.MemberSkills
                .Any(ms => ms.MemberId == memberSkill.MemberId && ms.SkillId == memberSkill.SkillId && ms.MemberSkillId != memberSkillId);

            if (otherMemberSkills)
            {
                throw new Exception("Duplicated skill");
            }

        }

    }
}
