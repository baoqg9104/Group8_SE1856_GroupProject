using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class SkillDAO
    {
        public static List<Skill> GetSkills()
        {
            using var context = new JobFinderContext();
            var skills = context.Skills.ToList();
            return skills;
        }

        public static void AddSkill(Skill skill)
        {
            using var context = new JobFinderContext();
            context.Skills.Add(skill);
            context.SaveChanges();
        }

        public static void UpdateSkill(Skill skill)
        {
            using var context = new JobFinderContext();
            context.Entry<Skill>(skill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteSkill(Skill skill)
        {
            using var context = new JobFinderContext();
            context.Skills.Remove(skill);
            context.SaveChanges();
        }
    }
}
