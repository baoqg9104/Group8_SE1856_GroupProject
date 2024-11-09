using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class InterviewScheduleService : IInterviewScheduleService
    {
        private readonly IInterviewScheduleRepository interviewScheduleRepository;

        public InterviewScheduleService()
        {
            interviewScheduleRepository = new InterviewScheduleRepository();
        }

        public void AddInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            interviewScheduleRepository.AddInterviewSchedule(interviewSchedule);
        }

        public void DeleteInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            interviewScheduleRepository.DeleteInterviewSchedule(interviewSchedule);
        }

        public InterviewSchedule GetInterviewSchedule(int id)
        {
            return interviewScheduleRepository.GetInterviewSchedule(id);
        }

        public List<InterviewSchedule> GetInterviewSchedules()
        {
            return interviewScheduleRepository.GetInterviewSchedules();
        }

        public void UpdateInterviewSchedule(InterviewSchedule interviewSchedule)
        {
            interviewScheduleRepository.UpdateInterviewSchedule(interviewSchedule);
        }
    }
}
