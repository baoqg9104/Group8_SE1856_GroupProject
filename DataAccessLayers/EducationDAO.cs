using BusinessObjects;
using DataAccessLayers.DTOs.Education;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class EducationDAO
    {
        public static List<Education> GetEducationsByUserId(int userId)
        {
            using var context = new JobFinderContext();

            var member = context.Members.FirstOrDefault(m => m.UserId == userId);

            var educations = context.Educations
                .Where(e => e.MemberId == member.MemberId)
                .ToList();

            return educations;
        }

        public static Education GetEducationById(int id)
        {
            using var context = new JobFinderContext();
            var education = context.Educations.FirstOrDefault(e => e.EducationId == id);
            return education;
        }

        public static void AddEducation(Education education)
        {
            if (education == null)
            {
                throw new ArgumentNullException("Education data cannot be null.");
            }

            ValidateEducation(education);

            using var context = new JobFinderContext();

            if (education.IsCurrent)
            {
                education.ToMonth = null;
                education.ToYear = null;
            }

            context.Educations.Add(education);
            context.SaveChanges();

        }

        public static void UpdateEducation(Education education)
        {
            if (education == null)
            {
                throw new ArgumentNullException("Education data cannot be null.");
            }

            ValidateEducation(education, education.EducationId);

            if (education.IsCurrent)
            {
                education.ToMonth = null;
                education.ToYear = null;
            }

            using var context = new JobFinderContext();
            context.Entry<Education>(education).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteEducation(Education education)
        {
            using var context = new JobFinderContext();
            context.Educations.Remove(education);
            context.SaveChanges();
        }

        private static void ValidateEducation(Education education, int? educationId = null)
        {
            education.School = education.School.Trim();
            education.Major = education.Major.Trim();

            if (string.IsNullOrWhiteSpace(education.School))
            {
                throw new Exception("School is required.");
            }

            if (string.IsNullOrWhiteSpace(education.Major))
            {
                throw new Exception("Major is required.");
            }

            if (education.FromMonth == 0)
            {
                throw new Exception("From month is required.");
            }

            if (education.FromYear == 0)
            {
                throw new Exception("From year is required.");
            }

            if (!education.IsCurrent)
            {
                if (education.ToMonth == 0)
                {
                    throw new Exception("To month is required");
                }

                if (education.ToYear == 0)
                {
                    throw new Exception("To year is required");
                }

                if (education.FromYear > education.ToYear
                 || (education.FromYear == education.ToYear
                 && education.FromMonth > education.ToMonth
                 ))
                {
                    throw new Exception("Please enter an end date that is later than the start date.");
                }
            }

            // Check overlap
            using var context = new JobFinderContext();

            var existingEducations = context.Educations
                .Where(e => e.MemberId == education.MemberId && e.EducationId != educationId)
                .ToList();

            foreach (var e in existingEducations)
            {
                bool overlap = false;

                if (education.IsCurrent && e.IsCurrent)
                {
                    overlap = true;
                }
                else if (!education.IsCurrent && !e.IsCurrent)
                {
                    var newStart = new DateTime(education.FromYear, education.FromMonth, 1);
                    var newEnd = new DateTime(education.ToYear.Value, education.ToMonth.Value, 1);
                    var existingStart = new DateTime(e.FromYear, e.FromMonth, 1);
                    var existingEnd = new DateTime(e.ToYear.Value, e.ToMonth.Value, 1);

                    if (newStart <= existingEnd && newEnd >= existingStart)
                    {
                        overlap = true;
                    }
                }
                else if (education.IsCurrent && !e.IsCurrent)
                {
                    var existingEnd = new DateTime(e.ToYear.Value, e.ToMonth.Value, 1);
                    var newStart = new DateTime(education.FromYear, education.FromMonth, 1);

                    if (newStart <= existingEnd)
                    {
                        overlap = true;
                    }
                }
                else if (!education.IsCurrent && e.IsCurrent)
                {
                    var newEnd = new DateTime(education.ToYear.Value, education.ToMonth.Value, 1);
                    var existingStart = new DateTime(e.FromYear, e.FromMonth, 1);

                    if (newEnd >= existingStart)
                    {
                        overlap = true;
                    }
                }

                if (overlap)
                {
                    throw new Exception("The education period overlaps with an existing education record.");
                }
            }

        }
    }
}
