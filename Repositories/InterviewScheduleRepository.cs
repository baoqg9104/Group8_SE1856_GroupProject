using BusinessObjects;
using DataAccessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class InterviewScheduleRepository : IInterviewScheduleRepository
    {
        public void AddInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            InterviewScheduleDAO.AddInterviewSchedule(interviewSchedule);
        }

        public void DeleteInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            InterviewScheduleDAO.DeleteInterviewSchedule(interviewSchedule);
        }

        public InterviewSchedule GetInterviewSchedule(int id)
        {
            return InterviewScheduleDAO.GetInterviewSchedule(id);
        }

        public List<InterviewSchedule> GetInterviewSchedules()
        {
            return InterviewScheduleDAO.GetInterviewSchedules();
        }

        public void UpdateInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            InterviewScheduleDAO.UpdateInterviewSchedule(interviewSchedule);
        }
    }
}
