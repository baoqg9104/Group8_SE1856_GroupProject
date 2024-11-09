using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class InterviewScheduleDAO
    {
        public static List<InterviewSchedule> GetInterviewSchedules()
        {
            var list = new List<InterviewSchedule>();
            try
            {
                using var context = new JobFinderContext();
                list = context.InterviewSchedules
                    .Include(i => i.JobApplication)
                        .ThenInclude(ja => ja.Job)
                            .ThenInclude(j => j.Company)
                    .Include(i => i.JobApplication)
                        .ThenInclude(ja => ja.Member)
                            .ThenInclude(m => m.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public static InterviewSchedule GetInterviewSchedule(int id)
        {
            using var context = new JobFinderContext();
            return context.InterviewSchedules.FirstOrDefault(i => i.InterviewId == id);
        }

        public static void AddInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            using var context = new JobFinderContext();

            if (interviewSchedule == null)
            {
                throw new Exception("Interview Scheulde not found");
            }

            if (interviewSchedule.InterviewDate < DateTime.Now.AddDays(2))
            {
                throw new Exception("Interview date must be at least two days in the future.");
            }

            if (interviewSchedule.InterviewDate > DateTime.Now.AddDays(30))
            {
                throw new Exception("Interview date must not be exceed 30 days in the future");
            }

            context.InterviewSchedules.Add(interviewSchedule);
            context.SaveChanges();
        }

        public static void UpdateInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            using var context = new JobFinderContext();

            if (interviewSchedule.Status == "Confirmed" && DateTime.Now > interviewSchedule.InterviewDate)
            {
                throw new Exception("Overdue to confirm");
            }

            if (interviewSchedule.Status == "Completed" && DateTime.Now < interviewSchedule.InterviewDate)
            {
                throw new Exception("The interview is not over");
            }

            if (interviewSchedule.Status == "No Show" && DateTime.Now < interviewSchedule.InterviewDate)
            {
                throw new Exception("The interview is not over");
            }

            context.Entry<InterviewSchedule>(interviewSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            using var context = new JobFinderContext();
            context.InterviewSchedules.Remove(interviewSchedule);
            context.SaveChanges();
        }
    }
}
