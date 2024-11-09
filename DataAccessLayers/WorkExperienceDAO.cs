using BusinessObjects;
using DataAccessLayers.DTOs.Education;
using DataAccessLayers.DTOs.WorkExperience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class WorkExperienceDAO
    {
        public static List<WorkExperience> GetWorkExperiencesByUserId(int userId)
        {
            using var context = new JobFinderContext();

            var member = context.Members.FirstOrDefault(m => m.UserId == userId);

            var experiences = context.WorkExperiences
                .Where(e => e.MemberId == member.MemberId)
                .ToList();

            return experiences;
        }

        public static void AddWorkExperience(WorkExperience workExperience)
        {
            if (workExperience == null)
            {
                throw new ArgumentNullException("WorkExperience data cannot be null.");
            }

            ValidateWorkExperience(workExperience);

            using var context = new JobFinderContext();

            if (workExperience.IsCurrent)
            {
                workExperience.ToMonth = null;
                workExperience.ToYear = null;
            }

            context.WorkExperiences.Add(workExperience);
            context.SaveChanges();

        }

        public static void UpdateWorkExperience(WorkExperience workExperience)
        {
            if (workExperience == null)
            {
                throw new ArgumentNullException("WorkExperience data cannot be null.");
            }

            ValidateWorkExperience(workExperience, workExperience.ExperienceId);

            if (workExperience.IsCurrent)
            {
                workExperience.ToMonth = null;
                workExperience.ToYear = null;
            }

            using var context = new JobFinderContext();
            context.Entry<WorkExperience>(workExperience).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteWorkExperience(WorkExperience workExperience)
        {
            using var context = new JobFinderContext();
            context.WorkExperiences.Remove(workExperience);
            context.SaveChanges();
        }

        private static void ValidateWorkExperience(WorkExperience workExperience, int? workExperienceId = null)
        {
            workExperience.JobTitle = workExperience.JobTitle.Trim();
            workExperience.CompanyName = workExperience.CompanyName.Trim();

            if (string.IsNullOrWhiteSpace(workExperience.JobTitle))
            {
                throw new Exception("Job title is required.");
            }

            if (string.IsNullOrWhiteSpace(workExperience.CompanyName))
            {
                throw new Exception("Company name is required.");
            }

            if (workExperience.FromMonth == 0)
            {
                throw new Exception("From month is required.");
            }

            if (workExperience.FromYear == 0)
            {
                throw new Exception("From year is required.");
            }

            if (!workExperience.IsCurrent)
            {
                if (workExperience.ToMonth == 0)
                {
                    throw new Exception("To month is required");
                }

                if (workExperience.ToYear == 0)
                {
                    throw new Exception("To year is required");
                }

                if (workExperience.FromYear > workExperience.ToYear
                 || (workExperience.FromYear == workExperience.ToYear
                 && workExperience.FromMonth > workExperience.ToMonth
                 ))
                {
                    throw new Exception("Please enter an end date that is later than the start date.");
                }
            }

            // Check overlap
            using var context = new JobFinderContext();

            var existingWorkExperiences = context.WorkExperiences
                .Where(e => e.MemberId == workExperience.MemberId && e.ExperienceId != workExperienceId)
                .ToList();

            foreach (var e in existingWorkExperiences)
            {
                bool overlap = false;

                if (workExperience.IsCurrent && e.IsCurrent)
                {
                    overlap = true;
                }
                else if (!workExperience.IsCurrent && !e.IsCurrent)
                {
                    var newStart = new DateTime(workExperience.FromYear, workExperience.FromMonth, 1);
                    var newEnd = new DateTime(workExperience.ToYear.Value, workExperience.ToMonth.Value, 1);
                    var existingStart = new DateTime(e.FromYear, e.FromMonth, 1);
                    var existingEnd = new DateTime(e.ToYear.Value, e.ToMonth.Value, 1);

                    if (newStart <= existingEnd && newEnd >= existingStart)
                    {
                        overlap = true;
                    }
                }
                else if (workExperience.IsCurrent && !e.IsCurrent)
                {
                    var existingEnd = new DateTime(e.ToYear.Value, e.ToMonth.Value, 1);
                    var newStart = new DateTime(workExperience.FromYear, workExperience.FromMonth, 1);

                    if (newStart <= existingEnd)
                    {
                        overlap = true;
                    }
                }
                else if (!workExperience.IsCurrent && e.IsCurrent)
                {
                    var newEnd = new DateTime(workExperience.ToYear.Value, workExperience.ToMonth.Value, 1);
                    var existingStart = new DateTime(e.FromYear, e.FromMonth, 1);

                    if (newEnd >= existingStart)
                    {
                        overlap = true;
                    }
                }

                if (overlap)
                {
                    throw new Exception("The work experience period overlaps with an existing work experience record.");
                }
            }
        }
    }
}
