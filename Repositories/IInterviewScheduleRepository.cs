using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IInterviewScheduleRepository
    {
        List<InterviewSchedule> GetInterviewSchedules();
        InterviewSchedule GetInterviewSchedule(int id);
        void AddInterviewSchedule(InterviewSchedule interviewSchedule);
        void UpdateInterviewSchedule(InterviewSchedule interviewSchedule);
        void DeleteInterviewSchedule(InterviewSchedule interviewSchedule);
    }
}
